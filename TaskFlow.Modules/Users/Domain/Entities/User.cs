using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using TaskFlow.BuildingBlocks.Localization;

namespace TaskFlow.Modules.Users.Domain.Entities
{
    public class User
    {
        public Guid Id { get; private set;}        
        public string FirstName { get; private set; } = string.Empty;
        public string? LastName { get; private set; }
        public string Email { get; private set; } = string.Empty;
        public string PasswordHash { get; private set; } = string.Empty;         
        public bool IsActive { get; private set; } = true;
        public DateTime CreatedAtUTC { get; private set; }
        public DateTime? EmailVerifiedAtUTC { get; private set; }
        private User() { }

        private User(string firstName,string lastName, string email,string passwordHash)
        {
            Id = Guid.CreateVersion7();
            SetFirstName(firstName);
            SetLastName(lastName);
            SetEmail(email);
            PasswordHash = passwordHash;                      
            CreatedAtUTC = DateTime.UtcNow;
            IsActive = true;
        }
        public void VerifyEmail()
        {
            EmailVerifiedAtUTC = DateTime.UtcNow;
        }
        public static User Create(string firstName, string lastName, string email, string passwordHash) 
        {
            return new User(firstName,lastName, email,passwordHash);
        }
        public void UpdateProfile(string firstName, string lastName, string email) 
        {
            SetFirstName(firstName);
            SetLastName(lastName);
            SetEmail(email);            
        }
        public void DeActivate ()
        {
            IsActive = false;
        }
        
        private void SetFirstName(string firstName)
        {
            if (string.IsNullOrEmpty(firstName))
            {
                throw new ValidationException(
                    ValidationKeys.NameRequired);
            }
            FirstName = firstName;
        }

        private void SetLastName(string lastName)
        {
            if (string.IsNullOrEmpty(lastName))
            {
                LastName = null;
                return;
            }
            LastName = lastName;
        }
        private void SetEmail(string email)
        {
            if (string.IsNullOrEmpty(email)) 
            {
                throw new ValidationException(
                   ValidationKeys.EmailRequired);
            }
                
            Email = email;
        }
        internal void SetId(Guid id)
        {
            Id = id;
        }

     
    }
}
