using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TaskFlow.Modules.Organizations.Domain.Entities;
using TaskFlow.Modules.Organizations.Infrastructure.Configurations;

namespace TaskFlow.Modules.Organizations.Infrastructure.Persistence
{
    public class OrganizationsDbContext:DbContext
    {
        public OrganizationsDbContext(
        DbContextOptions<OrganizationsDbContext> options)
        : base(options)
        {
        }

        public DbSet<Organization> Organizations => Set<Organization>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.ApplyConfigurationsFromAssembly(
            //    typeof(OrganizationsDbContext).Assembly);
             modelBuilder.ApplyConfiguration(
                   new OrganizationConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
