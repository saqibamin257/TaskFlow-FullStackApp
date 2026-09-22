using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TaskFlow.Modules.Users.Application.Abstractions;
using TaskFlow.Modules.Users.Domain.Entities;
using TaskFlow.Modules.Users.Infrastructure.Persistence;

namespace TaskFlow.Modules.Users.Infrastructure.Repositories
{
    public sealed class EmailVerificationTokenRepository : IEmailVerificationTokenRepository
    {
        private readonly UsersDbContext _context;

        public EmailVerificationTokenRepository(
            UsersDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(EmailVerificationToken token, CancellationToken cancellationToken)
        {
            await _context.EmailVerificationTokens.AddAsync(token, cancellationToken);
        }

        public async Task<EmailVerificationToken?> GetByTokenHashAsync(string tokenHash, CancellationToken cancellationToken)
        {
            return await _context.EmailVerificationTokens.FirstOrDefaultAsync(x => x.TokenHash == tokenHash, cancellationToken);
        }

        // No separate implementation is required because UsersDbContext
        // already implements IUsersUnitOfWork, and SaveChangesAsync()
        // is inherited from DbContext and satisfies the interface contract.
        //
        // Similarly, an explicit transaction boundary is not required here.
        // SaveChangesAsync() automatically wraps the database changes in
        // a transaction when multiple changes are being persisted together.
        //
        // Therefore, the following implementation is unnecessary:
        //
        // public async Task SaveChangesAsync(CancellationToken cancellationToken)
        // {
        //     await _context.SaveChangesAsync(cancellationToken);
        // }
    }
}
