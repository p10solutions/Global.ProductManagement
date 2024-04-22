using Global.ProductsManagement.Domain.Contracts.Data.Repositories;
using Global.ProductsManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Global.ProductsManagement.Infraestructure.Data.Repositories
{
    public class ProductRepository : IProductRepository
    {
        readonly ProductManagementContext _context;

        public ProductRepository(ProductManagementContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Product product)
            => await _context.Product.AddAsync(product);

        public async Task<Product?> GetAsync(Guid id)
            => await _context.Product
                .Include(x => x.Brand)
                .Include(x => x.Category)
                .SingleOrDefaultAsync(x => x.Id == id);

        public void Update(Product product)
            => _context.Product.Update(product);

        public bool Exists(string name, Guid categoryId, Guid brandId, Guid productId)
            => _context.Product.Any(x => x.Name == name && x.CategoryId == categoryId && x.BrandId == brandId && x.Id != productId);

    }
}
