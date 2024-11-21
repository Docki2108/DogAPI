using Microsoft.EntityFrameworkCore;
using TestAPI.Models;

namespace TestAPI.DB
{
    public class DBContext(DbContextOptions<DBContext> options) : DbContext(options)
    {
        public required DbSet<Dog> Dogs { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Dog>().HasKey(b => b.ID);
        }
    }
}
