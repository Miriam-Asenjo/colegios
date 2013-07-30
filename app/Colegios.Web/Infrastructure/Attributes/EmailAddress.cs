using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Colegios.Web.Infrastructure.Attributes
{
    public class EmailAddressAttribute : RegularExpressionAttribute
    {
        private const string EmailRegex = @"[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}";

        public EmailAddressAttribute()
            : base(EmailRegex)
        {
            this.ErrorMessage = "Introduzca una dirección de email valida";
        }

        public EmailAddressAttribute(string errorMessage): base(EmailRegex)
        {
            this.ErrorMessage = errorMessage;

        }

    }
}