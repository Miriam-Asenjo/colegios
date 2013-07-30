using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Colegios.Query.Core.DTOs;
using FluentNHibernate.Mapping;

namespace Colegios.Query.NHibernateMaps.ClassMaps
{
    [Serializable]
    public class CategoryFieldDtoMap: ClassMap<CategoryFieldDto>
    {
        public CategoryFieldDtoMap()
        {
            Table("category_fields");

            Id(x=>x.Id, "id");
            Map(x => x.Name, "Name").Length(45);
            Map(x => x.Description, "Description").Not.Nullable().Length(500);
            References(x => x.Category, "categoryId");


        }
    }
}
