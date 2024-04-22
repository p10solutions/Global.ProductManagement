using AutoFixture;
using Global.ProductsManagement.Application.Features.Products.Queries.GetProduct;

namespace Global.ProductManagement.UnitTest.Application.Features.Products.Queries
{
    public class GetProductQueryUnitTest
    {
        readonly Fixture _fixture;

        public GetProductQueryUnitTest()
        {
            _fixture = new Fixture();
        }

        [Fact]
        public void Command_Should_Be_Valid()
        {
            var command = _fixture.Create<GetProductQuery>();

            var result = command.Validate();

            Assert.True(result);
        }

        [Fact]
        public void Command_Should_Be_Invalid()
        {
            var command = new GetProductQuery(Guid.Empty);

            var result = command.Validate();

            Assert.False(result);
        }
    }
}
