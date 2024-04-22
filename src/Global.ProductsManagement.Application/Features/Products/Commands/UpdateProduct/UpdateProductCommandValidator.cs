using FluentValidation;

namespace Global.ProductsManagement.Application.Features.Products.Commands.UpdateProduct
{
    public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Name).Length(2, 200);
            RuleFor(x => x.Price).NotEmpty();
            RuleFor(x => x.BrandId).NotEmpty();
            RuleFor(x => x.CategoryId).NotEmpty();
            RuleFor(x=>x.Status).NotEmpty();
        }
    }
}
