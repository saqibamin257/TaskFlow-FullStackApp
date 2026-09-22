using System;
using System.Collections.Generic;
using System.Text;

namespace TaskFlow.BuildingBlocks.Models
{
    public class AuthenticatedUser
    {
        public Guid UserId { get; set; }

        public string Email { get; set; } = string.Empty;        
    }
}
