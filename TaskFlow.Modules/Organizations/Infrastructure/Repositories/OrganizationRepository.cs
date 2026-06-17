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

        public async Task<List<Organization>> GetAllAsync(
            CancellationToken cancellationToken = default)
        {
            return await _context.Organizations
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }

        public async Task<Organization?> GetByIdAsync(
            Guid id,
            CancellationToken cancellationToken = default)
        {
            return await _context.Organizations
                .FirstOrDefaultAsync(
                    x => x.Id == id,
                    cancellationToken);
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
            return await _context.Organizations.AnyAsync(x => x.Slug == slug, cancellationToken);
        }

        public async Task<bool> ExistsByNameAsync(string name, CancellationToken cancellationToken = default)
        {
            return await _context.Organizations.AnyAsync(x => x.Name == name, cancellationToken);
        }

        public async Task<bool> ExistsByOwnerUserIdAsync(Guid ownerUserId, CancellationToken cancellationToken = default)
        {
            return await _context.Organizations.AnyAsync(x => x.OwnerUserId == ownerUserId, cancellationToken);
        }
    }
}