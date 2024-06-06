using IOXFleetServicesAPI.QueryCommands;
using IOXFleetServicesAPI.Shared.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace IOXFleetServicesAPI.Controllers
{
    [Route("v1/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class AccountController : ControllerBase
    {
        private readonly Serilog.ILogger _logger;
        private readonly IMediator _mediator;

        public AccountController(
            Serilog.ILogger logger,
            IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpPost]
        [Route("Deposit")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Deposit(DepositCommand command)
        {
            var resp = await _mediator.Send(command);
            return StatusCode(resp.MessageCode, resp);
        }

        [HttpPost]
        [Route("Quote")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Quote(QuoteCommand command)
        {
            var resp = await _mediator.Send(command);
            return StatusCode(resp.MessageCode, resp);
        }

        [HttpPost]
        [Route("RenewLicense")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> RenewLicense(RenewLicenseCommand command)
        {
            var resp = await _mediator.Send(command);
            return StatusCode(resp.MessageCode, resp);
        }

    }
}
