using AutoMapper;
using Global.ProductsManagement.Domain.Entities;

namespace Global.ProductsManagement.Application.Features.Products.Queries.GetProduct
{
    public class GetProductMapper: Profile
    {
        public GetProductMapper()
        {
            CreateMap<Product, GetProductResponse>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name))
                .ForMember(dest => dest.BrandName, opt => opt.MapFrom(src => src.Brand.Name));
        }
    }
}
