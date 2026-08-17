using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace TaskFlow.Modules.Organizations.Application.Features.GetOrganization
{
    public sealed record GetOrganizationQuery(Guid Id) : IRequest<GetOrganizationResponse>;
    
}
