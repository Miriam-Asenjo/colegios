using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;
using Colegios.Query.Core.DTOs;

namespace Colegios.Query.NHibernateMaps.ClassMaps
{
    public class SchoolDetailsDtoMap: ClassMap<SchoolDetailsDto>
    {
        public SchoolDetailsDtoMap()
        {
            Table("school_details");

            Id(x => x.Id, "id");
            Map(x => x.Value, "Value").Not.Nullable().Length(1000);
            //Many to One
            References<SchoolDto>(x => x.School, "schoolId");
            //Many to One
            References<CategoryFieldDto>(x => x.CategoryField, "categoryFieldId");


        }
    }
}
