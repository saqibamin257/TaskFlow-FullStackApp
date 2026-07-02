using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using TaskFlow.BuildingBlocks.Localization;
using TaskFlow.Modules.Organizations.Application.Features.CreateOrganization;
using TaskFlow.Modules.Organizations.Application.Features.DeactivateOrganization;
using TaskFlow.Modules.Organizations.Application.Features.GetOrganization;
using TaskFlow.Modules.Organizations.Application.Features.UpdateOrganization;

namespace TaskFlow.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public sealed class OrganizationController : ControllerBase
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
        public async Task<ActionResult<CreateOrganizationResponse>> Post(CreateOrganizationCommand command,CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(command, cancellationToken);
            return StatusCode(StatusCodes.Status201Created, result);
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<GetOrganizationsResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<List<GetOrganizationsResponse>>> Get(CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetOrganizationsQuery(),cancellationToken);
            return Ok(result);
        }

       
        [HttpPut("{id:guid}")]
        [ProducesResponseType(typeof(UpdateOrganizationResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<UpdateOrganizationResponse>> Put(Guid id, UpdateOrganizationCommand request, CancellationToken cancellationToken) 
        {
            if (id != request.Id)
                return BadRequest(ErrorKeys.IdMismatched);
            var result = await _mediator.Send(request,cancellationToken);
            return Ok(result);
        }

        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(Guid id,CancellationToken cancellationToken)
        {
            await _mediator.Send(new DeactivateOrganizationCommand(id),cancellationToken);
            return NoContent();
        }
    }
}
