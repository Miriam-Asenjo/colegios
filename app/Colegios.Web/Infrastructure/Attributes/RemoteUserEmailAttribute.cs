using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Colegios.Query.Core.QueryInterfaces;
using System.ComponentModel.DataAnnotations;
using Microsoft.Practices.ServiceLocation;

namespace Colegios.Web.Infrastructure.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class RemoteUserEmailAttribute : System.Web.Mvc.RemoteAttribute
    {
        private readonly IUserQuery userQuery;


        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {

            var userEmailAvailable = userQuery.IsUserEmailAvailable((string)value);
            if (userEmailAvailable)
                return null;

            return new ValidationResult(this.FormatErrorMessage(validationContext.DisplayName));
        }


        public RemoteUserEmailAttribute(string action, string controller)
            : base(action, controller)
        {
            userQuery = ServiceLocator.Current.GetInstance<IUserQuery>();
        }
    }
}