using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;
using Colegios.Command.Core.Domain;

namespace Colegios.Command.Data.NHibernateMaps.ClassMaps
{
    public class NurseryMap: ClassMap<Nursery>
    {
        public NurseryMap()
        {
            Table("Nurseries");
            Id (x=> x.Id, "Id");

            Map(x => x.Name, "name").Not.Nullable().Length(100);
            Map(x => x.Address, "address").Not.Nullable().Length(100);
            Map(x => x.PostCode, "postcode").Not.Nullable();
            Map(x => x.Phone, "phone").Length(45);
            Map(x => x.Fax, "fax").Length(45);
            Map(x => x.Email, "email").Length(100);
            Map(x => x.Web, "web").Length(256);
            Map(x => x.Latitude, "latitude");
            Map(x => x.Longitude, "longitude");
            Map(x => x.IsBilingue, "bilingue");

            //Many to One
            References<EducationType>(x => x.Type, "type");
            //Many to One
            References<Locality>(x => x.Locality, "localityId");


        }
    }
}
