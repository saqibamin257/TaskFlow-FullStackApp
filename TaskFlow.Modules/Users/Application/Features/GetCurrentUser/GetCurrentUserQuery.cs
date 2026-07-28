using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace TaskFlow.Modules.Users.Application.Features.GetCurrentUser
{
    public sealed record GetCurrentUserQuery : IRequest<GetCurrentUserResponse>;    
}
