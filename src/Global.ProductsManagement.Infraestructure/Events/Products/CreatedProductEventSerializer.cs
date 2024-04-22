using Confluent.Kafka;
using Global.ProductsManagement.Domain.Models.Events.Products;
using System.Text;
using System.Text.Json;

namespace Global.ProductsManagement.Infraestructure.Events.Products
{
    public class CreatedProductEventSerializer : IAsyncSerializer<CreatedProductEvent>
    {
        public Task<byte[]> SerializeAsync(CreatedProductEvent data, SerializationContext context)
        {
            var json = JsonSerializer.Serialize(data);
            return Task.FromResult(Encoding.ASCII.GetBytes(json));
        }
    }
}
