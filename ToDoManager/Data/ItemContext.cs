using Microsoft.EntityFrameworkCore;
using ToDoManager.Models;

namespace ToDoManager.Data
{
    public class ItemContext : DbContext
    {
        public ItemContext(DbContextOptions<ItemContext> options) : base(options) { }
        public DbSet<Item> Items { get; set; }
    }
}
