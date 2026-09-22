using System;
using System.Collections.Generic;
using System.Text;

namespace TaskFlow.Modules.Users.Application.Abstractions
{
    public interface IEmailSender
    {
        Task SendAsync(string recipientEmail, string subject, string body, CancellationToken cancellationToken);
    }
}
