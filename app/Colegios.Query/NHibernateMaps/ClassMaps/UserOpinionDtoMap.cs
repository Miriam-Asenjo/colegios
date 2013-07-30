using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Colegios.Query.Core.DTOs;
using FluentNHibernate.Mapping;

namespace Colegios.Query.NHibernateMaps.ClassMaps
{
    [Serializable]
    public class UserOpinionDtoMap: ClassMap<UserOpinionDto>
    {
        public UserOpinionDtoMap()
        {
            Table("user_opinion");
            Id(x => x.Id, "Id").GeneratedBy.Increment();;

            Map(x => x.UserName, "UserName").Not.Nullable().Length(128);
            Map(x => x.Email, "Email").Nullable().Length(256);
            Map(x => x.Opinion, "Opinion").Not.Nullable().Length(2048);
            Map(x => x.CreationDate, "CreationDate").Not.Nullable();

        }

    }
}
