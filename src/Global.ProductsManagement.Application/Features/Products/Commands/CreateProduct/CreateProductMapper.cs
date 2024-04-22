using AutoMapper;
using Global.ProductsManagement.Domain.Entities;
using Global.ProductsManagement.Domain.Models.Events.Products;

namespace Global.ProductsManagement.Application.Features.Products.Commands.CreateProduct
{
    public class CreateProductMapper: Profile
    {
        public CreateProductMapper()
        {
            CreateMap<CreateProductCommand, Product>();
            CreateMap<Product, CreateProductResponse>();
            CreateMap<Product, CreatedProductEvent>();
        }
    }
}
