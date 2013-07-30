using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Microsoft.Practices.ServiceLocation;
using Colegios.Query.Core.QueryInterfaces;

namespace Colegios.Web.Infrastructure.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class RemoteUserNameAttribute : System.Web.Mvc.RemoteAttribute
    {
        private readonly IUserQuery userQuery;


        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var additionalField = validationContext.ObjectType.GetProperty(this.AdditionalFields);
            var userId = (int?)additionalField.GetValue(validationContext.ObjectInstance, null);
            var userNameAvailable = userQuery.IsUserNameAvailable((string)value);
            if (userNameAvailable)
                return null;

            return new ValidationResult(this.FormatErrorMessage(validationContext.DisplayName));
        }


        public RemoteUserNameAttribute(string action, string controller)
            : base(action, controller)
        {
            userQuery = ServiceLocator.Current.GetInstance<IUserQuery>();
        }
    }
}