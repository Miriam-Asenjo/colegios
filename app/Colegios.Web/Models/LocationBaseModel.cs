using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Colegios.Query.Core.DTOs;

namespace Colegios.Web.Models
{
    public class SearchBaseModel
    {
        public SearchBaseModel(IEnumerable<CityDto> cities, Dictionary<string, IEnumerable<TownDto>> towns, Dictionary<string, IEnumerable<LocalityDto>> districts, IEnumerable<EducationTypeDto> educationTypes)
        {
            this.Cities = cities;
            this.Towns = towns;
            this.Districts = districts;
            this.EducationTypes = educationTypes;
        }

        //public SchoolSearchViewModel SearchModel { get; set; }

        public IEnumerable<CityDto> Cities { get; set; }
        public Dictionary<string, IEnumerable<TownDto>> Towns { get; set; }
        public Dictionary<string, IEnumerable<LocalityDto>> Districts { get; set; }
        public IEnumerable<EducationTypeDto> EducationTypes { get; set; }


    }

}