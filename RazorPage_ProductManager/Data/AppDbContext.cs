using Microsoft.EntityFrameworkCore;
using RazorPage_ProductManager.Core.Models;

namespace RazorPage_ProductManager.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>(e =>
            {
                e.Property(p => p.Price).HasColumnType("decimal(18, 2)");
                e.Property(p => p.Description).IsRequired(false);
            });
        }
    }
}
