using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Colegios.Query.Core.DTOs;
using FluentNHibernate.Mapping;

namespace Colegios.Query.NHibernateMaps.ClassMaps
{
    public class EducationDtoMap : ClassMap<EducationTypeDto>
    {
        public EducationDtoMap()
        {
            Table("lookup_educationtype");
            Id(x => x.Id,"Id");

            Map(x => x.Name, "Name").Not.Nullable().Length(255);
            Map(x => x.Enabled, "Enabled").Not.Nullable();

            Map(x => x.IsNursery, "Nursery").Not.Nullable();
            Map(x => x.IsSchool, "School").Not.Nullable();

        }
    }
}
