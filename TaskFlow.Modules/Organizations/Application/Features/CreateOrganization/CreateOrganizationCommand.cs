using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using TaskFlow.Modules.Organizations.Application.Abstraction;

namespace TaskFlow.Modules.Organizations.Application.Features.CreateOrganization
{
    public sealed record CreateOrganizationCommand(string Name,string Slug,string Description, string? LogoUrl) : IRequest<CreateOrganizationResponse>;
    
}
