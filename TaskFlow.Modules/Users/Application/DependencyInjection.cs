using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace TaskFlow.Modules.Users.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddUsersApplication(
            this IServiceCollection services)
        {
            // ------------------------------
            // MediatR
            // ------------------------------
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(
                    typeof(AssemblyReference).Assembly);
            });

            // ------------------------------
            // FluentValidation
            // ------------------------------
            services.AddValidatorsFromAssembly(
                typeof(AssemblyReference).Assembly);

            return services;
        }
    }
}
