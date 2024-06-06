using IOXFleetServicesAPI.QueryCommands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace IOXFleetServicesAPI.Controllers
{
    [Route("v1/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class VehicleController : ControllerBase
    {
        private readonly Serilog.ILogger _logger;
        private readonly IMediator _mediator;

        public VehicleController(
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
        public async Task<IActionResult> AddVehicle(AddVehicleCommand command)
        {
            var resp = await _mediator.Send(command);
            return StatusCode(resp.MessageCode, resp);
        }


        [HttpPost]
        [Route("GetVehicleList")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetVehicleList(GetVehicleListCommand command)
        {
            var resp = await _mediator.Send(command);
            return StatusCode(resp.MessageCode, resp);
        }

    }
}
