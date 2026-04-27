using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace TaskFlow.Modules.Users.Application.Features.GetUsers
{
    public record GetUsersQuery() : IRequest<List<GetUsersResponse>>;    
}
