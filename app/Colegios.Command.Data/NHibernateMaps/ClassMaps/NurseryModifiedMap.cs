using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;
using Colegios.Command.Core.Domain;
using Colegios.Command.Core.Domain.Lookups;

namespace Colegios.Command.Data.NHibernateMaps.ClassMaps
{
    public class NurseryModifiedMap: ClassMap<NurseryModified>
    {
        public NurseryModifiedMap (){
            Table("nurseries_modified");

            Id(x => x.Id, "Id");

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

            Map(x => x.Type, "type").CustomType<EducationTypeNursery>();

            //Many to One
            References<Nursery>(x => x.Nursery, "nurseryId").Nullable();

            //Many to One
            References<Locality>(x => x.Locality, "localityId");
            //One to Many
            HasMany<NurseryDetailsModified>(x => x.Details).KeyColumn("nurseryId").Inverse().LazyLoad();
            References<User>(x => x.User, "UserId").Not.Nullable().Cascade.SaveUpdate().Insert().Update();
        }
    }
}
