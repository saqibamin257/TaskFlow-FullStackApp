using System;
using System.Collections.Generic;
using System.Text;

namespace TaskFlow.Modules.Users.Application.Abstractions
{
    public interface IEmailVerificationTokenGenerator
    {
        EmailVerificationTokenResult Generate();
        string Hash(string rawToken);
        public sealed record EmailVerificationTokenResult(string RawToken, string TokenHash, DateTime ExpiresAtUTC);
    }
}
