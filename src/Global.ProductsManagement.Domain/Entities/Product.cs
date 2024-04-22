namespace Global.ProductsManagement.Domain.Entities
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Details { get; set; }
        public double Price { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public Guid CategoryId { get; set; }
        public EProdutoStatus Status { get; set; }
        public Guid BrandId { get; set; }
        public Category? Category { get; set; }
        public Brand? Brand { get; set; }

        public Product(string name, double price, Guid categoryId, EProdutoStatus status, Guid brandId, string details)
        {
            Id = Guid.NewGuid();
            Name = name;
            Price = price;
            CreateDate = DateTime.Now;
            CategoryId = categoryId;
            Status = status;
            BrandId = brandId;
            Details = details;
        }
    }
}
