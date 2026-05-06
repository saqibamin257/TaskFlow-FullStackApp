using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

using System;
using System.Collections.Generic;
using System.Text;

namespace TaskFlow.Modules.Users.Infrastructure.Persistence
{
    public class UsersDbContextFactory
    : IDesignTimeDbContextFactory<UsersDbContext>
    {
        public UsersDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<UsersDbContext>();

            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = configuration.GetConnectionString("DefaultConnection");

            optionsBuilder.UseSqlServer(connectionString);

            return new UsersDbContext(optionsBuilder.Options);
        }
    }
}
