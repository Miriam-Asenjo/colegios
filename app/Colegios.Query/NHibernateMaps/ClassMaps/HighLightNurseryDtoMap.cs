using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;
using Colegios.Query.Core.DTOs;

namespace Colegios.Query.NHibernateMaps.ClassMaps
{
    public class HighLightNurseryDtoMap: ClassMap<HighLightNurseryDto>
    {
        public HighLightNurseryDtoMap()
        {
            Table("highlight_nurseries");

            Id(x=> x.Id, "Id");
            Map(x => x.StartDate, "startDate").Not.Nullable();
            Map(x => x.EndDate, "endDate").Not.Nullable();
            Map(x => x.LogoName, "logoName");
            References(x => x.Nursery, "nurseryId");


        }
    }
}
