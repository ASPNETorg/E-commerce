using Microsoft.EntityFrameworkCore;
using E_commerce.Domain.DomainModels;

namespace E_commerce.Infrastructure.Data
{
    public class ProjectDbContext : DbContext
    {
        public ProjectDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Person> People { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<OrderHeader> OrderHeaders { get; set; }
    }
}
