using System;
using System.Collections.Generic;
using System.Text;
using TaskFlow.Modules.Users.Domain.Entities;

namespace TaskFlow.Modules.Users.Application.Abstractions
{
    public interface IEmailVerificationTokenRepository
    {
        Task AddAsync(EmailVerificationToken token, CancellationToken cancellationToken);

        Task<EmailVerificationToken?> GetByTokenHashAsync(string tokenHash, CancellationToken cancellationToken);        
    }
}
