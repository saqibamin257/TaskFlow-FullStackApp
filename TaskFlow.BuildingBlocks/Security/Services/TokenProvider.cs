using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Paseto;
using Paseto.Builder;
using Paseto.Cryptography.Key;
using System;
using System.Collections.Generic;
using System.Text;
using TaskFlow.BuildingBlocks.Models;
using TaskFlow.BuildingBlocks.Security.Abstraction;
using Paseto.Protocol;

namespace TaskFlow.BuildingBlocks.Security.Services
{
    public class TokenProvider:ITokenProvider
    {
        private readonly Models.TokenOptions _tokenOptions;
        public TokenProvider(IOptions<Models.TokenOptions> tokenOptions)
        {
            _tokenOptions = tokenOptions.Value;
        }

        public string Generate(Models.AuthenticatedUser user)
        {
            var key = GetSymmetricKey();
            
            var token = new PasetoBuilder()
            .Use(ProtocolVersion.V4, Purpose.Local)
            .WithKey(key)
            .Issuer(_tokenOptions.Issuer)
            .Audience(_tokenOptions.Audience)
            .Subject(user.UserId.ToString())
            .Expiration(DateTime.UtcNow.AddMinutes(
                _tokenOptions.ExpiryMinutes))
            .AddClaim("email", user.Email)
            .AddClaim("role", user.Role)
            .AddClaim("tenantId",
                user.TenantId.ToString())
            .Encode();

            return token;
        }

        private PasetoSymmetricKey GetSymmetricKey()
        {
            var keyBytes =
                Convert.FromBase64String(
                    _tokenOptions.SecretKey);

            if (keyBytes.Length != 32)
            {
                throw new Exception(
                    "Token secret key must be 32 bytes.");
            }

            return new PasetoSymmetricKey(
                                        keyBytes,
                                        new Version4());
        }
    }
}
