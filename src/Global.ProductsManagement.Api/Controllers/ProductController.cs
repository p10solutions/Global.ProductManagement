using Global.ProductsManagement.Api.Controllers.Base;
using Global.ProductsManagement.Application.Features.Products.Commands.CreateProduct;
using Global.ProductsManagement.Application.Features.Products.Commands.UpdateProduct;
using Global.ProductsManagement.Application.Features.Products.Queries.GetProduct;
using Global.ProductsManagement.Domain.Contracts.Notifications;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Global.ProductsManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController(IMediator mediator, INotificationsHandler notifications) : ApiControllerBase(mediator, notifications)
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetProductResponse))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAsync(Guid id)
            => await SendAsync(new GetProductQuery(id));

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(CreateProductResponse))]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PostAsync(CreateProductCommand createProductCommand)
            => await SendAsync(createProductCommand, HttpStatusCode.Created);

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UpdateProductResponse))]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task PutAsync(Guid id, UpdateProductCommand updateProductCommand)
            => await SendAsync(updateProductCommand);
    }
}
