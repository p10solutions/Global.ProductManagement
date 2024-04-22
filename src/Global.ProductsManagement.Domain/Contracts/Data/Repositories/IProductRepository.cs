using Global.ProductsManagement.Domain.Entities;

namespace Global.ProductsManagement.Domain.Contracts.Data.Repositories
{
    public interface IProductRepository
    {
        Task AddAsync(Product product);
        void Update(Product product);
        Task<Product?> GetAsync(Guid id);
        bool Exists(string name, Guid categoryId, Guid brandId, Guid productId);
    }
}
