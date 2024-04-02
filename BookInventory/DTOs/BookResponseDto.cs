using BookInventory.Models;

namespace BookInventory.Dtos
{
    public class BookResponseDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = String.Empty;
        public List<AuthorResponseDto> Authors { get; set; } = [];
        public string Genre { get; set; } = String.Empty;
        public int Pages { get; set; }
        public int Quantity { get; set; }
    }
}
