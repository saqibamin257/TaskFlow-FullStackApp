using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using TaskFlow.BuildingBlocks.Localization;
using TaskFlow.BuildingBlocks.Security.Abstraction;
using TaskFlow.Modules.Organizations.Application.Abstraction;

namespace TaskFlow.Modules.Organizations.Application.Features.GetOrganization
{
    public sealed class GetOrganizationsHandler : IRequestHandler<GetOrganizationsQuery, List<GetOrganizationsResponse>>
    {
        private readonly IOrganizationRepository _repository;
        private readonly ICurrentUser _currentUser;

        public GetOrganizationsHandler(IOrganizationRepository repository,ICurrentUser currentUser)
        {
            _repository = repository;
            _currentUser = currentUser;
        }

        public async Task<List<GetOrganizationsResponse>> Handle(GetOrganizationsQuery request,CancellationToken cancellationToken)
        {
            var organizations = await _repository.GetAccessibleOrganizationsAsync(_currentUser.UserId, cancellationToken);

            return organizations.Select( x => new GetOrganizationsResponse(
                x.Id,
                x.Name,
                x.Slug,
                x.Description,
                x.LogoUrl,
                x.OwnerUserId,
                x.IsActive,
                x.CreatedAtUTC,
                x.UpdatedAtUTC))
                .ToList();
        }
    }
}
