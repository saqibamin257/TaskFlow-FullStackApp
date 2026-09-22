using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using TaskFlow.Modules.Users.Application.Abstractions;
using static TaskFlow.Modules.Users.Application.Abstractions.IEmailVerificationTokenGenerator;

namespace TaskFlow.Modules.Users.Infrastructure.Security
{
    public sealed class EmailVerificationTokenGenerator : IEmailVerificationTokenGenerator
    {
        private const int TokenSizeInBytes = 32;
        private const int ExpirationHours = 24;

        public EmailVerificationTokenResult Generate()
        {
            var tokenBytes = RandomNumberGenerator.GetBytes(
                TokenSizeInBytes);

            var rawToken = Convert.ToBase64String(tokenBytes);

            var tokenHash = Convert.ToHexString(
                SHA256.HashData(
                    Encoding.UTF8.GetBytes(rawToken)));

            var expiresAtUTC =
                DateTime.UtcNow.AddHours(ExpirationHours);

            return new EmailVerificationTokenResult(
                rawToken,
                tokenHash,
                expiresAtUTC);
        }
    }
}
