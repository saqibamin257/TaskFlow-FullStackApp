using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using TaskFlow.Modules.Users.Domain.Entities;

namespace TaskFlow.Modules.Users.Infrastructure.Persistence
{
    public class UsersDbContext:DbContext
    {
        public UsersDbContext(DbContextOptions<UsersDbContext> options) : base(options) 
        {
        }
        public DbSet<User> Users => Set<User>();
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(
                typeof(UsersDbContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }

    }
}
