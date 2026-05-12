using System;
using System.Collections.Generic;
using System.Text;

namespace TaskFlow.Modules.Users.Application.Features.CreateUser
{
    public class CreateUserResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Role { get;  set; } = string.Empty;        
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAtUTC { get; set; }   
        
    }
}
