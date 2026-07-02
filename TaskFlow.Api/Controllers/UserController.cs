using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskFlow.BuildingBlocks.Localization;
using TaskFlow.BuildingBlocks.Security.Abstraction;
using TaskFlow.Modules.Users.Application.Features.CreateUser;
using TaskFlow.Modules.Users.Application.Features.DeleteUser;
using TaskFlow.Modules.Users.Application.Features.GetUsers;
using TaskFlow.Modules.Users.Application.Features.UpdateUser;

namespace TaskFlow.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public sealed class UserController : ControllerBase
    {
        private readonly IMediator _mediator;
        public UserController(IMediator mediator)
        {
            _mediator = mediator;            
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<GetUsersResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<List<GetUsersResponse>>> Get(CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetUsersQuery(), cancellationToken);
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(typeof(CreateUserResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<CreateUserResponse>> Post(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(request, cancellationToken);
            return StatusCode(StatusCodes.Status201Created, result);           
        }

        [HttpPut("{id:guid}")]
        [ProducesResponseType(typeof(UpdateUserResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)] //implement later
        [ProducesResponseType(StatusCodes.Status404NotFound)]  //implement later
        public async Task<ActionResult<UpdateUserResponse>> Put(Guid id, UpdateUserCommand request, CancellationToken cancellationToken) 
        {
            if (id != request.Id)
                return BadRequest(ErrorKeys.IdMismatched);
            var result = await _mediator.Send(request, cancellationToken);
            return Ok(result);
        }

        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)] //implement later
        [ProducesResponseType(StatusCodes.Status404NotFound)] //implement later
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken) 
        {
            await _mediator.Send(new DeleteUserCommand { Id = id }, cancellationToken);
            return NoContent();
        }
    }
}
