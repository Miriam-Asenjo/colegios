using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;
using Colegios.Command.Core.Domain;


namespace Colegios.Command.Data.NHibernateMaps.ClassMaps
{
    public class NurseryDetailsModifiedMap: ClassMap<NurseryDetailsModified>
    {
        public NurseryDetailsModifiedMap()
        {
            Table("nursery_details_modified");

            Id(x => x.Id, "Id");
            Map(x => x.Value, "Value").Not.Nullable().Length(1000);
            //Many to One
            References<NurseryModified>(x => x.Nursery, "nurseryId");
            //Many to One
            References<CategoryField>(x => x.CategoryField, "categoryFieldId");
        }
    }
}
