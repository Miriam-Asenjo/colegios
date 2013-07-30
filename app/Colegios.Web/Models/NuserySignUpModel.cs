using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.Practices.ServiceLocation;
using Colegios.Query.Core.QueryInterfaces;
using System.Linq;

namespace Colegios.Web.Models
{
    public class NurserySignUpModel : IValidatableObject
    {
        public RegistrationModel Registration { get; set; }

        public InstitutionModel Institution { get; set; }

        public IList<CategoryDetailModel> CategoryDetails { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var nurseryQuery = ServiceLocator.Current.GetInstance<INurseryQuery>();
            var categoryFields = nurseryQuery.GetCategoryFields();

            for(var i = 0; i < categoryFields.Count; i ++)
            {
                var categoryDetail = CategoryDetails[i];
                var category = categoryFields.Where(x => x.CategoryField.Id == categoryDetail.CategoryFieldId).FirstOrDefault();
                if ((category.Mandatory) && (string.IsNullOrEmpty(categoryDetail.Value)))
                    yield return new ValidationResult("El campo " + category.CategoryField.Description + " es obligatorio", new[] {"CategoryDetails[" + i + "].Value"});

            }
            //if (PreferredSectors == null || !PreferredSectors.Any())
            //    yield return new ValidationResult("Please select at least one sector.", new[] { "PreferredSectors" });
            yield return null;
            //throw new NotImplementedException();
        }
    }
}