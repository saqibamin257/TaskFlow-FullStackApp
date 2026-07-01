using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace TaskFlow.Modules.Organizations.Application.Features.UpdateOrganization
{
    public sealed record UpdateOrganizationCommand(Guid Id,string Name, string Slug, string Description, string? LogoUrl):IRequest<UpdateOrganizationResponse>;    
}
