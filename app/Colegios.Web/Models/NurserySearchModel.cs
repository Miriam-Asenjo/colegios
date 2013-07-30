using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Colegios.Web.Models
{
    public class NurserySearchModel
    {
        [Required(ErrorMessage = "Selecciona una Ciudad")]
        [DisplayName("Ciudad")]
        public int CityId { get; set; }

        [Required(ErrorMessage = "Selecciona un Municipio")]
        [DisplayName("Municipio")]
        public int TownId { get; set; }

        public string Locations { get; set; }

        [DisplayName("Tipo Centro")]
        public int? EducationTypeId { get; set; }

        [DisplayName("¿Es Bilingüe?")]
        [Required]
        public bool IsBilingual { get; set; }

        [DisplayName("¿Qué Buscas?")]
        [StringLength(255)]
        public string Name { get; set; }

    }
}
