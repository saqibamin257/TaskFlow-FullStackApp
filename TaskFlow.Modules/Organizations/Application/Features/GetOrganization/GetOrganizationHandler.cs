using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using TaskFlow.BuildingBlocks.Localization;
using TaskFlow.Modules.Organizations.Application.Abstraction;

namespace TaskFlow.Modules.Organizations.Application.Features.GetOrganization
{
    public sealed class GetOrganizationHandler : IRequestHandler<GetOrganizationQuery, GetOrganizationResponse>
    {
        private readonly IOrganizationRepository _repository;

        public GetOrganizationHandler(
            IOrganizationRepository repository)
        {
            _repository = repository;
        }

        public async Task<GetOrganizationResponse> Handle(
            GetOrganizationQuery request,
            CancellationToken cancellationToken)
        {
            var organization =
                await _repository.GetByIdAsync(
                    request.Id,
                    cancellationToken);

            if (organization is null)
            {
                throw new KeyNotFoundException(
                    ErrorKeys.OrganizationNotFound);
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
