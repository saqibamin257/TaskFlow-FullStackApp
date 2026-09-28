using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TaskFlow.Modules.Users.Application.Abstractions;
using TaskFlow.Modules.Users.Domain.Entities;
using TaskFlow.Modules.Users.Infrastructure.Configurations;

namespace TaskFlow.Modules.Users.Infrastructure.Persistence
{
    public class UsersDbContext:DbContext, IUsersUnitOfWork
    {
        public UsersDbContext(DbContextOptions<UsersDbContext> options) : base(options) 
        {
        }
        public DbSet<User> Users => Set<User>();
        public DbSet<EmailVerificationToken> EmailVerificationTokens => Set<EmailVerificationToken>();
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.ApplyConfigurationsFromAssembly(
            //    typeof(UsersDbContext).Assembly);

            modelBuilder.ApplyConfiguration(
                     new UserConfiguration());

            modelBuilder.ApplyConfiguration(
           new EmailVerificationTokenConfiguration());


            base.OnModelCreating(modelBuilder);
        }
    }
}
