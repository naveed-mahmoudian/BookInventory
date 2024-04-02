using BookInventory.Data;
using BookInventory.Models;
using Microsoft.EntityFrameworkCore;

namespace BookInventory.Services
{
    public class AuthorService
    {
        private readonly BookDbContext _context;

        public AuthorService(BookDbContext context)
        { 
            _context = context;
        }

        public async Task<List<Author>> GetAuthorsAsync()
        {
            List<Author> authors = await _context.Author
                .Include(author => author.Books)
                .ToListAsync();

            return authors;
        }

        public async Task<Author?> GetAuthorAsync(Guid id)
        {
            var author = await _context.Author
                .Include(a => a.Books)
                .FirstOrDefaultAsync(author => author.Id == id);

            return author;
        }
    }
}
