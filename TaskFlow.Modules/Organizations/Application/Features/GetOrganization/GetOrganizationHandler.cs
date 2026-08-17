using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using TaskFlow.BuildingBlocks.Localization;
using TaskFlow.BuildingBlocks.Security.Abstraction;
using TaskFlow.BuildingBlocks.Security.Services;
using TaskFlow.Modules.Organizations.Application.Abstraction;
using TaskFlow.Modules.Organizations.Infrastructure.Repositories;

namespace TaskFlow.Modules.Organizations.Application.Features.GetOrganization
{
    public sealed class GetOrganizationHandler: IRequestHandler<GetOrganizationQuery,GetOrganizationResponse>
    {
        private readonly IOrganizationRepository _organizationRepository;
        private readonly ICurrentUser _currentUser;

        public GetOrganizationHandler(IOrganizationRepository organizationRepository, ICurrentUser currentUser)
        {
            _organizationRepository = organizationRepository;
            _currentUser = currentUser;
        }
        public async Task<GetOrganizationResponse> Handle(GetOrganizationQuery request,CancellationToken cancellationToken)
        {
            var organization = await _organizationRepository.GetByIdAsync(
                request.Id,
                cancellationToken);

            if (organization is null)
            {
                throw new ValidationException(ErrorKeys.OrganizationNotFound);
            }

            // Only the owner can access this endpoint (for now)
            if (organization.OwnerUserId != _currentUser.UserId)
            {
                throw new ValidationException(ErrorKeys.OrganizationAccessDenied);
            }

            return new GetOrganizationResponse(
                organization.Id,
                organization.Name,
                organization.Slug,
                organization.Description,
                organization.LogoUrl,
                organization.OwnerUserId,
                organization.IsActive,
                organization.CreatedAtUTC,
                organization.UpdatedAtUTC);
        }

    }
}
