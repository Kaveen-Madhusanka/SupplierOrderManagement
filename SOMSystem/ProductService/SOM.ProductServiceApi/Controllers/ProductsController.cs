using MediatR;
using Microsoft.AspNetCore.Mvc;
using SOM.ProductService.Application.Products.Commands;
using SOM.ProductService.Application.Products.Queries;
using SOM.ProductService.Domain.Product;
using SOM.Shared.Entities;

namespace SOM.ProductServiceApi.Controllers;


public class ProductsController : ApiControllerBase
{
    private readonly ILogger<ProductsController> _logger;
    public ProductsController(IMediator mediator, ILogger<ProductsController> logger) : base(mediator)
    {
        _logger = logger;
    }

    [HttpGet("", Name = "GetProducts")]
    public async Task<ActionResult<List<Product>>> GetProducts()
    {
        _logger.LogInformation("Logging GetProducts ");
        var results = await _mediator.Send(new GetProductQuery());
        return Ok(results);
    }

    [HttpPost("", Name = "CreateProduct")]
    public async Task<ActionResult<int>> CreateProduct(CreateProductCommand command)
    {
        var result = await _mediator.Send(command);

        return Ok(result);
    }

    [HttpGet("{id}", Name = "GetProduct")]
    public async Task<ActionResult<List<Product>>> GetProduct(int id)
    {

        return Ok();
    }
}

