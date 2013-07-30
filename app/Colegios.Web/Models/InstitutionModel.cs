using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Colegios.Web.Models
{
    public class InstitutionModel
    {
        [Required]
        [Display(Name="Nombre")]
        [StringLength(100, ErrorMessage="El campo no puede sobrepasar los 100 carácteres")]
        public virtual string Name { get; set; }

        //public Type {get;set;}

        [Required]
        [Display(Name="Dirección")]
        [StringLength(100, ErrorMessage = "El campo no puede sobrepasar los 100 carácteres")]
        public virtual string Address { get; set; }

        [Required]
        [Display(Name="Código Postal")]
        [RegularExpression(@"^\d{4,5}", ErrorMessage = "Introduzca un código postal válido")]
        public virtual string Postcode { get; set; }

        [Required]
        [Display(Name="Teléfono")]
        [RegularExpression(@"^\d{6,9}", ErrorMessage = "Introduzca un número de teléfono válido")]
        public virtual string FirstPhone { get; set; }

        [Display(Name="2º Teléfono")]
        [RegularExpression(@"^$|^\d{6,9}", ErrorMessage = "Introduzca un número de teléfono válido")]
        public virtual string SecondPhone { get; set; }

        
        [RegularExpression(@"^$|^\d{6,9}", ErrorMessage = "Introduzca un número de fax válido")]
        public virtual string Fax { get; set; }

        [Required]
        [RegularExpression(@"^[_a-z0-9-]+(.[_a-z0-9-]+)*@[a-z0-9-]+(.[a-z0-9-]+)*(.[a-z]{2,3})$", ErrorMessage = "Introduzca una dirección de email válida")]
        public virtual string Email { get; set; }

        [Display(Name="Sitio Web")]
        [RegularExpression(@"^(https?:\/\/)?(www\.)?([\da-z\.-]+)\.([a-z\.]{2,6})([\/\w \.-]*)*\/?$", ErrorMessage = "Introduzca una dirección web válida")]
        public virtual string WebSite { get; set; }

        [Required(ErrorMessage="Seleccione una ciudad")]
        [Display(Name = "Ciudad")]
        public virtual int City { get; set; }

        [Display(Name = "Municipio")]
        [Required]
        public virtual int Town { get; set; }


        [Display(Name = "Distrito")]
        [Required]
        public virtual int District { get; set; }

        [Display(Name = "Tipo de Centro")]
        [Required]
        public virtual int EducationType { get; set; }

        //public virtual Locality Locality { get; set; }

        //public bool IsBilingue { get; set; }


    }
}