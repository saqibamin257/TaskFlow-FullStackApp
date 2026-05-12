using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using TaskFlow.BuildingBlocks.Application.Behaviors;
using TaskFlow.BuildingBlocks.Localization.Abstraction;
using TaskFlow.BuildingBlocks.Localization.Services;
using TaskFlow.BuildingBlocks.Security.Abstraction;
using TaskFlow.BuildingBlocks.Security.Services;

namespace TaskFlow.BuildingBlocks
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddBuildingBlocks(
            this IServiceCollection services, IConfiguration configuration)
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


            //------ service:4
            //----Password Hasher
            services.AddSingleton<IPasswordHasher, PasswordHasher>();




            ////----- service:5
            ////----- Token Option
            //services.Configure<TokenOptions>(configuration.GetSection("Token"));
            //Console.WriteLine(
            //    configuration["Token:Issuer"]);

            //Console.WriteLine(
            //    configuration["Token:SecretKey"]);

            //------ service:6
            //----Token Provider
            services.AddScoped<ITokenProvider, TokenProvider>();
            
            ////----- service:7
            ////----- Token Validator 
            services.AddScoped<ITokenValidator,TokenValidator>();


            return services;
        }
    }
}
