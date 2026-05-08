using System;
using System.Collections.Generic;
using System.Text;

namespace TaskFlow.BuildingBlocks.Models
{
    public class AuthenticatedUser
    {
        public Guid UserId { get; set; }

        public string Email { get; set; } = string.Empty;

        public string Role { get; set; } = string.Empty;

        public Guid TenantId { get; set; }
    }
}
