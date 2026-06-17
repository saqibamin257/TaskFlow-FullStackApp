using System;
using System.Collections.Generic;
using System.Text;
using TaskFlow.Modules.Organizations.Domain.Entities;

namespace TaskFlow.Modules.Organizations.Application.Abstraction
{
    public interface IOrganizationRepository
    {
        Task<List<Organization>> GetAllAsync(
            CancellationToken cancellationToken = default);

        Task<Organization?> GetByIdAsync(
            Guid id,
            CancellationToken cancellationToken = default);

        Task<Organization?> GetBySlugAsync(
            string slug,
            CancellationToken cancellationToken = default);

        Task AddAsync(
            Organization organization,
            CancellationToken cancellationToken = default);

        Task<bool> ExistsBySlugAsync(
            string slug,
            CancellationToken cancellationToken = default);
        Task<bool> ExistsByNameAsync(
            string name,
            CancellationToken cancellationToken = default);

        Task UpdateAsync(
            Organization organization,
            CancellationToken cancellationToken = default);
        Task<bool> ExistsByOwnerUserIdAsync(
            Guid ownerUserId,
            CancellationToken cancellationToken = default);
    }
}
