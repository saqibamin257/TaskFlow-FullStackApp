using FluentValidation;
using MediatR;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Text;
using TaskFlow.BuildingBlocks.Localization;
using TaskFlow.BuildingBlocks.Security.Abstraction;
using TaskFlow.Modules.Organizations.Application.Abstraction;
using TaskFlow.Modules.Organizations.Domain.Entities;

namespace TaskFlow.Modules.Organizations.Application.Features.UpdateOrganization
{
    public class UpdateOrganizationHandler : IRequestHandler<UpdateOrganizationCommand, UpdateOrganizationResponse>
    {
        private readonly IOrganizationRepository _organizationRepository;
        private readonly ICurrentUser _currentUser;

        public UpdateOrganizationHandler(IOrganizationRepository organizationRepository, ICurrentUser currentUser) 
        {
            _organizationRepository = organizationRepository;
            _currentUser = currentUser;
        }
        public async Task<UpdateOrganizationResponse> Handle(UpdateOrganizationCommand request, CancellationToken cancellationToken)
        {
            //get organization
            var organization = await _organizationRepository.GetByIdAsync(request.Id, cancellationToken);
            if (organization is null)
            {
                throw new ValidationException(ErrorKeys.OrganizationNotFound);
            }

            //check current user owned this organization
            if (organization.OwnerUserId != _currentUser.UserId) 
            {
                throw new ValidationException(ErrorKeys.OrganizationAccessDenied);
            }

            // check slug
            if (!string.Equals(organization.Slug, request.Slug, StringComparison.OrdinalIgnoreCase)) 
            {
                var slugExists = await _organizationRepository.ExistsBySlugExcludingOrganizationAsync(request.Slug, request.Id, cancellationToken);
                if (slugExists) 
                {
                    throw new ValidationException(ErrorKeys.OrganizationSlugAlreadyExists);
                }
            }

            //check name
            if (!string.Equals(organization.Name, request.Name, StringComparison.OrdinalIgnoreCase)) 
            {
                var nameExists = await _organizationRepository.ExistsByNameExcludingOrganizationAsync(request.Name, request.Id, cancellationToken);
                if (nameExists) 
                {
                    throw new ValidationException(ErrorKeys.OrganizationNameAlreadyExists);
                }
            }

            //apply domain behaviour for Update
            organization.Update(request.Name, request.Slug, request.Description, request.LogoUrl);

            //Persists Changes
            await _organizationRepository.UpdateAsync(organization, cancellationToken);

            return new UpdateOrganizationResponse(
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
