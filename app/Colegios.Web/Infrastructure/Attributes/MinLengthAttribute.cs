using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Colegios.Web.Infrastructure.Attributes
{
    public class MinLengthAttribute: ValidationAttribute
    {
        private int _minLength;
        private const string _defaultErrorMessage = "'La longitud mínima del campo {0}' deber ser '{1}'";  

        public MinLengthAttribute(int minLength)
        {
            this._minLength = minLength;
        }

        //Override default FormatErrorMessage Method  
        public override string FormatErrorMessage(string name)
        {
            return string.Format(_defaultErrorMessage, name, (_minLength > 1) ? _minLength + " carácteres" : _minLength + " caracter");
        } 

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {

            var fieldValue = (string)value;

            if ((!String.IsNullOrEmpty(fieldValue)) && (fieldValue.Length >= _minLength))
                return null;
            else {
                var message = FormatErrorMessage(validationContext.DisplayName);
                return new ValidationResult(message);
            }

        }
    }
}