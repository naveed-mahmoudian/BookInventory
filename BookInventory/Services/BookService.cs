using BookInventory.Data;
using BookInventory.Models;
using Microsoft.EntityFrameworkCore;

namespace BookInventory.Services
{
    public class BookService
    {
        private readonly BookDbContext _context;
        
        public BookService(BookDbContext context)
        {
            _context = context;
        }

        public async Task<List<Book>> GetBooksAsync()
        {
            List<Book> books = await _context.Book
                .Include(book => book.Authors)
                .ToListAsync();

            return books;
        }

        public async Task<Book?> GetBookAsync(Guid id)
        {
            Book? book = await _context.Book
                .Include(book => book.Authors)
                .FirstOrDefaultAsync(book => book.Id == id);

            return book;
        }

        public async Task CreateBookAsync(Book book)
        {
            /*
             * When the client adds a Book, create an empty list to track existing authors. 
             * Loop through the authors sent by the client.
             * If the author doesn't exist, add them to the database then track them in the existing authors list.
             * If the author does exist, track them in the existing authors list.
             * Finally, update the authors list in the Book sent from the client with the existing authors list.
            */

            List<Author> existingAuthors = new List<Author>();

            foreach (Author author in book.Authors)
            {
                Author? existingAuthor = await _context.Author
                    .FirstOrDefaultAsync(a => a.FirstName == author.FirstName 
                    && a.LastName == author.LastName);

                if (existingAuthor == null)
                {
                    await _context.Author.AddAsync(author);
                    existingAuthors.Add(author);
                }
                else
                {
                    existingAuthors.Add(existingAuthor);
                }
            }

            book.Authors = existingAuthors;

            await _context.Book.AddAsync(book);
            await _context.SaveChangesAsync();

            return;
        }

        public async Task UpdateBookAsync(Book updatedBook)
        {
            Book? bookToUpdate = await _context.Book.Include(book => book.Authors).FirstOrDefaultAsync(book => book.Id == updatedBook.Id);

            if (bookToUpdate == null)
            {
                throw new Exception($"No books found with id: {updatedBook.Id}");
            }

            // Keep track of author ids that currently exist in the book to update
            List<Guid> authorIds = new List<Guid>();
            foreach (Author existingBookAuthor in bookToUpdate.Authors)
            {
                authorIds.Add(existingBookAuthor.Id);
            }

            /* Loop through the authors that were submitted by the client.
             * Check if the author exists in the db
             * If it doesn't exist in db, add it to db and add it to the book to update.
             * If it does exist in db, check to see if the author exists in the book to update (by Id in authorIds)
             * and if it doesn't, it is safe to add to the book to update
            */
            foreach (Author author in updatedBook.Authors)
            {
                Author? existingAuthor = await _context.Author
                    .FirstOrDefaultAsync(a => a.FirstName == author.FirstName
                    && a.LastName == author.LastName);

                if (existingAuthor == null)
                {
                    await _context.Author.AddAsync(author);
                    bookToUpdate.Authors.Add(author);
                }
                else
                {
                    if (authorIds.Contains(existingAuthor.Id) == false)
                    {
                        bookToUpdate.Authors.Add(existingAuthor);
                    }
                }
            }

            bookToUpdate.Title = updatedBook.Title;
            bookToUpdate.Genre = updatedBook.Genre;
            bookToUpdate.Pages = updatedBook.Pages;
            bookToUpdate.Quantity = updatedBook.Quantity;

            await _context.SaveChangesAsync();

            return;
        }

        public async Task DeleteBookAsync(Guid id)
        {
            Book? bookToDelete = await _context.Book.FirstOrDefaultAsync(book => book.Id == id);

            if (bookToDelete == null)
            {
                throw new Exception($"No books found with id: {id}");
            }

            _context.Book.Remove(bookToDelete);
            await _context.SaveChangesAsync();

            return;
        }
    }
}
