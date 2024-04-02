using BookInventory.Dtos;
using BookInventory.Mappers;
using BookInventory.Models;
using BookInventory.Services;
using Microsoft.AspNetCore.Mvc;

namespace BookInventory.Controllers
{
    [ApiController]
    [Route("api/author")]
    public class AuthorController : ControllerBase
    {
        private readonly AuthorService _authorService;
        private readonly ResponseMappers _responseMappers;

        public AuthorController(AuthorService authorService, ResponseMappers responseMappers)
        {
            _authorService = authorService;
            _responseMappers = responseMappers;
        }

        [HttpGet]
        public async Task<ActionResult<List<AuthorResponseDto>>> GetAll()
        {
            List<Author> authors = await _authorService.GetAuthorsAsync();
            List<AuthorResponseDto> authorResponseDtos = authors.Select(author => 
            _responseMappers.ToAuthorResponseDto(author)).ToList();

            return Ok(authorResponseDtos);
        }

        [HttpGet("{id:Guid}")]
        public async Task<ActionResult<AuthorResponseDto>> Get([FromRoute] Guid id)
        {
            var author = await _authorService.GetAuthorAsync(id);

            if (author == null) { return NotFound(); }

            AuthorResponseDto authorResponseDto = _responseMappers.ToAuthorResponseDto(author);
            
            return Ok(authorResponseDto);
        }
    }
}
