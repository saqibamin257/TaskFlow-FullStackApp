using System;
using System.Collections.Generic;
using System.Text;

namespace TaskFlow.BuildingBlocks.Security.Abstraction
{
    public interface ICurrentUser
    {
        Guid UserId { get; }

        string Email { get; }

        string Role { get; }

        Guid TenantId { get; }

        bool IsAuthenticated { get; }
    }
}
