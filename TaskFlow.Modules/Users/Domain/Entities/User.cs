using System;
using System.Collections.Generic;
using System.Text;

namespace TaskFlow.Modules.Users.Domain.Entities
{
    public class User
    {
        public int Id { get; private set;}
        public string Name { get; private set; } = string.Empty;
        public string Email { get; private set; } = string.Empty;
        public string PasswordHash { get; private set; } = string.Empty;
        public string Role { get; private set; } = "Member";
        public Guid TenantId { get; private set; }
        public bool IsActive { get; private set; } = true;
        public DateTime CreatedAtUTC { get; private set; }
        private User() { }

        private User(string name,string email,string passwordHash,string role,Guid tenantId)
        {
            SetName(name);
            SetEmail(email);
            PasswordHash = passwordHash;
            Role = role;
            TenantId = tenantId;
            CreatedAtUTC = DateTime.UtcNow;
            IsActive = true;
        }        
        public static User Create(string name, string email, string passwordHash, string role, Guid tenantId) 
        {
            return new User(name, email,passwordHash,role,tenantId);
        }
        public void Update(string name, string email) 
        {
            SetName(name);
            SetEmail(email);            
        }
        public void DeActivate ()
        {
            IsActive = false;
        }
        private void SetName(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("Name can not be empty");
            Name = name;
        }
        private void SetEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
                throw new ArgumentException("Email can not be empty");
            Email = email;
        }
        internal void SetId(int id)
        {
            Id = id;
        }
    }
}
