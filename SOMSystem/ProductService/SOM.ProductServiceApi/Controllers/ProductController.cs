using MediatR;
using Microsoft.AspNetCore.Mvc;
using SOM.ProductService.Application.Products.Commands;
using SOM.ProductService.Application.Products.Queries;
using SOM.ProductService.Domain.Product;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SOM.ProductServiceApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ProductController(IMediator mediator)
        {
            _mediator= mediator;
        }

        [HttpGet("GetProducts", Name = "GetProducts")]
        public async Task<ActionResult<List<Product>>> GetProducts()
        {
            var results = await _mediator.Send(new GetProductQuery());
            return Ok(results);
        }

        [HttpPost("CreateProduct", Name = "CreateProduct")]
        public async Task<ActionResult<int>> CreateProduct(CreateProductCommand command)
        {
            var result = await _mediator.Send(command);

            return Ok(result);
        }
    }
}
