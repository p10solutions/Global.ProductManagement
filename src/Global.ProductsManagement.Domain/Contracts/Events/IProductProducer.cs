using Global.ProductsManagement.Domain.Models.Events.Products;

namespace Global.ProductsManagement.Domain.Contracts.Events
{
    public interface IProductProducer
    {
        Task SendCreatedEventAsync(CreatedProductEvent @event);
        Task SendUpdatedEventAsync(UpdatedProductEvent @event);
    }
}
