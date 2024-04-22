using AutoMapper;
using Global.ProductsManagement.Domain.Contracts.Cache;
using Global.ProductsManagement.Domain.Contracts.Data.Repositories;
using Global.ProductsManagement.Domain.Contracts.Notifications;
using Global.ProductsManagement.Domain.Models.Notifications;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Global.ProductsManagement.Application.Features.Products.Queries.GetProduct
{
    public class GetProductHandler : IRequestHandler<GetProductQuery, GetProductResponse>
    {
        readonly IProductRepository _ProductRepository;
        readonly ILogger<GetProductHandler> _logger;
        readonly INotificationsHandler _notificationsHandler;
        readonly IMapper _mapper;
        readonly IProductCache _productCache;

        public GetProductHandler(IProductRepository ProductRepository, ILogger<GetProductHandler> logger,
            INotificationsHandler notificationsHandler, IMapper mapper, IProductCache productCache)
        {
            _ProductRepository = ProductRepository;
            _logger = logger;
            _notificationsHandler = notificationsHandler;
            _mapper = mapper;
            _productCache = productCache;
        }

        public async Task<GetProductResponse> Handle(GetProductQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var product = await _productCache.GetAsync(request.Id);

                if (product == null)
                {
                    product = await _ProductRepository.GetAsync(request.Id);

                    if (product == null)
                    {
                        _logger.LogWarning("Product: {ProductId} not found", request.Id);

                        return _notificationsHandler
                            .AddNotification("Product not found", ENotificationType.NotFound)
                            .ReturnDefault<GetProductResponse>();
                    }

                    await _productCache.AddAsync(product);
                }

                var response = _mapper.Map<GetProductResponse>(product);

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred when trying to get the Product: {exception}", ex.Message);

                return _notificationsHandler
                        .AddNotification("An error occurred when trying to get the Product", ENotificationType.InternalError)
                        .ReturnDefault<GetProductResponse>();
            }
        }
    }
}
