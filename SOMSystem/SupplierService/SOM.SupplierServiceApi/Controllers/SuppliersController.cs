using MediatR;
using Microsoft.AspNetCore.Mvc;
using SOM.Shared.Entities;
using SOM.SupplierService.Application.Supplier.Commands;
using SOM.SupplierService.Application.Supplier.Queries;
using SOM.SupplierService.Domain.Supplier;

namespace SOM.SupplierServiceApi.Controllers;


public class SuppliersController : ApiControllerBase
{
    private readonly ILogger<SuppliersController> _logger;
    public SuppliersController(IMediator mediator, ILogger<SuppliersController> logger) : base(mediator)
    {
        _logger = logger;
    }

    [HttpGet("", Name = "GetSuppliers")]
    public async Task<ActionResult<List<Supplier>>> GetSupplier()
    {
        _logger.LogInformation("Logging Get Suppliers ");
        var results = await _mediator.Send(new GetSupplierQuery());
        return Ok(results);
    }

    [HttpPost("", Name = "CreateSupplier")]
    public async Task<ActionResult<int>> CreateSupplier(CreateSupplierCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(result);
    }
}

