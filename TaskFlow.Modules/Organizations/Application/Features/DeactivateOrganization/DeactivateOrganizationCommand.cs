using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace TaskFlow.Modules.Organizations.Application.Features.DeactivateOrganization
{
    public sealed record DeactivateOrganizationCommand(Guid Id): IRequest;
}
