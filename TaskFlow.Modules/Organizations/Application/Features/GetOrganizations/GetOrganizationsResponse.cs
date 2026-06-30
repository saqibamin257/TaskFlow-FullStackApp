using System;
using System.Collections.Generic;
using System.Text;

namespace TaskFlow.Modules.Organizations.Application.Features.GetOrganization
{
    public sealed record GetOrganizationsResponse(
    Guid Id,
    string Name,
    string Slug,
    string Description,
    string? LogoUrl,
    Guid OwnerUserId,
    bool IsActive,
    DateTime CreatedAtUTC,
    DateTime? UpdatedAtUTC);
}
