using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;
using Colegios.Query.Core.DTOs;

namespace Colegios.Query.NHibernateMaps.ClassMaps
{
    public class UserDtoMap: ClassMap<UserDto>
    {
        public UserDtoMap()
        {
            Table("users");
            Id(x => x.Id, "Id");

            Map(x => x.UserName, "UserName").Not.Nullable().Length(32);
            Map(x => x.Password, "Password").Not.Nullable().Length(128);
            Map(x => x.HashSalt, "HashSalt").Not.Nullable().Length(128);
            Map(x => x.Email, "Email").Not.Nullable().Length(320);
        }
    }
}
