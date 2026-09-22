using System;
using System.Collections.Generic;
using System.Text;
using TaskFlow.Modules.Users.Application.Abstractions;

namespace TaskFlow.Modules.Users.Infrastructure.Email
{
    public class EmailSender:IEmailSender
    {
        public Task SendAsync(
        string recipientEmail,
        string subject,
        string body,
        CancellationToken cancellationToken)
        {
            // Email provider implementation will go here.

            return Task.CompletedTask;
        }
    }
}
