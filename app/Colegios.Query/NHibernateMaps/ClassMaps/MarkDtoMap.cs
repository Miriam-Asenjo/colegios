using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;
using Colegios.Query.Core.DTOs;

namespace Colegios.Query.NHibernateMaps.ClassMaps
{
    public class MarkDtoMap: ClassMap<MarkDto>
    {
        public MarkDtoMap()
        {
            Table("Marks");

            Id(x => x.Id, "Id");

            Map(x => x.AverageMark, "AverageMark").Not.Nullable();
            Map(x => x.TotalPercentageSuccess, "TotalPercentageSuccess").Not.Nullable();
            Map(x => x.LanguageTotalPercentageSuccess, "LanguageTotalPercentageSuccess").Not.Nullable();
            Map(x => x.CultureTotalPercentageSuccess, "CultureTotalPercentageSuccess");
            Map(x => x.MathsTotalPercentageSuccess, "MathsTotalPercentageSuccess").Not.Nullable();
            Map(x => x.YearExam, "YearExam").Not.Nullable();
            Map(x => x.TotalStudents, "TotalStudents");

            //Many to one association
            References<SchoolDto>(x => x.School,"schoolId").LazyLoad();

        } 
    }
}
