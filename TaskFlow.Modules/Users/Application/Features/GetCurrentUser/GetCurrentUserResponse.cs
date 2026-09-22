using System;
using System.Collections.Generic;
using System.Text;

namespace TaskFlow.Modules.Users.Application.Features.GetCurrentUser
{
    public sealed record GetCurrentUserResponse(
        Guid Id,
        string FirstName,
        string LastName,
        string Email                         
    );
}
