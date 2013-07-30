using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;
using Colegios.Query.Core.DTOs;

namespace Colegios.Query.NHibernateMaps.ClassMaps
{
    [Serializable]
    public class LocalityDtoMap: ClassMap<LocalityDto>
    {
        public LocalityDtoMap()
        {
            Table("lookup_localities");

            Id(x => x.Id, "Id");
            Map(x => x.Name, "Name");

            References<TownDto>(x => x.Town, "TownId");
            References<LocalityTypeDto>(x => x.Type, "TypeId");
        }
    }
}
