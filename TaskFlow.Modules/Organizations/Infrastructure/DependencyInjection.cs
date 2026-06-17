using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using TaskFlow.Modules.Organizations.Application.Abstraction;
using TaskFlow.Modules.Organizations.Infrastructure.Persistence;
using TaskFlow.Modules.Organizations.Infrastructure.Repositories;

namespace TaskFlow.Modules.Organizations.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddOrganizationsInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<OrganizationsDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IOrganizationRepository, OrganizationRepository>();

            return services;
        }
    }
}
