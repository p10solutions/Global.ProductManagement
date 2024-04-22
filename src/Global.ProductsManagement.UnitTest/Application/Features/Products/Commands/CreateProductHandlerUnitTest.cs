using Microsoft.Extensions.Logging;
using Moq;
using AutoFixture;
using Global.ProductsManagement.Domain.Models.Notifications;
using Global.ProductsManagement.Domain.Contracts.Notifications;
using Global.ProductsManagement.Domain.Contracts.Events;
using Global.ProductsManagement.Application.Features.Products.Commands.CreateProduct;
using Global.ProductsManagement.Domain.Contracts.Data.Repositories;
using Global.ProductsManagement.Domain.Contracts.Data;
using AutoMapper;
using Global.ProductsManagement.Domain.Models.Events.Products;
using Global.ProductsManagement.Domain.Entities;

namespace Global.ProductManagement.UnitTest.Application.Features.Products.Commands
{
    public class CreateProductHandlerUnitTest
    {
        readonly Mock<IProductRepository> _ProductRepository;
        readonly Mock<IProductProducer> _ProductProducer;
        readonly Mock<ILogger<CreateProductHandler>> _logger;
        readonly Mock<INotificationsHandler> _notificationsHandler;
        readonly Mock<IUnitOfWork> _unitOfWork;
        readonly Fixture _fixture;
        readonly CreateProductHandler _handler;

        public CreateProductHandlerUnitTest()
        {
            _ProductRepository = new Mock<IProductRepository>();
            _ProductProducer = new Mock<IProductProducer>();
            _logger = new Mock<ILogger<CreateProductHandler>>();
            _notificationsHandler = new Mock<INotificationsHandler>();
            _unitOfWork = new Mock<IUnitOfWork>();
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new CreateProductMapper());
            });
            IMapper mapper = mappingConfig.CreateMapper();

            _fixture = new Fixture();
            _handler = new CreateProductHandler(_ProductRepository.Object, _logger.Object, mapper, 
                _notificationsHandler.Object, _unitOfWork.Object, _ProductProducer.Object);
        }

        [Fact]
        public async Task Product_Should_Be_Created_Successfully_When_All_Information_Has_Been_Submitted()
        {
            var productCommand = _fixture.Create<CreateProductCommand>();

            _ProductRepository.Setup(x => x.Exists(It.IsAny<string>(), It.IsAny<Guid>(), It.IsAny<Guid>(), It.IsAny<Guid>())).Returns(false);

            var response = await _handler.Handle(productCommand, CancellationToken.None);

            Assert.NotNull(response);
            _ProductRepository.Verify(x => x.AddAsync(It.IsAny<Product>()), Times.Once);
            _ProductProducer.Verify(x => x.SendCreatedEventAsync(It.IsAny<CreatedProductEvent>()), Times.Once);
            _unitOfWork.Verify(x => x.CommitAsync(), Times.Once);
            _ProductRepository.Verify(x => x.Exists(It.IsAny<string>(), It.IsAny<Guid>(), It.IsAny<Guid>(), It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        public async Task Product_Should_Not_Be_Created_When_An_Exception_Was_Thrown()
        {
            var productCommand = _fixture.Create<CreateProductCommand>();

            _ProductRepository.Setup(x => x.Exists(It.IsAny<string>(), It.IsAny<Guid>(), It.IsAny<Guid>(), It.IsAny<Guid>())).Returns(false);
            _ProductRepository.Setup(x => x.AddAsync(It.IsAny<Product>())).Throws(new Exception());
            _notificationsHandler
                .Setup(x => x.AddNotification(It.IsAny<string>(), It.IsAny<ENotificationType>(), It.IsAny<object>()))
                    .Returns(_notificationsHandler.Object);
            _notificationsHandler.Setup(x => x.ReturnDefault<Guid>()).Returns(Guid.Empty);

            var response = await _handler.Handle(productCommand, CancellationToken.None);

            Assert.Null(response);

            _ProductRepository.Verify(x => x.AddAsync(It.IsAny<Product>()), Times.Once);
            _ProductProducer.Verify(x => x.SendCreatedEventAsync(It.IsAny<CreatedProductEvent>()), Times.Never);
            _unitOfWork.Verify(x => x.CommitAsync(), Times.Never);
            _ProductRepository.Verify(x => x.Exists(It.IsAny<string>(), It.IsAny<Guid>(), It.IsAny<Guid>(), It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        public async Task Product_Should_Not_Be_Created_When_Already_Exists()
        {
            var productCommand = _fixture.Create<CreateProductCommand>();

            _ProductRepository.Setup(x => x.Exists(It.IsAny<string>(), It.IsAny<Guid>(), It.IsAny<Guid>(), It.IsAny<Guid>())).Returns(true);
            _ProductRepository.Setup(x => x.AddAsync(It.IsAny<Product>())).Throws(new Exception());
            _notificationsHandler
                .Setup(x => x.AddNotification(It.IsAny<string>(), It.IsAny<ENotificationType>(), It.IsAny<object>()))
                    .Returns(_notificationsHandler.Object);
            _notificationsHandler.Setup(x => x.ReturnDefault<Guid>()).Returns(Guid.Empty);

            var response = await _handler.Handle(productCommand, CancellationToken.None);

            Assert.Null(response);

            _ProductRepository.Verify(x => x.AddAsync(It.IsAny<Product>()), Times.Never);
            _ProductProducer.Verify(x => x.SendCreatedEventAsync(It.IsAny<CreatedProductEvent>()), Times.Never);
            _unitOfWork.Verify(x => x.CommitAsync(), Times.Never);
            _ProductRepository.Verify(x => x.Exists(It.IsAny<string>(), It.IsAny<Guid>(), It.IsAny<Guid>(), It.IsAny<Guid>()), Times.Once);
        }
    }
}