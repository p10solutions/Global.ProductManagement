using FluentValidation;

namespace Global.ProductsManagement.Application.Features.Products.Queries.GetProduct
{
    public class GetProductQueryValidator : AbstractValidator<GetProductQuery>
    {
        public GetProductQueryValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
        }
    }
}
