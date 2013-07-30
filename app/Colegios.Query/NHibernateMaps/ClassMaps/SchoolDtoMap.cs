using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Colegios.Query.Core.DTOs;
using FluentNHibernate.Mapping;

namespace Colegios.Query.NHibernateMaps.ClassMaps
{
    [Serializable]
    public class SchoolDtoMap: ClassMap<SchoolDto>
    {
        public SchoolDtoMap()
        {
            Table("schools");
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
            Map(x => x.OpenDay, "openday");
            Map(x => x.isBilingue, "bilingue");
            Map(x => x.ContinuousTime, "continuousTime");
            Map(x => x.Twitter, "twitter");
            Map(x => x.Video, "video");
            Map(x => x.YoutubeId, "youtubeid");
            Map(x => x.PicTitle, "picTitle");
            Map(x => x.PicName, "picName");

            //Many to One
            References<EducationTypeDto>(x=> x.Type, "type");
            //Many to One
            References<NurseryDto>(x => x.Nursery, "nurseryId");
            //Many to One
            References<LocalityDto>(x => x.Locality, "localityId");
            //One to Many
            HasMany<SchoolDetailsDto>(x => x.Details).KeyColumn("schoolId").Inverse().LazyLoad();
            
            //One to Many
            HasMany<MarkDto>(x => x.Marks).KeyColumn("schoolId").Inverse().LazyLoad();

            //One to Many
            HasMany<MarkLeaDto>(x => x.MarksLea).KeyColumn("schoolId").Inverse().LazyLoad();
            //One to Many
            HasMany<HighLightSchoolDto>(x => x.HighLightedSchool).KeyColumn("schoolId").Not.Inverse().LazyLoad();
            //One to Many
            HasMany<SchoolPicDto>(x => x.Pics).KeyColumn("schoolId").Not.Inverse().LazyLoad();

        }

    }
}
