using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Colegios.Query.Core.DTOs;
using FluentNHibernate.Mapping;

namespace Colegios.Query.NHibernateMaps.ClassMaps
{
    [Serializable]
    public class CategoryDtoMap : ClassMap<CategoryDto>
    {

        public CategoryDtoMap () {

            Table("lookup_categories");
            Id(x => x.Id, "Id");
            Map(x => x.Name, "categoryName").Not.Nullable().Length(45);
        }
        

    }
}
