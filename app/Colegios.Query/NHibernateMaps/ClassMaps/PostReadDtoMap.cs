using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;
using Colegios.Query.Core.DTOs;

namespace Colegios.Query.NHibernateMaps.ClassMaps
{
    public class PostReadDtoMap: ClassMap<PostReadDto>
    {
        public PostReadDtoMap()
        {
            Table("post_reads");
            Id(x => x.PostId, "PostId").GeneratedBy.Assigned();

            Map(x => x.Count, "Count").Nullable();
            Map(x => x.Enabled, "Enabled").Not.Nullable();
        }
    }
}
