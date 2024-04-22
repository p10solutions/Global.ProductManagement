using AutoFixture;
using AutoMapper;
using Global.ProductsManagement.Application.Features.Products.Queries.GetProduct;
using Global.ProductsManagement.Domain.Contracts.Cache;
using Global.ProductsManagement.Domain.Contracts.Data.Repositories;
using Global.ProductsManagement.Domain.Contracts.Notifications;
using Global.ProductsManagement.Domain.Entities;
using Global.ProductsManagement.Domain.Models.Notifications;
using Microsoft.Extensions.Logging;
using Moq;

namespace Global.ProductManagement.UnitTest.Application.Features.Products.Queries
{
    public class GetProductUnitTest
    {
        readonly Mock<IProductRepository> _ProductRepository;
        readonly Mock<ILogger<GetProductHandler>> _logger;
        readonly Mock<INotificationsHandler> _notificationsHandler;
        readonly Fixture _fixture;
        readonly GetProductHandler _handler;
        readonly Mock<IProductCache> _ProductCache;

        public GetProductUnitTest()
        {
            _ProductRepository = new Mock<IProductRepository>();
            _logger = new Mock<ILogger<GetProductHandler>>();
            _notificationsHandler = new Mock<INotificationsHandler>();
            _fixture = new Fixture();
            _ProductCache = new Mock<IProductCache>();

            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new GetProductMapper());
            });
            IMapper mapper = mappingConfig.CreateMapper();

            _handler = new GetProductHandler(_ProductRepository.Object, _logger.Object, _notificationsHandler.Object, mapper, _ProductCache.Object);
        }

        [Fact]
        public async Task Product_Should_Be_Geted_Successfully_When_All_Information_Has_Been_Submitted()
        {
            var productQuery = _fixture.Create<GetProductQuery>();
            var product = _fixture.Create<Product>();

            _ProductCache.Setup(x => x.GetAsync(It.IsAny<Guid>())).ReturnsAsync(product);

            var response = await _handler.Handle(productQuery, CancellationToken.None);

            Assert.NotNull(response);
            _ProductCache.Verify(x => x.GetAsync(It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        public async Task Product_Should_Be_Geted_From_Repository()
        {
            var productQuery = _fixture.Create<GetProductQuery>();
            var product = _fixture.Create<Product>();
            _ProductRepository.Setup(x => x.GetAsync(It.IsAny<Guid>())).ReturnsAsync(product);

            var response = await _handler.Handle(productQuery, CancellationToken.None);

            Assert.NotNull(response);
            _ProductRepository.Verify(x => x.GetAsync(It.IsAny<Guid>()), Times.Once);
            _ProductCache.Verify(x => x.GetAsync(It.IsAny<Guid>()), Times.Once);
            _ProductCache.Verify(x => x.AddAsync(It.IsAny<Product>()), Times.Once);
        }

        [Fact]
        public async Task Product_Should_Not_Be_Geted_When_An_Exception_Was_Thrown()
        {
            var productQuery = _fixture.Create<GetProductQuery>();
            _ProductRepository.Setup(x => x.GetAsync(It.IsAny<Guid>())).Throws(new Exception());
            _notificationsHandler
                .Setup(x => x.AddNotification(It.IsAny<string>(), It.IsAny<ENotificationType>(), It.IsAny<object>()))
                    .Returns(_notificationsHandler.Object);
            _notificationsHandler.Setup(x => x.ReturnDefault<Guid>()).Returns(Guid.Empty);

            var response = await _handler.Handle(productQuery, CancellationToken.None);

            Assert.Null(response);
            _ProductRepository.Verify(x => x.GetAsync(It.IsAny<Guid>()), Times.Once);
            _ProductCache.Verify(x => x.GetAsync(It.IsAny<Guid>()), Times.Once);
        }
    }
}