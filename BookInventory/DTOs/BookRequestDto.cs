namespace BookInventory.DTOs
{
    public class BookRequestDto
    {
        public string Title { get; set; } = String.Empty;
        public List<AuthorRequestDto> Authors { get; set; } = [];
        public string Genre { get; set; } = String.Empty;
        public int Pages { get; set; }
        public int Quantity { get; set; }
    }
}
