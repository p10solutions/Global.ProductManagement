using AutoMapper;
using Global.ProductsManagement.Domain.Contracts.Data;
using Global.ProductsManagement.Domain.Contracts.Data.Repositories;
using Global.ProductsManagement.Domain.Contracts.Events;
using Global.ProductsManagement.Domain.Contracts.Notifications;
using Global.ProductsManagement.Domain.Entities;
using Global.ProductsManagement.Domain.Models.Events.Products;
using Global.ProductsManagement.Domain.Models.Notifications;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Global.ProductsManagement.Application.Features.Products.Commands.CreateProduct
{
    public class CreateProductHandler : IRequestHandler<CreateProductCommand, CreateProductResponse>
    {
        readonly IProductRepository _productRepository;
        readonly ILogger<CreateProductHandler> _logger;
        readonly IMapper _mapper;
        readonly INotificationsHandler _notificationsHandler;
        readonly IUnitOfWork _unitOfWork;
        readonly IProductProducer _productProducer;

        public CreateProductHandler(IProductRepository productRepository, ILogger<CreateProductHandler> logger,
            IMapper mapper, INotificationsHandler notificationsHandler, IUnitOfWork unitOfWork, IProductProducer productProducer)
        {
            _productRepository = productRepository;
            _logger = logger;
            _mapper = mapper;
            _notificationsHandler = notificationsHandler;
            _unitOfWork = unitOfWork;
            _productProducer = productProducer;
        }

        public async Task<CreateProductResponse> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var product = _mapper.Map<Product>(request);

            try
            {
                if (_productRepository.Exists(product.Name, product.CategoryId, product.BrandId, product.Id))
                {
                    _logger.LogWarning("There is already a product with that name: {ProductName}", request.Name);

                    return _notificationsHandler
                        .AddNotification("There is already a product with that name", ENotificationType.BusinessValidation)
                        .ReturnDefault<CreateProductResponse>();
                }

                await _productRepository.AddAsync(product);
                await _unitOfWork.CommitAsync();

                var @event = _mapper.Map<CreatedProductEvent>(product);

                await _productProducer.SendCreatedEventAsync(@event);

                var response = _mapper.Map<CreateProductResponse>(product);

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred when trying to insert the product: {Exception}", ex.Message);

               return _notificationsHandler
                    .AddNotification("An error occurred when trying to insert the product", ENotificationType.InternalError)
                    .ReturnDefault<CreateProductResponse>();
            }
        }
    }
}
