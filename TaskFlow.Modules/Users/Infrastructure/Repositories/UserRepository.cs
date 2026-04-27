using System;
using System.Collections.Generic;
using System.Text;
using TaskFlow.Modules.Users.Application.Abstractions;
using TaskFlow.Modules.Users.Domain.Entities;

namespace TaskFlow.Modules.Users.Infrastructure.Repositories
{
    public class UserRepository:IUserRepository
    {
        private static List<User> _users = new List<User>
                {
                  User.Create("Saqib Amin","saq@gmail.com",Guid.NewGuid()),
                  User.Create("Waqar Amin","waq@gmail.com",Guid.NewGuid()),                  
                };
        public Task AddAsync(User user, CancellationToken cancellation = default)
        {
            // Simulate identity generation
            user.GetType().GetProperty("Id")!
                .SetValue(user, _users.Count == 0 ? 1 : _users.Max(x => x.Id) + 1);
            _users.Add(user);
            return Task.CompletedTask;
        }        

        public Task<List<User>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return Task.FromResult(_users.ToList());
        }

        public Task<User?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            var user = _users.FirstOrDefault(x => x.Id == id);
            return Task.FromResult(user);
        }

        public Task UpdateAsync(User user, CancellationToken cancellation = default)
        {
            var existingUser = _users.FirstOrDefault(x => x.Id == user.Id);
            if (existingUser == null)
                throw new Exception("User not found");
            // Since entity is reference type, it's already updated
            return Task.CompletedTask;
        }

        public Task DeleteAsync(User user, CancellationToken cancellation = default) 
        {
            _users.Remove(user);
            return Task.CompletedTask;
        }

    }
}
