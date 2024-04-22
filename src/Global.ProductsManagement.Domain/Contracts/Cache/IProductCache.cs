using Global.ProductsManagement.Domain.Entities;

namespace Global.ProductsManagement.Domain.Contracts.Cache
{
    public interface IProductCache
    {
        Task AddAsync(Product product);
        Task<Product?> GetAsync(Guid id);
    }
}
