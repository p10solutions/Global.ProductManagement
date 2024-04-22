using Confluent.Kafka;
using Global.ProductsManagement.Domain.Models.Events.Products;
using System.Text;
using System.Text.Json;

namespace Global.ProductsManagement.Infraestructure.Events.Products
{
    public class UpdatedProductEventSerializer : IAsyncSerializer<UpdatedProductEvent>
    {
        public Task<byte[]> SerializeAsync(UpdatedProductEvent data, SerializationContext context)
        {
            var json = JsonSerializer.Serialize(data);
            return Task.FromResult(Encoding.ASCII.GetBytes(json));
        }
    }
}
