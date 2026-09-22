using System;
using System.Collections.Generic;
using System.Text;

namespace TaskFlow.Modules.Users.Application.Abstractions
{
    public interface IEmailVerificationLinkGenerator
    {
        string GenerateEmailVerificationLink(string rawToken);
    }
}
