using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Colegios.Web.Enums;

namespace Colegios.Web.Models
{
    public class MarkSearchViewModel
    {
        [DisplayName("Prueba")]
        public MarkTypeEnum MarkType { get; set; }

        [DisplayName("Año")]
        public int Year { get; set; }

        [Required(ErrorMessage = "Selecciona una Ciudad")]
        [DisplayName("Ciudad")]
        public int CityId { get; set; }

        [Required(ErrorMessage = "Selecciona un Municipio")]
        [DisplayName("Municipio")]
        public int TownId { get; set; }

        [DisplayName("Distritos")]
        public int[] Locations{ get; set; }

        [DisplayName("Tipo de Centro")]
        public int? EducationTypeId { get; set; }


    }
}