using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace TaskFlow.Modules.Users.Application.Features.Auth.Register
{
    public sealed record RegisterCommand(string FirstName,string LastName,string Email,string Password):IRequest<RegisterResponse>;
    
}
