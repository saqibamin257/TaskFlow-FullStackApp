using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TaskFlow.Modules.Users.Application.Abstractions;
using TaskFlow.Modules.Users.Domain.Entities;
using TaskFlow.Modules.Users.Infrastructure.Persistence;

namespace TaskFlow.Modules.Users.Infrastructure.Repositories
{
    public class UserRepository:IUserRepository
    {
        private readonly UsersDbContext _context;

        public UserRepository(UsersDbContext context) 
        {
            _context = context;
        }          

        public async Task<List<User>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Users
                .AsNoTracking()
                .ToListAsync(cancellationToken);            
        }

        public async Task<User?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);        
        }

        public async Task AddAsync(User user, CancellationToken cancellationToken = default)
        {
            await _context.Users.AddAsync(user, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

        }

        public async Task UpdateAsync(User user, CancellationToken cancellationToken = default)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync(cancellationToken);            
        }

        public async Task DeleteAsync(User user, CancellationToken cancellationToken = default) 
        {
            _context.Users.Remove(user);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
