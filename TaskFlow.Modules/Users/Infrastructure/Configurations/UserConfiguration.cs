using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using TaskFlow.Modules.Users.Domain.Entities;


namespace TaskFlow.Modules.Users.Infrastructure.Configurations
{
    public class UserConfiguration :IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder) 
        {


            // ----- All SQL related rules stay here like lengths, indexes and constraints  ----------
            // Table
            // ------------------------------
            builder.ToTable("Users");

            // ------------------------------
            // Primary Key
            // ------------------------------
            builder.HasKey(x => x.Id);

            // ------------------------------
            // Properties
            // ------------------------------
            builder.Property(x => x.Name)
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(x => x.Email)
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(x => x.PasswordHash)
                .IsRequired();

            builder.Property(x => x.Role)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(x => x.TenantId)
                .IsRequired();

            builder.Property(x => x.IsActive)
                .IsRequired();

            builder.Property(x => x.CreatedAtUTC)
                .IsRequired();

            // ------------------------------
            // Indexes
            // ------------------------------
            builder.HasIndex(x => x.Email)
                .IsUnique();
        }
    }
}
