using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Web.Mvc;
using System.Web.Security;
using Colegios.Web.Infrastructure.Attributes;
using Microsoft.Practices.ServiceLocation;

namespace Colegios.Web.Models
{

    public class RegistrationModel
    {
        public int? UserId { get; set; }

        [Required]
        [RemoteUserName("isusernameavailable", "account",ErrorMessage="Ya existe un usuario registrado con este nombre de usuario", AdditionalFields="UserId")]
        [StringLength(32, ErrorMessage = "El nombre de usuario no debe exceder los 32 carácteres")]
        [Display(Name = "Nombre de Usuario")]
        public string UserName { get; set; }

        [Required]
        [Remote("isemailavailable", "account", ErrorMessage="Ya existe un usuario registrado con esa dirección de correo")]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"^[_a-z0-9-]+(.[_a-z0-9-]+)*@[a-z0-9-]+(.[a-z0-9-]+)*(.[a-z]{2,3})$", ErrorMessage = "Introduzca una dirección de email válida")]
        [StringLength(320,ErrorMessage="La dirección de correo no debe exceder los 320 carácteres")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "Las contraseñas no coinciden")]
        public string ConfirmPassword { get; set; }



    }
}
