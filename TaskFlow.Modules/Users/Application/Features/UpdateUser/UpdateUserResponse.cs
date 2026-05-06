using System;
using System.Collections.Generic;
using System.Text;

namespace TaskFlow.Modules.Users.Application.Features.UpdateUser
{
    public class UpdateUserResponse
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
    }
}
