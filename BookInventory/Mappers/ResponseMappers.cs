using BookInventory.Dtos;
using BookInventory.Models;

namespace BookInventory.Mappers
{
    public class ResponseMappers
    {
        public BookResponseDto ToBookReponseDto(Book book)
        {
            return new BookResponseDto
            {
                Id = book.Id,
                Title = book.Title,
                Authors = book.Authors.Select(author => ToAuthorResponseDto(author)).ToList(),
                Genre = book.Genre,
                Pages = book.Pages,
                Quantity = book.Quantity,
            };
        }

        public AuthorResponseDto ToAuthorResponseDto(Author author)
        {
            return new AuthorResponseDto
            {
                Id = author.Id,
                FirstName = author.FirstName,
                LastName = author.LastName,
                BookIds = author.Books.Select(book => book.Id).ToList(),
            };
        }
    }
}
