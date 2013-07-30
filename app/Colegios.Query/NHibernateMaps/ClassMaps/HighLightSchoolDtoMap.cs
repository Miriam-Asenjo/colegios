using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Colegios.Query.Core.DTOs;
using FluentNHibernate.Mapping;

namespace Colegios.Query.NHibernateMaps.ClassMaps
{
    public class HighLightSchoolDtoMap: ClassMap<HighLightSchoolDto>
    {
        public HighLightSchoolDtoMap(){
            Table("highlight_schools");

            Id(x => x.Id, "Id");
            Map(x => x.StartDate, "startDate").Not.Nullable();
            Map(x => x.EndDate, "endDate").Not.Nullable();
            Map(x => x.LogoName, "logoName");
            References(x => x.School, "schoolId");
            
        }

    }
}
