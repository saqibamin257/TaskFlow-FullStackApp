using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskFlow.Modules.Organizations.Application.Features.CreateOrganization;

namespace TaskFlow.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrganizationController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrganizationController(
            IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [ProducesResponseType(typeof(CreateOrganizationResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]   
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<CreateOrganizationResponse>> CreateOrganization(CreateOrganizationCommand command,CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(command,cancellationToken);
            return Ok(response);

            //return CreatedAtAction(nameof(CreateOrganization),new { id = response.Id },response);
        }
    }
}
