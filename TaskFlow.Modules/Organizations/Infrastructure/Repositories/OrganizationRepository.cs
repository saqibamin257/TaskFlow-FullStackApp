using Microsoft.EntityFrameworkCore;
using TaskFlow.Modules.Organizations.Application.Abstraction;
using TaskFlow.Modules.Organizations.Domain.Entities;
using TaskFlow.Modules.Organizations.Infrastructure.Persistence;

namespace TaskFlow.Modules.Organizations.Infrastructure.Repositories
{
    public class OrganizationRepository : IOrganizationRepository
    {
        private readonly OrganizationsDbContext _context;

        public OrganizationRepository(
            OrganizationsDbContext context)
        {
            _context = context;
        }
        public async Task<List<Organization>> GetAccessibleOrganizationsAsync(Guid userId, CancellationToken cancellationToken = default)
        {
            return await _context.Organizations.AsNoTracking()
                                               .Where(x => x.OwnerUserId == userId && x.IsActive==true)
                                               .OrderBy(x => x.Name)
                                               .ToListAsync(cancellationToken);
        }

        public async Task<Organization?> GetByIdAsync(
            Guid id,
            CancellationToken cancellationToken = default)
        {
            return await _context.Organizations.FirstOrDefaultAsync(x => x.Id == id && x.IsActive == true, cancellationToken);
        }

        public async Task<Organization?> GetBySlugAsync(
            string slug,
            CancellationToken cancellationToken = default)
        {
            return await _context.Organizations
                .FirstOrDefaultAsync(
                    x => x.Slug == slug,
                    cancellationToken);
        }

        public async Task AddAsync(
            Organization organization,
            CancellationToken cancellationToken = default)
        {
            await _context.Organizations.AddAsync(
                organization,
                cancellationToken);

            await _context.SaveChangesAsync(
                cancellationToken);
        }

        public async Task UpdateAsync(
            Organization organization,
            CancellationToken cancellationToken = default)
        {
            _context.Organizations.Update(
                organization);

            await _context.SaveChangesAsync(
                cancellationToken);
        }

        public async Task<bool> ExistsBySlugAsync(string slug, CancellationToken cancellationToken = default)
        {
            var normalizedSlug = slug.Trim().ToLowerInvariant();

            return await _context.Organizations
                    .AnyAsync(x => x.Slug == normalizedSlug ,cancellationToken);
        }

        public async Task<bool> ExistsByNameAsync(string name, CancellationToken cancellationToken = default)
        {
            var normalizedName = name.Trim();

            return await _context.Organizations
                .AnyAsync(
                    x => x.Name == normalizedName,
                    cancellationToken);
        }

        public async Task<bool> ExistsBySlugExcludingOrganizationAsync(string slug, Guid organizationId, CancellationToken cancellationToken = default)
        {
            var normalizedSlug = slug.Trim().ToLowerInvariant();
            return await _context.Organizations.AnyAsync(x => x.Id != organizationId && x.Slug == normalizedSlug, cancellationToken);
        }

        public async Task<bool> ExistsByNameExcludingOrganizationAsync(string name, Guid organizationId, CancellationToken cancellationToken = default)
        {
            var normalizedName = name.Trim();
            return await _context.Organizations.AnyAsync(x => x.Id != organizationId && x.Name == normalizedName, cancellationToken);
        }
    }
}