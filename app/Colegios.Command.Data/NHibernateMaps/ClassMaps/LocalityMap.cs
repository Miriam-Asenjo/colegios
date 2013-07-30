using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;
using Colegios.Query.Core.DTOs;
using Colegios.Command.Core.Domain;

namespace Colegios.Command.Data.NHibernateMaps.ClassMaps
{
    [Serializable]
    public class LocalityMap: ClassMap<Locality>
    {
        public LocalityMap()
        {
            Table("lookup_localities");

            Id(x => x.Id, "Id");
            Map(x => x.Name, "Name");

        }
    }
}
