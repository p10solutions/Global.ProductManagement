using AutoFixture;
using Global.ProductsManagement.Application.Features.Products.Commands.UpdateProduct;
using Global.ProductsManagement.Domain.Entities;

namespace Global.ProductManagement.UnitTest.Application.Features.Products.Commands
{
    public class UpdateProductCommandUnitTest
    {
        readonly Fixture _fixture;

        public UpdateProductCommandUnitTest()
        {
            _fixture = new Fixture();
        }

        [Fact]
        public void Command_Should_Be_Valid()
        {
            var command = _fixture.Create<UpdateProductCommand>();

            var result = command.Validate();

            Assert.True(result);
        }

        [Fact]
        public void Command_Should_Be_Invalid()
        {
            Guid categoryId = Guid.Empty;
            Guid brandId = Guid.Empty;
            EProdutoStatus status = EProdutoStatus.Active;
            Guid id = Guid.Empty;

            var command = new UpdateProductCommand(
                id,
                "Widget 3000",
                "High-quality widget",
                19.99,
                categoryId,
                status,
                brandId
            );

            var result = command.Validate();

            Assert.False(result);
        }
    }
}
