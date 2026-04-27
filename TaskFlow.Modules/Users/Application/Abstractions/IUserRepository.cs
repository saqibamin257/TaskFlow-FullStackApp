using System;
using System.Collections.Generic;
using System.Text;
using TaskFlow.Modules.Users.Domain.Entities;

namespace TaskFlow.Modules.Users.Application.Abstractions
{
    public interface IUserRepository
    {
        Task<List<User>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<User?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task AddAsync(User user, CancellationToken cancellation = default);
        Task UpdateAsync(User user, CancellationToken cancellation = default);
        Task DeleteAsync(User user, CancellationToken cancellation = default);
    }
}
