using System;
using System.Collections.Generic;
using System.Text;

namespace TaskFlow.Modules.Users.Domain.Entities
{
    public class EmailVerificationToken
    {
        public Guid Id { get; private set; }
        public Guid UserId { get; private set; }
        public string TokenHash { get; private set; } = string.Empty;

        public DateTime ExpiresAtUTC { get; private set; }

        public DateTime? UsedAtUTC { get; private set; }

        public DateTime CreatedAtUTC { get; private set; }

        private EmailVerificationToken()
        {
        }

        private EmailVerificationToken(
            Guid userId,
            string tokenHash,
            DateTime expiresAtUTC)
        {
            Id = Guid.CreateVersion7();
            UserId = userId;
            TokenHash = tokenHash;
            ExpiresAtUTC = expiresAtUTC;
            CreatedAtUTC = DateTime.UtcNow;
        }

        public static EmailVerificationToken Create(
            Guid userId,
            string tokenHash,
            DateTime expiresAtUTC)
        {
            return new EmailVerificationToken(
                userId,
                tokenHash,
                expiresAtUTC);
        }

        public bool IsExpired()
        {
            return DateTime.UtcNow >= ExpiresAtUTC;
        }

        public bool IsUsed()
        {
            return UsedAtUTC.HasValue;
        }

        public void MarkAsUsed()
        {
            if (IsUsed()) 
            {
                return;
            }
            UsedAtUTC = DateTime.UtcNow;
        }

    }
}
