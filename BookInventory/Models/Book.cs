namespace BookInventory.Models
{
    public class Book
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = String.Empty;
        public List<Author> Authors { get; set; } = [];
        public string Genre { get; set; } = String.Empty;
        public int Pages { get; set; }
        public int Quantity { get; set; }
    }
}
