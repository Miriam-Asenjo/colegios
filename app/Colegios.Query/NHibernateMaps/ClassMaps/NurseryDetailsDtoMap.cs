using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;
using Colegios.Query.Core.DTOs;

namespace Colegios.Query.NHibernateMaps.ClassMaps
{
    public class NurseryDetailsDtoMap: ClassMap<NurseryDetailsDto>
    {
        public NurseryDetailsDtoMap()
        {
            Table("Nursery_Details");

            Id(x => x.Id, "id");
            Map(x => x.Value, "Value").Not.Nullable().Length(1000);
            //Many to One
            References<NurseryDto>(x => x.Nursery, "nurseryId");
            //Many to One
            References<CategoryFieldDto>(x => x.CategoryField, "categoryFieldId");


        }
    }
}
