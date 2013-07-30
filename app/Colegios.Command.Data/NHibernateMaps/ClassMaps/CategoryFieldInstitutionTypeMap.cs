using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;
using Colegios.Command.Core.Domain;
using Colegios.Command.Core.Domain.Lookups;

namespace Colegios.Command.Data.NHibernateMaps.ClassMaps
{
    [Serializable]
    public class CategoryFieldInstitutionTypeMap: ClassMap<CategoryFieldInstitutionType>
    {
        public CategoryFieldInstitutionTypeMap()
        {
            Table("category_details_institution");
            Id(x => x.Id);
            References(x => x.CategoryField, "categoryFieldId");
            Map(x => x.InstitutionType, "institutiontypeid").CustomType<InstitutionType>();
            Map(x => x.Mandatory);
        }
    }
}
