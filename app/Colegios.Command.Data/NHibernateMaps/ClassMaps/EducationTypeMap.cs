using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Colegios.Query.Core.DTOs;
using FluentNHibernate.Mapping;
using Colegios.Command.Core.Domain;

namespace Colegios.Command.Data.NHibernateMaps.ClassMaps
{
    public class EducationTypeMap : ClassMap<EducationType>
    {
        public EducationTypeMap()
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
