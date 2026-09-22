using System;
using System.Collections.Generic;
using System.Text;

namespace TaskFlow.Modules.Users.Application.Abstractions
{
    public interface IUsersUnitOfWork
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
