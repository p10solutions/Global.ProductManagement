using Global.ProductsManagement.Application.Features.Common;
using MediatR;

namespace Global.ProductsManagement.Application.Features.Products.Queries.GetProduct
{
    public class GetProductQuery : CommandBase<GetProductQuery>, IRequest<GetProductResponse>
    {
        public GetProductQuery(Guid id) : base(new GetProductQueryValidator())
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}
