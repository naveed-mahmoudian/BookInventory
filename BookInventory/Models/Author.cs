using System.Text.Json.Serialization;

namespace BookInventory.Models
{
    public class Author
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; } = String.Empty;
        public string LastName { get; set; } = String.Empty;
        [JsonIgnore]
        public List<Book> Books { get; set; } = [];
    }
}
