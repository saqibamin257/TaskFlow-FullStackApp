using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace TaskFlow.Modules.Users.Application.Features.DeleteUser
{
    public class DeleteUserCommand:IRequest<bool>
    {
        public Guid Id { get; set; }
    }
}
