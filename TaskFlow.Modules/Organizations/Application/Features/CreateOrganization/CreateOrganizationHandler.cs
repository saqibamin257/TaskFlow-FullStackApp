using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using TaskFlow.BuildingBlocks.Localization;
using TaskFlow.BuildingBlocks.Security.Abstraction;
using TaskFlow.Modules.Organizations.Application.Abstraction;
using TaskFlow.Modules.Organizations.Domain.Entities;

namespace TaskFlow.Modules.Organizations.Application.Features.CreateOrganization
{
    public class CreateOrganizationHandler : IRequestHandler<CreateOrganizationCommand,CreateOrganizationResponse>
    {
        private readonly IOrganizationRepository _organizationRepository;
        private readonly ICurrentUser _currentUser;

        public CreateOrganizationHandler(IOrganizationRepository organizationRepository, ICurrentUser currentUser) 
        {
            _organizationRepository = organizationRepository;
            _currentUser = currentUser;
        }
        public async Task<CreateOrganizationResponse> Handle(CreateOrganizationCommand request, CancellationToken cancellationToken) 
        {
            if (!_currentUser.IsAuthenticated)
            {
                throw new UnauthorizedAccessException(
                    ErrorKeys.Unauthorized);
            }
            
            var slugExists = await _organizationRepository.ExistsBySlugAsync(request.Slug, cancellationToken);

            if (slugExists)
            {
                throw new ValidationException(
                    ErrorKeys.OrganizationSlugAlreadyExists);
            }

            var nameExists = await _organizationRepository.ExistsByNameAsync(request.Name,cancellationToken);

            if (nameExists)
            {
                throw new ValidationException(
                    ErrorKeys.OrganizationNameAlreadyExists);
            }


            var organization = Organization.Create(
            request.Name,
            request.Slug,
            request.Description,
            request.LogoUrl,
            _currentUser.UserId);

            await _organizationRepository.AddAsync(
                organization,
                cancellationToken);

            return new CreateOrganizationResponse(
            organization.Id,
            organization.Name,
            organization.Slug,
            organization.Description,
            organization.LogoUrl,
            organization.OwnerUserId,
            organization.IsActive,
            organization.CreatedAtUTC
            );
        }
    }
}
