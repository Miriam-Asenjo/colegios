using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Colegios.Query.Core.DTOs;
using FluentNHibernate.Mapping;

namespace Colegios.Query.NHibernateMaps.ClassMaps
{
    public class NurseryPicDtoMap: ClassMap<NurseryPicDto>
    {
        public NurseryPicDtoMap()
        {
            Table("nursery_pics");

            Id(x=> x.Id, "id");
            Map(x => x.PictureName, "pictureName").Not.Nullable().Length(255);

        }
    }
}
