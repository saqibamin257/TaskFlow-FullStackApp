using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ICurrentUser _currentUser;

        public UserController(IMediator mediator, ICurrentUser currentUser)
        {
            _mediator = mediator;
            _currentUser = currentUser;
        }

       
        [HttpGet("me")]
        public IActionResult Me()
        {
            return Ok(new
            {
                _currentUser.UserId,
                _currentUser.Email,
                _currentUser.Role,
                _currentUser.TenantId,
                _currentUser.IsAuthenticated
            });
        }
        
        [HttpGet]
        public async Task<ActionResult<List<GetUsersResponse>>> Get()
        {
            var result = await _mediator.Send(new GetUsersQuery());
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<CreateUserResponse>> Post(CreateUserCommand request)
        {
            var result = await _mediator.Send(request);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<UpdateUserResponse>> Put(Guid id, UpdateUserCommand request) 
        {
            if (id != request.Id)
                return BadRequest("Id mismatch");
            var result = await _mediator.Send(request);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> Delete(Guid id) 
        {
            var result = await _mediator.Send(new DeleteUserCommand { Id = id });
            return Ok(result);
        }
    }
}
