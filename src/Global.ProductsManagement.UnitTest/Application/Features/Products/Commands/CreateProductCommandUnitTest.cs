using AutoFixture;
using Global.ProductsManagement.Application.Features.Products.Commands.CreateProduct;
using Global.ProductsManagement.Domain.Entities;

namespace Global.ProductManagement.UnitTest.Application.Features.Products.Commands
{
    public class CreateProductCommandUnitTest
    {
        readonly Fixture _fixture;

        public CreateProductCommandUnitTest()
        {
            _fixture = new Fixture();
        }

        [Fact]
        public void Command_Should_Be_Valid()
        {
            var command = _fixture.Create<CreateProductCommand>();

            var result = command.Validate();

            Assert.True(result);
        }

        [Fact]
        public void Command_Should_Be_Invalid()
        {
            Guid categoryId = Guid.Empty; 
            Guid brandId = Guid.Empty;   
            EProdutoStatus status = EProdutoStatus.Active; 

            var command = new CreateProductCommand(
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
