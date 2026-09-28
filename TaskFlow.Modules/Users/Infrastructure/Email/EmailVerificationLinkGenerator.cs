using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using TaskFlow.Modules.Users.Application.Abstractions;

namespace TaskFlow.Modules.Users.Infrastructure.Email
{
    public class EmailVerificationLinkGenerator : IEmailVerificationLinkGenerator
    {        
        private readonly IConfiguration _configuration;
        public EmailVerificationLinkGenerator(
            IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateEmailVerificationLink(string rawToken)
        {
            var frontendUrl =
                _configuration["Application:FrontendUrl"]
                ?? throw new InvalidOperationException(
                    "Application:FrontendUrl is not configured.");

            return $"{frontendUrl.TrimEnd('/')}/verify-email?token={Uri.EscapeDataString(rawToken)}";
        }
    }
}
