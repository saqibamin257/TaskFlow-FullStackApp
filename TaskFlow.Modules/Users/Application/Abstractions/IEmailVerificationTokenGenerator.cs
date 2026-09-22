using System;
using System.Collections.Generic;
using System.Text;

namespace TaskFlow.Modules.Users.Application.Abstractions
{
    public interface IEmailVerificationTokenGenerator
    {
        EmailVerificationTokenResult Generate();
        public sealed record EmailVerificationTokenResult(string RawToken, string TokenHash, DateTime ExpiresAtUTC);
    }
}
