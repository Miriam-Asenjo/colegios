using Colegios.Query.Core.DTOs;
using FluentNHibernate.Conventions.Helpers;
using FluentNHibernate.Mapping;

namespace Colegios.Query.NHibernateMaps.ClassMaps
{
    public class CityDtoMap: ClassMap<CityDto>
    {

        public CityDtoMap ()
        {
            Table("lookup_cities");
            Id(x => x.Id, "Id");

            Map(x => x.Name, "Name").Not.Nullable().Length(255);

        }
        
    }
}
