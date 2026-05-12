using Microsoft.Extensions.Configuration;
using Paseto;
using Paseto.Builder;
using Paseto.Cryptography.Key;
using Paseto.Protocol;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using TaskFlow.BuildingBlocks.Security.Abstraction;

namespace TaskFlow.BuildingBlocks.Security.Services
{
    public class TokenValidator : ITokenValidator
    {
        private readonly IConfiguration _configuration;

        public TokenValidator(
            IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public ClaimsPrincipal? Validate(string token)
        {
            try
            {
                var keyBytes =
                    Convert.FromBase64String(
                        _configuration["Token:SecretKey"]!);

                var key =
                    new PasetoSymmetricKey(
                        keyBytes,
                        new Version4());

                var validationResult =
                    new PasetoBuilder()
                        .Use(
                            ProtocolVersion.V4,
                            Purpose.Local)
                        .WithKey(key)
                        .Decode(token);

                var claims = new List<Claim>
                {
                    new(
                        ClaimTypes.NameIdentifier,
                        validationResult.Paseto.Payload["sub"]
                            ?.ToString() ?? string.Empty),

                    new(
                        ClaimTypes.Email,
                        validationResult.Paseto.Payload["email"]
                            ?.ToString() ?? string.Empty),

                    new(
                        ClaimTypes.Role,
                        validationResult.Paseto.Payload["role"]
                            ?.ToString() ?? string.Empty),

                    new(
                        "tenantId",
                        validationResult.Paseto.Payload["tenantId"]
                            ?.ToString() ?? string.Empty)
                };

                var identity =
                    new ClaimsIdentity(
                        claims,
                        "Paseto");

                return new ClaimsPrincipal(identity);
            }
            catch
            {
                return null;
            }
        }
    }
}
