using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Colegios.Web.Models
{
    public class OpinionModel
    {

        [Required(ErrorMessage = "Introduce un nombre de contacto")]
        [DisplayName("Nombre")]
        public string Name { get; set; }

        [DataType(DataType.EmailAddress)]
        [DisplayName("Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Rellena el campo Opinión")]
        [DisplayName("Opinión")]
        [DataType(DataType.MultilineText)]
        [StringLength(2048, ErrorMessage = "El campo opinión no puede contener más de 2048 carácteres.")]
        public string Opinion { get; set; }

    }
}