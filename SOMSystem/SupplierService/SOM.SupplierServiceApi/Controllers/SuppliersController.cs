using MediatR;
using Microsoft.AspNetCore.Mvc;
using SOM.SupplierService.Application.Products.Commands;
using SOM.SupplierService.Application.Products.Queries;
using SOM.SupplierService.Domain.Product;
using SOM.Shared.Entities;

namespace SOM.SupplierServiceApi.Controllers;


public class SuppliersController : ApiControllerBase
{
    private readonly ILogger<SuppliersController> _logger;
    public SuppliersController(IMediator mediator, ILogger<SuppliersController> logger) : base(mediator)
    {
        _logger = logger;
    }

    [HttpGet("", Name = "GetProducts")]
    public async Task<ActionResult<List<Supplier>>> GetProducts()
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
}

