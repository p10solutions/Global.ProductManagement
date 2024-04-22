using Global.ProductsManagement.Domain.Contracts.Cache;
using Global.ProductsManagement.Domain.Entities;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using System.Text;
using System.Text.Json;

namespace Global.ProductsManagement.Infraestructure.Cache
{
    public class ProductCache : IProductCache
    {
        readonly IDistributedCache _distributedCache;
        readonly int _minutesExpiration;
        readonly string _cacheKey;

        public ProductCache(IDistributedCache distributedCache, IConfiguration configuration)
        {
            _distributedCache = distributedCache;
            _minutesExpiration = Convert.ToInt32(configuration.GetSection("Cache:MinutesExpiration").Value);
            _cacheKey = configuration.GetSection("Cache:Key").Value;
        }

        public async Task AddAsync(Product product)
        {
            var serializedProduct = JsonSerializer.Serialize(product);

            var cacheProduct = Encoding.UTF8.GetBytes(serializedProduct);

            var options = new DistributedCacheEntryOptions()
                .SetAbsoluteExpiration(DateTime.Now.AddMinutes(_minutesExpiration));

            var key = $"{_cacheKey}:{product.Id}";

            await _distributedCache.SetAsync(key, cacheProduct, options);
        }

        public async Task<Product?> GetAsync(Guid id)
        {
            var key = $"{_cacheKey}:{id}";

            var productCache = await _distributedCache.GetAsync(key);

            if (productCache == null)
                return null;    

            var productSerialized = Encoding.UTF8.GetString(productCache);

            var product = JsonSerializer.Deserialize<Product>(productSerialized);

            return product;
        }
    }
}
