using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using TaskFlow.Modules.Users.Application.Abstractions;
using TaskFlow.Modules.Users.Infrastructure.Email;
using TaskFlow.Modules.Users.Infrastructure.Persistence;
using TaskFlow.Modules.Users.Infrastructure.Repositories;
using TaskFlow.Modules.Users.Infrastructure.Security;

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
            services.AddScoped<IEmailVerificationTokenRepository, EmailVerificationTokenRepository>();

            // ------------------------------
            // Register Service
            // ------------------------------


            services.AddSingleton<IEmailVerificationTokenGenerator,EmailVerificationTokenGenerator>();
            services.AddScoped<IEmailSender, EmailSender>();
            services.AddSingleton<IEmailVerificationLinkGenerator,EmailVerificationLinkGenerator>();
            services.AddScoped<IUsersUnitOfWork>(provider => provider.GetRequiredService<UsersDbContext>());

            return services;
        }
    }
}
