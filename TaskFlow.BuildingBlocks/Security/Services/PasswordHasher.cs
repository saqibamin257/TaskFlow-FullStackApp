using Konscious.Security.Cryptography;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using TaskFlow.BuildingBlocks.Security.Abstraction;

namespace TaskFlow.BuildingBlocks.Security.Services
{
    public class PasswordHasher : IPasswordHasher
    {
        public string Hash(string password)
        {
            // Generate random salt
            var salt = RandomNumberGenerator.GetBytes(16);

            // Configure Argon2
            var argon2 = new Argon2id(
                Encoding.UTF8.GetBytes(password))
            {
                Salt = salt,
                DegreeOfParallelism = 8,
                Iterations = 4,
                MemorySize = 1024 * 128
            };

            // Generate hash
            var hash = argon2.GetBytes(32);

            // Combine salt + hash
            var hashBytes = new byte[48];

            Buffer.BlockCopy(salt, 0, hashBytes, 0, 16);
            Buffer.BlockCopy(hash, 0, hashBytes, 16, 32);

            return Convert.ToBase64String(hashBytes);

        }

        public bool Verify(string password, string passwordHash)
        {
            var hashBytes = Convert.FromBase64String(passwordHash);

            // Extract salt
            var salt = new byte[16];
            Buffer.BlockCopy(hashBytes, 0, salt, 0, 16);

            // Hash incoming password
            var argon2 = new Argon2id(
                Encoding.UTF8.GetBytes(password))
            {
                Salt = salt,
                DegreeOfParallelism = 8,
                Iterations = 4,
                MemorySize = 1024 * 128
            };

            var hash = argon2.GetBytes(32);

            // Compare hashes
            for (int i = 0; i < 32; i++)
            {
                if (hashBytes[i + 16] != hash[i])
                {
                    return false;
                }
            }

            return true;
        }
    }
}
