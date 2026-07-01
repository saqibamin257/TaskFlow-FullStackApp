using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
        public async Task<ActionResult<CreateOrganizationResponse>> Create(CreateOrganizationCommand command,CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(command,cancellationToken);
            return Ok(response);

            //return CreatedAtAction(nameof(CreateOrganization),new { id = response.Id },response);
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<GetOrganizationsResponse>), StatusCodes.Status200OK)]        
        public async Task<ActionResult<List<GetOrganizationsResponse>>> Get(CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new GetOrganizationsQuery(),cancellationToken);
            return Ok(response);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<UpdateOrganizationResponse>> Put(Guid id, UpdateOrganizationCommand request) 
        {
            if (id != request.Id)
                return BadRequest(ErrorKeys.IdMismatched);
            var result = await _mediator.Send(request);
            return Ok(result);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id,CancellationToken cancellationToken)
        {
            await _mediator.Send(new DeactivateOrganizationCommand(id),cancellationToken);
            return NoContent();
        }
    }
}
