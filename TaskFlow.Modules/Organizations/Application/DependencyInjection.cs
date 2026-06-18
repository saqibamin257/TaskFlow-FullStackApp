using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Text;

namespace TaskFlow.Modules.Organizations.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddOrganizationsApplication(
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
