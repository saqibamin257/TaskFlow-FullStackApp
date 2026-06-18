using System;
using System.Collections.Generic;
using System.Text;

namespace TaskFlow.Modules.Organizations.Domain.Entities
{
    //        Note:
    //        Organization Name: Pinenix Software Solutions
    //        Slug:pinenix
    public class Organization
    {
        public Guid Id { get; private set; }        
        public string Name { get; private set; } = string.Empty;
        public string Slug { get; private set; } = string.Empty;
        public string Description { get; private set; } = string.Empty;
        public string? LogoUrl { get; private set; }
        public Guid OwnerUserId { get; private set; }
        public bool IsActive { get; private set; } = true;
        public DateTime CreatedAtUTC { get; private set; }
        public DateTime? UpdatedAtUTC { get; private set; }

        private Organization()
        {
        }
        private Organization(string name, string slug, string description, string? logoUrl, Guid ownerUserId)
        {
            SetName(name);
            SetSlug(slug);

            Description = description;
            SetLogoUrl(logoUrl);
            OwnerUserId = ownerUserId;

            CreatedAtUTC = DateTime.UtcNow;
            IsActive = true;
        }
        public static Organization Create(
            string name,
            string slug,
            string description,
            string? logoUrl, 
            Guid ownerUserId)
        {
            return new Organization(
                name,
                slug,
                description,
                logoUrl, 
                ownerUserId);
        }

        public void Update(string name,string slug, string description,string? logoUrl)
        {
            SetName(name);
            SetSlug(slug);
            Description = description;
            SetLogoUrl(logoUrl);
            UpdatedAtUTC = DateTime.UtcNow;
        }

        public void DeActivate()
        {
            IsActive = false;
            UpdatedAtUTC = DateTime.UtcNow;
        }

        private void SetName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Organization name cannot be empty.");

            Name = name.Trim();
        }

        private void SetSlug(string slug)
        {
            if (string.IsNullOrWhiteSpace(slug))
                throw new ArgumentException("Organization slug cannot be empty.");

            Slug = slug.Trim().ToLowerInvariant();
        }

        private void SetLogoUrl(string? logoUrl)
        {
            LogoUrl = string.IsNullOrWhiteSpace(logoUrl)
                ? null
                : logoUrl.Trim();
        }
        internal void SetId(Guid id)
        {
            Id = id;
        }
    }
}
