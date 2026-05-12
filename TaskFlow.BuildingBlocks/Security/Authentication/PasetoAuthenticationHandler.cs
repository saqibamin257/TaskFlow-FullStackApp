using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Encodings.Web;
using TaskFlow.BuildingBlocks.Security.Abstraction;
using TaskFlow.BuildingBlocks.Security.Constants;

namespace TaskFlow.BuildingBlocks.Security.Authentication
{
    public class PasetoAuthenticationHandler:AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private readonly ITokenValidator _tokenValidator;
        public PasetoAuthenticationHandler(
        IOptionsMonitor<AuthenticationSchemeOptions> options,
        ILoggerFactory logger,
        UrlEncoder encoder,
        ITokenValidator tokenValidator)
        : base(options, logger, encoder)
        {
            _tokenValidator = tokenValidator;
        }
        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            var authorizationHeader =
                Request.Headers.Authorization.ToString();

            if (string.IsNullOrWhiteSpace(
                authorizationHeader))
            {
                return Task.FromResult(
                    AuthenticateResult.NoResult());
            }

            if (!authorizationHeader.StartsWith(
                "Bearer "))
            {
                return Task.FromResult(
                    AuthenticateResult.Fail(
                        "Invalid authorization scheme."));
            }

            var token = authorizationHeader["Bearer ".Length..];

            var principal = _tokenValidator.Validate(token);

            if (principal is null)
            {
                return Task.FromResult(
                    AuthenticateResult.Fail(
                        "Invalid token."));
            }

            var ticket =
                new AuthenticationTicket(
                    principal,
                    AuthenticationSchemes.Bearer);

            return Task.FromResult(
                AuthenticateResult.Success(ticket));
        }
    }
}
