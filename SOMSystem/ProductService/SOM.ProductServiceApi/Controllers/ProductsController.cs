using MediatR;
using Microsoft.AspNetCore.Mvc;
using SOM.ProductService.Application.Products.Commands;
using SOM.ProductService.Application.Products.Queries;
using SOM.ProductService.Domain.Product;
using SOM.Shared.Common;

namespace SOM.ProductServiceApi.Controllers;


public class ProductsController : ApiControllerBase
{
    public ProductsController(IMediator mediator) : base(mediator)
    {
    }

    [HttpGet("", Name = "GetProducts")]
    public async Task<ActionResult<List<Product>>> GetProducts()
    {
        var results = await _mediator.Send(new GetProductQuery());
        return Ok(results);
    }

    [HttpPost("", Name = "CreateProduct")]
    public async Task<ActionResult<int>> CreateProduct(CreateProductCommand command)
    {
        var result = await _mediator.Send(command);

        return Ok(result);
    }
}

