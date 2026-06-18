using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using TaskFlow.Modules.Organizations.Domain.Entities;

namespace TaskFlow.Modules.Organizations.Infrastructure.Configurations
{
    public class OrganizationConfiguration : IEntityTypeConfiguration<Organization>
    {
        public void Configure(
            EntityTypeBuilder<Organization> builder)
        {
            // ------------------------------
            // Table
            // ------------------------------
            builder.ToTable("Organizations");

            // ------------------------------
            // Primary Key
            // ------------------------------
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasDefaultValueSql("NEWSEQUENTIALID()");

            // ------------------------------
            // Properties
            // ------------------------------
            builder.Property(x => x.Name)
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(x => x.Slug)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(x => x.Description)
                .HasMaxLength(1000);

            builder.Property(x => x.LogoUrl)
                .HasMaxLength(500);

            builder.Property(x => x.OwnerUserId)
                .IsRequired();

            builder.Property(x => x.IsActive)
                .IsRequired();

            builder.Property(x => x.CreatedAtUTC)
                .IsRequired();

            // ------------------------------
            // Indexes
            // ------------------------------
            builder.HasIndex(x => x.Slug)
                .IsUnique();
        }
    }
}
