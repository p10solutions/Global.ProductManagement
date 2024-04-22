using Global.ProductsManagement.Application.Features.Products.Commands.CreateProduct;
using Global.ProductsManagement.Application.Features.Products.Commands.UpdateProduct;
using Global.ProductsManagement.Application.Features.Products.Queries.GetProduct;
using Global.ProductsManagement.Domain.Contracts.Cache;
using Global.ProductsManagement.Domain.Contracts.Data;
using Global.ProductsManagement.Domain.Contracts.Data.Repositories;
using Global.ProductsManagement.Domain.Contracts.Events;
using Global.ProductsManagement.Domain.Contracts.Notifications;
using Global.ProductsManagement.Infraestructure.Cache;
using Global.ProductsManagement.Infraestructure.Data;
using Global.ProductsManagement.Infraestructure.Data.Repositories;
using Global.ProductsManagement.Infraestructure.Events.Products;
using Global.ProductsManagement.Infraestructure.Validation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Global.ProductsManagement.Infraestructure.IoC
{
    public static class ProductManagementProvider
    {
        public static IServiceCollection AddProviders(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies()));

            var connectionString = configuration.GetConnectionString("ProductManagement");


            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = configuration.GetConnectionString("Redis");
            });
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(FailFastValidator<,>));
            services.AddScoped<INotificationsHandler, NotificationHandler>();
            services.AddDbContextPool<ProductManagementContext>(opt => opt.UseSqlServer(connectionString));
            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<IProductProducer, ProductProducer>();
            services.AddTransient<IProductCache, ProductCache>();
            services.AddTransient<IProductProducer, ProductProducer>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddAutoMapper(typeof(CreateProductMapper));
            services.AddAutoMapper(typeof(UpdateProductMapper));
            services.AddAutoMapper(typeof(GetProductMapper));

            return services;
        }
    }
}
