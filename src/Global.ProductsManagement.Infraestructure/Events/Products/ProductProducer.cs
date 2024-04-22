using Confluent.Kafka;
using Global.ProductsManagement.Domain.Contracts.Events;
using Global.ProductsManagement.Domain.Models.Events.Products;
using Microsoft.Extensions.Configuration;

namespace Global.ProductsManagement.Infraestructure.Events.Products
{
    public class ProductProducer : IProductProducer
    {
        readonly ProducerConfig _config;
        readonly string _topic;

        public ProductProducer(IConfiguration configuration)
        {
            _topic = configuration.GetSection("Kafka:Topic").Value;
            _config = new ProducerConfig
            {
                BootstrapServers = configuration.GetSection("Kafka:Server").Value,
            };
        }

        public async Task SendCreatedEventAsync(CreatedProductEvent @event)
        {
            using var producer = new ProducerBuilder<Null, CreatedProductEvent>(_config)
                .SetValueSerializer(new CreatedProductEventSerializer())
                .Build();

            var message = new Message<Null, CreatedProductEvent>() { Value = @event };

            await producer.ProduceAsync(_topic, message);
        }

        public async Task SendUpdatedEventAsync(UpdatedProductEvent @event)
        {
            using var producer = new ProducerBuilder<Null, UpdatedProductEvent>(_config)
                .SetValueSerializer(new UpdatedProductEventSerializer())
                .Build();

            var message = new Message<Null, UpdatedProductEvent>() { Value = @event };

            await producer.ProduceAsync(_topic, message);
        }
    }
}
