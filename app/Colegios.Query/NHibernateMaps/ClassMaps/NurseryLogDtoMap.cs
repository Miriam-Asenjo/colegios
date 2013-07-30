using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;
using Colegios.Query.Core.DTOs;

namespace Colegios.Query.NHibernateMaps.ClassMaps
{
    public class NurseryLogDtoMap : ClassMap<NurseryLogDto>
    {
        public NurseryLogDtoMap()
        {
            Table("nursery_log");
            Id(x => x.Id, "Id");
            References<NurseryModifiedDto>(x => x.NurseryModified, "nurseryModifiedId");
            Map(x => x.UpdatedOn, "updatedOn").Not.Nullable();
        }
    }
}
