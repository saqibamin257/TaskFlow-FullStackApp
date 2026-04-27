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
        public Guid TenantId { get; private set; }
        private User() { }

        private User(string name,string email,Guid tenantId)
        {
            SetName(name);
            SetEmail(email);
            TenantId = tenantId;
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
        public static User Create(string name, string email, Guid tenantId) 
        {
            return new User(name, email, tenantId);
        }
        public void UpdateUser(string name, string email) 
        {
            SetName(name);
            SetEmail(email);            
        }
    }
}
