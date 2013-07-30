using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;
using Colegios.Command.Core.Domain;
using Colegios.Command.Core.Domain.Lookups;


namespace Colegios.Command.Data.NHibernateMaps.ClassMaps
{
    public class UserMap: ClassMap<User>
    {
        public UserMap()
        {
            Table("users");
            Id(x => x.Id, "Id");

            Map(x => x.UserName, "UserName").Not.Nullable().Length(32);
            Map(x => x.HashPassword, "HashPassword").Not.Nullable().Length(128);
            Map(x => x.Email, "Email").Not.Nullable().Length(320);
            Map(x => x.UserType, "typeId").CustomType<UserType>();
        }
    }
}
