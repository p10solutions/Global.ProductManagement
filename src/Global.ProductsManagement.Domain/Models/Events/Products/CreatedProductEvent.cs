using Global.ProductsManagement.Domain.Entities;

namespace Global.ProductsManagement.Domain.Models.Events.Products
{
    public record CreatedProductEvent
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public Guid CategoryId { get; set; }
        public EProdutoStatus Status { get; set; }
        public Guid BrandId { get; set; }
    }
}
