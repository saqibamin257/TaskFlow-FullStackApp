using System;
using System.Collections.Generic;
using System.Text;
using TaskFlow.Modules.Organizations.Domain.Entities;

namespace TaskFlow.Modules.Organizations.Application.Abstraction
{
    public interface IOrganizationRepository
    {
        Task<List<Organization>> GetAccessibleOrganizationsAsync(Guid userId, CancellationToken cancellationToken = default);
        Task<Organization?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<Organization?> GetBySlugAsync(string slug, CancellationToken cancellationToken = default);
        Task AddAsync(Organization organization, CancellationToken cancellationToken = default);
        Task UpdateAsync(Organization organization, CancellationToken cancellationToken = default);
        //create
        Task<bool> ExistsBySlugAsync(string slug, CancellationToken cancellationToken = default);
        Task<bool> ExistsByNameAsync(string name, CancellationToken cancellationToken = default);
        
        //update
        Task<bool> ExistsBySlugExcludingOrganizationAsync(string slug,Guid organizationId, CancellationToken cancellationToken = default);
        Task<bool> ExistsByNameExcludingOrganizationAsync(string name, Guid organizationId, CancellationToken cancellationToken = default);

    }
}
