using Global.ProductsManagement.Domain.Entities;

namespace Global.ProductsManagement.Application.Features.Products.Queries.GetProduct
{
    public class GetProductResponse
    {
        public GetProductResponse(Guid id, string name, string details, double price, DateTime createDate, 
            DateTime? updateDate, Guid categoryId, EProdutoStatus status, Guid brandId, string brandName, string categoryName)
        {
            Id = id;
            Name = name;
            Details = details;
            Price = price;
            CreateDate = createDate;
            UpdateDate = updateDate;
            CategoryId = categoryId;
            Status = status;
            BrandId = brandId;
            BrandName = brandName;
            CategoryName = categoryName;
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Details { get; set; }
        public double Price { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public Guid CategoryId { get; set; }
        public EProdutoStatus Status { get; set; }
        public Guid BrandId { get; set; }
        public string BrandName { get; set; }
        public string CategoryName { get; set; }
    }
}