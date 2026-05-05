using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace TaskFlow.Modules.Users.Application.Features.CreateUser
{
    public class CreateUserQuery:IRequest<CreateUserResponse>
    {
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Role { get; set; } = "Member";
    }
}
