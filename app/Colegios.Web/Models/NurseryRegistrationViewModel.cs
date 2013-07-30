using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Colegios.Query.Core.DTOs;

namespace Colegios.Web.Models
{
    public class NurseryRegistrationViewModel
    {

        //public NurserySignUpModel SignUpModel { get; set; }
        public RegistrationModel Registration { get; set; }

        public InstitutionModel Institution { get; set; }

        public SearchBaseModel BaseModel { get; set; }

        public IList<CategoryModel> Categories { get; set; }

        public IList<CategoryDetailModel> CategoryDetails { get; set; }


        public NurseryRegistrationViewModel(SearchBaseModel baseModel, IList<CategoryFieldInstitutionTypeDto> categoryFields)
        {
            this.BaseModel = baseModel;
            this.Categories = new List<CategoryModel>();
            var categories = categoryFields.GroupBy(x => x.CategoryField.Category.Name);

            foreach (var categoryGroup in categories)
            {
                var category = new CategoryModel()
                {
                    Name = categoryGroup.Key,
                    Fields = categoryGroup.Select(y => new CategoryField()
                    {
                        Id = y.CategoryField.Id,
                        Name = y.CategoryField.Name,
                        Description = y.CategoryField.Description, 
                        Mandatory = y.Mandatory
                    }).OrderBy(x=> x.Name).OrderBy(x=> x.Id).ToList()
                };

                this.Categories.Add(category);
                //var elements = categoryGroup[categoryGroup.Key];
            }

        }
    }
}