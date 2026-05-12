using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using TaskFlow.BuildingBlocks.Security.Abstraction;

namespace TaskFlow.BuildingBlocks.Security.Services
{
    public class CurrentUser : ICurrentUser
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUser(
            IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public bool IsAuthenticated =>
            _httpContextAccessor
                .HttpContext?
                .User?
                .Identity?
                .IsAuthenticated ?? false;

        public Guid UserId =>
            GetGuidClaim(
                ClaimTypes.NameIdentifier);

        public string Email =>
            GetClaim(
                ClaimTypes.Email);

        public string Role =>
            GetClaim(
                ClaimTypes.Role);

        public Guid TenantId =>
            GetGuidClaim(
                "tenantId");

        private string GetClaim(
            string claimType)
        {
            return _httpContextAccessor
                       .HttpContext?
                       .User?
                       .FindFirst(claimType)?
                       .Value
                   ?? string.Empty;
        }

        private Guid GetGuidClaim(
            string claimType)
        {
            var value = GetClaim(claimType);

            return Guid.TryParse(
                value,
                out var result)
                ? result
                : Guid.Empty;
        }
    }
}
