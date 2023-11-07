using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace SOM.Shared.Common
{
    [Route("api/[controller]")]
    [ApiController]
    public abstract class ApiControllerBase: ControllerBase
    {
        protected readonly IMediator _mediator;
        public ApiControllerBase(IMediator mediator)
        {
            _mediator = mediator;
        }
    }
}
