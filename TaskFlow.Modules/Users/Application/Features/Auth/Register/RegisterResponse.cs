using System;
using System.Collections.Generic;
using System.Text;

namespace TaskFlow.Modules.Users.Application.Features.Auth.Register
{
    public sealed record RegisterResponse(Guid Id,string FirstName,string? LastName,string Email);
    
}
