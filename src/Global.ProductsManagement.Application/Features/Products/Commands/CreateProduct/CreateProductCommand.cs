using Global.ProductsManagement.Application.Features.Common;
using Global.ProductsManagement.Domain.Entities;
using MediatR;

namespace Global.ProductsManagement.Application.Features.Products.Commands.CreateProduct
{
    public class CreateProductCommand(string name, string details, double price, Guid categoryId, EProdutoStatus status, Guid brandId) 
        : CommandBase<CreateProductCommand>(new CreateProductCommandValidator()), IRequest<CreateProductResponse>
    {
        public string Name { get; init; } = name;
        public string Details { get; init; } = details;
        public double Price { get; init; } = price;
        public Guid CategoryId { get; init; } = categoryId;
        public EProdutoStatus Status { get; init; } = status;
        public Guid BrandId { get; init; } = brandId;
    }
}
