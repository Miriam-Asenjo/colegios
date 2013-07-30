using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;
using Colegios.Query.Core.DTOs;

namespace Colegios.Query.NHibernateMaps.ClassMaps
{
    [Serializable]
    public class TownDtoMap: ClassMap<TownDto>
    {
        public TownDtoMap () 
        {
            Table("lookup_towns");

            Id(x => x.Id, "Id");

            Map(x => x.Name, "Name").Not.Nullable().Length(255);

            References<CityDto>(x=> x.City,"CityId");

           
        }
        
    }
}
