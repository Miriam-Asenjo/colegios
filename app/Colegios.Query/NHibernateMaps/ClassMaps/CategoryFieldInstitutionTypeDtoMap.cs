using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Colegios.Query.Core.DTOs;
using FluentNHibernate.Mapping;
using Colegios.Query.Core.Entities.Lookups;

namespace Colegios.Query.NHibernateMaps.ClassMaps
{
    [Serializable]
    public class CategoryFieldInstitutionTypeDtoMap: ClassMap<CategoryFieldInstitutionTypeDto>
    {
        public CategoryFieldInstitutionTypeDtoMap()
        {
            Table("category_details_institution");
            Id(x => x.Id);
            References(x => x.CategoryField, "categoryFieldId");
            Map(x => x.InstitutionType, "institutiontypeid").CustomType<InstitutionType>();
            Map(x => x.Mandatory);
        }
    }
}
