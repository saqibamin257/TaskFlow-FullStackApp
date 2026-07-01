using System;
using System.Collections.Generic;
using System.Text;

namespace TaskFlow.Modules.Organizations.Application.Features.CreateOrganization
{
    public sealed record CreateOrganizationResponse(Guid Id,
                                                   string Name,
                                                   string Slug,
                                                   string Description,
                                                   string?LogoUrl,
                                                   Guid OwnerUserId, 
                                                   bool isActive,
                                                   DateTime CreateAtUTC
                                                   );    
}
