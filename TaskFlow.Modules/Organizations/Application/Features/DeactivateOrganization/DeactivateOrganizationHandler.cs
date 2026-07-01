using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using TaskFlow.BuildingBlocks.Localization;
using TaskFlow.BuildingBlocks.Security.Abstraction;
using TaskFlow.Modules.Organizations.Application.Abstraction;

namespace TaskFlow.Modules.Organizations.Application.Features.DeactivateOrganization
{
    public class DeactivateOrganizationHandler:IRequestHandler<DeactivateOrganizationCommand>
    {
        private readonly IOrganizationRepository _organizationRepository;
        private readonly ICurrentUser _currentUser;
        public DeactivateOrganizationHandler(IOrganizationRepository organizationRepository, ICurrentUser currentUser)
        {
            _organizationRepository = organizationRepository;
            _currentUser = currentUser;
        }
        public async Task Handle(DeactivateOrganizationCommand request, CancellationToken cancellationToken)         
        {
            var organization = await _organizationRepository.GetByIdAsync(request.Id,cancellationToken);
            if (organization is null) 
            {
                throw new ValidationException(ErrorKeys.OrganizationNotFound);
            }

            if (organization.OwnerUserId != _currentUser.UserId)
            {
                throw new ValidationException(
                    ErrorKeys.OrganizationAccessDenied);
            }
            if (!organization.IsActive) 
            {
                throw new ValidationException(ErrorKeys.OrganizationAlreadyDeactivated);
            }

            organization.DeActivate();

            await _organizationRepository.UpdateAsync(organization,cancellationToken);
        }
    }
}
