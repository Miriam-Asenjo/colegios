using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;
using Colegios.Query.Core.DTOs;

namespace Colegios.Query.NHibernateMaps.ClassMaps
{
    public class LocalityTypeDtoMap: ClassMap<LocalityTypeDto>
    {
        public LocalityTypeDtoMap()
        {
            Table("lookup_localitytype");

            Id(x => x.Id, "Id");
            Map(x => x.Name, "Name");

        }
    }
}
