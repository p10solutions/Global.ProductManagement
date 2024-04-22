using Global.ProductsManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Global.ProductsManagement.Infraestructure.Data
{
    public class ProductManagementContext(DbContextOptions<ProductManagementContext> options) : DbContext(options)
    {
        public DbSet<Product> Product { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Brand> Brand { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
