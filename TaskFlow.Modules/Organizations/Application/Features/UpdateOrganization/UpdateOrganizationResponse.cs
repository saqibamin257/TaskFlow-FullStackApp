using System;
using System.Collections.Generic;
using System.Text;

namespace TaskFlow.Modules.Organizations.Application.Features.UpdateOrganization
{
    public sealed record UpdateOrganizationResponse(
                                                    Guid Id,
                                                    string Name,
                                                    string Slug,
                                                    string Description,
                                                    string? LogoUrl,
                                                    Guid OwnerUserId,
                                                    bool IsActive,
                                                    DateTime CreatedAtUTC,
                                                    DateTime? UpdatedAtUTC
                                                    );
}
