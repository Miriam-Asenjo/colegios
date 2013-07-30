using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpArch.Domain.DomainModel;
using Shared.Security;
using Colegios.Command.Core.Domain.Lookups;

namespace Colegios.Command.Core.Domain
{
    [Serializable]
    public class User: Entity
    {
        public User (){}

        public User(string userName, string email, string password, UserType userType)
        {
            UserName = userName;
            Email = email;
            HashPassword = PasswordHash.CreateHash(password);
            UserType = userType;

        }

        public virtual string UserName { get; set; }
        public virtual string Email { get; set; }
        public virtual string HashPassword { get; protected set; }
        public virtual UserType UserType { get; set; }
    }
}
