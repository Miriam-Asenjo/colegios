using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;
using Colegios.Query.Core.DTOs;

namespace Colegios.Query.NHibernateMaps.ClassMaps
{
    public class NurseryDtoMap: ClassMap<NurseryDto>
    {
        public NurseryDtoMap()
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
            Map(x => x.YoutubeId, "youtubeid").Nullable();

            //Many to One
            References<EducationTypeDto>(x => x.Type, "type");
            //Many to One
            References<LocalityDto>(x => x.Locality, "localityId");
            //One to Many
            HasMany<NurseryDetailsDto>(x => x.Details).KeyColumn("nurseryId").Inverse().LazyLoad();
            //One to Many
            HasMany<HighLightNurseryDto>(x => x.HighLightedNursery).KeyColumn("nurseryId").Not.Inverse().LazyLoad();
            //One to Many
            HasMany<NurseryPicDto>(x => x.Pics).KeyColumn("nurseryId").Not.Inverse().LazyLoad();


        }
    }
}
