using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;
using Colegios.Query.Core.DTOs;

namespace Colegios.Query.NHibernateMaps.ClassMaps
{
    public class MarkLeaDtoMap : ClassMap<MarkLeaDto>
    {
        public MarkLeaDtoMap()
        {
            Table("Marks_Lea");

            Id(x => x.Id, "Id");

            Map(x => x.TotalAverageMark, "TotalAverageMark").Not.Nullable();
            Map(x => x.AverageMarkMaths, "AverageMarkMaths").Not.Nullable();
            Map(x => x.AverageMarkLanguage, "AverageMarkLanguage").Not.Nullable();
            Map(x => x.YearExam, "YearExam").Not.Nullable();

            //Many to one association
            References<SchoolDto>(x => x.School, "schoolId").LazyLoad();

        }
    }
}
