using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;
using Colegios.Query.Core.DTOs;

namespace Colegios.Query.NHibernateMaps
{
    [Serializable]
    public class SchoolPicDtoMap: ClassMap<SchoolPicDto>
    {
        public SchoolPicDtoMap()
        {
            Table("school_pics");
            Id(x => x.Id, "id");
            Map(x => x.PictureName, "PictureName").Not.Nullable().Length(255);

        }
    }
}
