namespace BookInventory.Dtos
{
    public class AuthorResponseDto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; } = String.Empty;
        public string LastName { get; set; } = String.Empty;
        public List<Guid> BookIds { get; set; } = [];
    }
}
