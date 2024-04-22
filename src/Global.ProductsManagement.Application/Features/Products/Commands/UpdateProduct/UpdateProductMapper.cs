using AutoMapper;
using Global.ProductsManagement.Domain.Entities;
using Global.ProductsManagement.Domain.Models.Events.Products;

namespace Global.ProductsManagement.Application.Features.Products.Commands.UpdateProduct
{
    public class UpdateProductMapper : Profile
    {
        public UpdateProductMapper()
        {
            CreateMap<UpdateProductCommand, Product>()
                .ForMember(dest => dest.UpdateDate, opt => opt.MapFrom(src=> DateTime.Now));
            CreateMap<Product, UpdateProductResponse>();
            CreateMap<Product, UpdatedProductEvent>();
        }
    }
}
