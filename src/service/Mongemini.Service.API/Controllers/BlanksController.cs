using MediatR;
using Microsoft.AspNetCore.Mvc;
using Mongemini.Service.Application.Commands.Blanks;
using Mongemini.Service.Application.Queries.Blanks;
using Mongemini.Service.Application.ViewModels.Blanks;

namespace Mongemini.Service.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BlanksController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BlanksController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<BlankViewModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAutoWorkSettingPageAsync([FromQuery] GetBlanksRequest request, CancellationToken cancellationToken)
        {
            return Ok(await _mediator.Send(request, cancellationToken).ConfigureAwait(false));
        }

        [HttpPost]
        [ProducesResponseType(typeof(BlankViewModel), StatusCodes.Status200OK)]
        public async Task<IActionResult> CreateAutoWorkSettingAsync([FromBody] BlankViewModel blank, CancellationToken cancellationToken)
        {
            return Ok(await _mediator.Send(new CreateBlankCommand(blank), cancellationToken).ConfigureAwait(false));
        }
    }
}
