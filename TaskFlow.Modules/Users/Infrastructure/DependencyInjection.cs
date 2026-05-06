using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using TaskFlow.Modules.Users.Application.Abstractions;
using TaskFlow.Modules.Users.Infrastructure.Persistence;
using TaskFlow.Modules.Users.Infrastructure.Repositories;

namespace TaskFlow.Modules.Users.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddUsersInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            // ------------------------------
            // Register UsersDbContext
            // ------------------------------
            services.AddDbContext<UsersDbContext>(options =>
            {
                options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection"));
            });

            // ------------------------------
            // Register Repositories
            // ------------------------------

            services.AddScoped<IUserRepository, UserRepository>();

            return services;
        }
    }
}
