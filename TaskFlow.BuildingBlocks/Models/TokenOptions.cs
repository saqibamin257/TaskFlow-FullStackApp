using System;
using System.Collections.Generic;
using System.Text;

namespace TaskFlow.BuildingBlocks.Models
{
    public class TokenOptions
    {
        public string Issuer { get; set; } = string.Empty;

        public string Audience { get; set; } = string.Empty;

        public string SecretKey { get; set; } = string.Empty;

        public int ExpiryMinutes { get; set; }
    }
}
