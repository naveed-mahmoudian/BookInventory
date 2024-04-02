using BookInventory.Models;
using Microsoft.EntityFrameworkCore;

namespace BookInventory.Data
{
    public class BookDbContext : DbContext
    {
        public BookDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions) { }

        public DbSet<Book> Book { get; set; }
        public DbSet<Author> Author { get; set; }
    }
}
