using Microsoft.EntityFrameworkCore;

namespace MiniProject010.Models
{
    public class Context : DbContext
    {
        public DbSet<Color> Colors { get; set; }
        public Context(DbContextOptions options) : base(options) { }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
    }
}
