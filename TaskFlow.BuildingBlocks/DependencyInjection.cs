using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using TaskFlow.BuildingBlocks.Application.Behaviors;
using TaskFlow.BuildingBlocks.Localization.Abstraction;
using TaskFlow.BuildingBlocks.Localization.Services;

namespace TaskFlow.BuildingBlocks
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddBuildingBlocks(
            this IServiceCollection services)
        {
            //-------- service:1
            // Allows services to access current HTTP request context.
            services.AddHttpContextAccessor();




            //-------- service:2
            // Registers MediatR pipeline behaviors
            // shared across the entire application.
            // Registers MediatR validation pipeline behavior.
            //
            // Every Command/Query passes through this behavior
            // before reaching its handler, allowing automatic
            // FluentValidation execution and centralized validation.


            services.AddTransient(
                typeof(IPipelineBehavior<,>),
                typeof(ValidationBehavior<,>));


            //-------- service:3
            // Registers JSON-based localization service
            // used for multilingual validation/error messages.
            services.AddScoped<
                ILocalizationService,
                JsonLocalizationService>();

            return services;
        }
    }
}
