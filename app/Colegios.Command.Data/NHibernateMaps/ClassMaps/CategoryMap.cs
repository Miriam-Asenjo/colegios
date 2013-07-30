using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using FluentNHibernate.Mapping;
using Colegios.Command.Core.Domain;

namespace Colegios.Command.Data.NHibernateMaps.ClassMaps
{
    [Serializable]
    public class CategoryMap : ClassMap<Category>
    {

        public CategoryMap () {

            Table("lookup_categories");

            Id(x => x.Id, "Id");
            Map(x => x.Name, "categoryName").Not.Nullable().Length(45);
        }
        

    }
}
