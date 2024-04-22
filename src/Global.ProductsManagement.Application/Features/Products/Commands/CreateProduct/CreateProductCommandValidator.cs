using FluentValidation;

namespace Global.ProductsManagement.Application.Features.Products.Commands.CreateProduct
{
    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Name).Length(2, 200);
            RuleFor(x => x.Price).NotEmpty();
            RuleFor(x => x.BrandId).NotEmpty();
            RuleFor(x => x.CategoryId).NotEmpty();
            RuleFor(x=>x.Status).NotEmpty();
        }
    }
}
