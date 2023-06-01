using CodeBridgeTestTask.Models;
using Microsoft.EntityFrameworkCore;

namespace CodeBridgeTestTask.Data
{
    public class DogDbContext : DbContext
    {
        public DogDbContext(DbContextOptions<DogDbContext> options) : base(options) { }

        public DbSet<Dog> Dogs { get; set; }
    }
}
