using BookInventory.DTOs;
using BookInventory.Mappers;
using BookInventory.Models;
using BookInventory.Services;
using Microsoft.AspNetCore.Mvc;

namespace BookInventory.Controllers
{
    [ApiController]
    [Route("api/book")]
    public class BookController : ControllerBase
    {
        private readonly BookService _bookService;

        public BookController(BookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Book>>> GetAll() 
        {
            List<Book> books = await _bookService.GetBooksAsync();

            return Ok(books);
        }

        [HttpGet("{id:Guid}")]
        public async Task<ActionResult<Book>> Get([FromRoute] Guid id)
        {
            Book? book = await _bookService.GetBookAsync(id);

            if (book == null) { return NotFound(); }

            return Ok(book);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] BookRequestDto bookDto)
        {
            Book book = new Book
            {
                Title = bookDto.Title,
                Authors = bookDto.Authors.Select(author => 
                new Author { FirstName = author.FirstName, LastName = author.LastName }).ToList(),
                Genre = bookDto.Genre,
                Pages = bookDto.Pages,
                Quantity = bookDto.Quantity,
            };

            await _bookService.CreateBookAsync(book);

            return Ok();
        }

        [HttpPut("{id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] BookRequestDto bookDto)
        {
            Book updatedbook = new Book
            {
                Id = id,
                Title = bookDto.Title,
                Authors = bookDto.Authors.Select(author =>
                new Author { FirstName = author.FirstName, LastName = author.LastName }).ToList(),
                Genre = bookDto.Genre,
                Pages = bookDto.Pages,
                Quantity = bookDto.Quantity,
            };

            try
            {
                await _bookService.UpdateBookAsync(updatedbook);

                return Ok();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            try
            {
                await _bookService.DeleteBookAsync(id);

                return NoContent();
            }
            catch(Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
