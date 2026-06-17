using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TaskFlow.Modules.Users.Domain.Entities;
using TaskFlow.Modules.Users.Infrastructure.Configurations;

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
            //modelBuilder.ApplyConfigurationsFromAssembly(
            //    typeof(UsersDbContext).Assembly);

            modelBuilder.ApplyConfiguration(
                     new UserConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
