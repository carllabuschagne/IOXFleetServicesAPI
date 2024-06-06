using IOXFleetServicesAPI.QueryCommands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace IOXFleetServicesAPI.Controllers
{
    [Route("v1/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class UserController : ControllerBase
    {
        private readonly Serilog.ILogger _logger;
        private readonly IMediator _mediator;

        public UserController(
            Serilog.ILogger logger,
            IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateUser(CreateUserCommand command)
        {
            var resp = await _mediator.Send(command);
            return StatusCode(resp.MessageCode, resp);
        }

    }
}
