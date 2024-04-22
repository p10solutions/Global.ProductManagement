using Microsoft.Extensions.Logging;
using Moq;
using AutoFixture;
using Global.ProductsManagement.Domain.Models.Notifications;
using Global.ProductsManagement.Domain.Contracts.Notifications;
using Global.ProductsManagement.Domain.Contracts.Events;
using Global.ProductsManagement.Application.Features.Products.Commands.UpdateProduct;
using Global.ProductsManagement.Domain.Contracts.Data.Repositories;
using Global.ProductsManagement.Domain.Contracts.Data;
using AutoMapper;
using Global.ProductsManagement.Domain.Models.Events.Products;
using Global.ProductsManagement.Domain.Entities;

namespace Global.ProductManagement.UnitTest.Application.Features.Products.Commands
{
    public class UpdateProductHandlerUnitTest
    {
        readonly Mock<IProductRepository> _ProductRepository;
        readonly Mock<IProductProducer> _ProductProducer;
        readonly Mock<ILogger<UpdateProductHandler>> _logger;
        readonly Mock<INotificationsHandler> _notificationsHandler;
        readonly Mock<IUnitOfWork> _unitOfWork;
        readonly Fixture _fixture;
        readonly UpdateProductHandler _handler;

        public UpdateProductHandlerUnitTest()
        {
            _ProductRepository = new Mock<IProductRepository>();
            _ProductProducer = new Mock<IProductProducer>();
            _logger = new Mock<ILogger<UpdateProductHandler>>();
            _notificationsHandler = new Mock<INotificationsHandler>();
            _unitOfWork = new Mock<IUnitOfWork>();
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new UpdateProductMapper());
            });
            IMapper mapper = mappingConfig.CreateMapper();

            _fixture = new Fixture();
            _handler = new UpdateProductHandler(_ProductRepository.Object, _logger.Object, mapper,
                _notificationsHandler.Object, _unitOfWork.Object, _ProductProducer.Object);
        }

        [Fact]
        public async Task Product_Should_Be_Updated_Successfully_When_All_Information_Has_Been_Submitted()
        {
            var productCommand = _fixture.Create<UpdateProductCommand>();

            _ProductRepository.Setup(x => x.Exists(It.IsAny<string>(), It.IsAny<Guid>(), It.IsAny<Guid>(), It.IsAny<Guid>())).Returns(false);

            var response = await _handler.Handle(productCommand, CancellationToken.None);

            Assert.NotNull(response);
            _ProductRepository.Verify(x => x.Update(It.IsAny<Product>()), Times.Once);
            _ProductProducer.Verify(x => x.SendUpdatedEventAsync(It.IsAny<UpdatedProductEvent>()), Times.Once);
            _unitOfWork.Verify(x=>x.CommitAsync(), Times.Once);
            _ProductRepository.Verify(x => x.Exists(It.IsAny<string>(), It.IsAny<Guid>(), It.IsAny<Guid>(), It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        public async Task Product_Should_Not_Be_Updated_When_An_Exception_Was_Thrown()
        {
            var productCommand = _fixture.Create<UpdateProductCommand>();
            _ProductRepository.Setup(x => x.Update(It.IsAny<Product>())).Throws(new Exception());
            _ProductRepository.Setup(x => x.Exists(It.IsAny<string>(), It.IsAny<Guid>(), It.IsAny<Guid>(), It.IsAny<Guid>())).Returns(false);
            _notificationsHandler
                .Setup(x => x.AddNotification(It.IsAny<string>(), It.IsAny<ENotificationType>(), It.IsAny<object>()))
                    .Returns(_notificationsHandler.Object);
            _notificationsHandler.Setup(x => x.ReturnDefault<Guid>()).Returns(Guid.Empty);

            var response = await _handler.Handle(productCommand, CancellationToken.None);

            Assert.Null(response);

            _ProductRepository.Verify(x => x.Update(It.IsAny<Product>()), Times.Once);
            _ProductProducer.Verify(x => x.SendUpdatedEventAsync(It.IsAny<UpdatedProductEvent>()), Times.Never);
            _unitOfWork.Verify(x => x.CommitAsync(), Times.Never);
            _ProductRepository.Verify(x => x.Exists(It.IsAny<string>(), It.IsAny<Guid>(), It.IsAny<Guid>(), It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        public async Task Product_Should_Not_Be_Updated_When_Already_Exists()
        {
            var productCommand = _fixture.Create<UpdateProductCommand>();
            _ProductRepository.Setup(x => x.Update(It.IsAny<Product>())).Throws(new Exception());
            _ProductRepository.Setup(x => x.Exists(It.IsAny<string>(), It.IsAny<Guid>(), It.IsAny<Guid>(), It.IsAny<Guid>())).Returns(true);
            _notificationsHandler
                .Setup(x => x.AddNotification(It.IsAny<string>(), It.IsAny<ENotificationType>(), It.IsAny<object>()))
                    .Returns(_notificationsHandler.Object);
            _notificationsHandler.Setup(x => x.ReturnDefault<Guid>()).Returns(Guid.Empty);

            var response = await _handler.Handle(productCommand, CancellationToken.None);

            Assert.Null(response);

            _ProductRepository.Verify(x => x.Update(It.IsAny<Product>()), Times.Never);
            _ProductProducer.Verify(x => x.SendUpdatedEventAsync(It.IsAny<UpdatedProductEvent>()), Times.Never);
            _unitOfWork.Verify(x => x.CommitAsync(), Times.Never);
            _ProductRepository.Verify(x => x.Exists(It.IsAny<string>(), It.IsAny<Guid>(), It.IsAny<Guid>(), It.IsAny<Guid>()), Times.Once);
        }
    }
}