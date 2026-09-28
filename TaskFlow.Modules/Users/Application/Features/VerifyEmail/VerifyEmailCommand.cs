using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace TaskFlow.Modules.Users.Application.Features.VerifyEmail
{
    public sealed record VerifyEmailCommand(string Token):IRequest<VerifyEmailResponse>
    {
    }
}
