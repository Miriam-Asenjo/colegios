using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;
using Colegios.Query.Core.DTOs;

namespace Colegios.Query.NHibernateMaps.ClassMaps
{
    public class NurseryDetailsModifiedDtoMap: ClassMap<NurseryDetailsModifiedDto>
    {
        public NurseryDetailsModifiedDtoMap()
        {
            Table("nursery_details_modified");

            Id(x => x.Id, "Id");
            Map(x => x.Value, "Value").Not.Nullable().Length(1000);
            //Many to One
            References<NurseryModifiedDto>(x => x.Nursery, "nurseryId");
            //Many to One
            References<CategoryFieldDto>(x => x.CategoryField, "categoryFieldId");
        }
    }
}
