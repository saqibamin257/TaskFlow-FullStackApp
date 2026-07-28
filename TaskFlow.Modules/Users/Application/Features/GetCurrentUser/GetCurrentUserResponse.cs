using System;
using System.Collections.Generic;
using System.Text;

namespace TaskFlow.Modules.Users.Application.Features.GetCurrentUser
{
    public sealed record GetCurrentUserResponse(
        Guid Id,
        string Name,
        string Email,
        string Role,
        Guid TeenantId           
    );
}
