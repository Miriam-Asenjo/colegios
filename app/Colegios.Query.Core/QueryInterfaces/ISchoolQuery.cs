using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Colegios.Query.Core.DTOs;
using Colegios.Query.Core.Criteria;

namespace Colegios.Query.Core.QueryInterfaces
{
    public interface ISchoolQuery 
    {
        IEnumerable<SchoolDto> FindSchools(SchoolCriteria criteria, int pageSize, out int totalCount, out IList<HighLightSchoolDto> highLightedSchools, int highLightedNum, int pageNo = 1);
        SchoolDto GetSchool(int schoolId);
        IEnumerable<SchoolDto> Ranking(int year, int markType, int? tipoCentro, int townId, int[] localities, out int numTotalSchools, int pageNo = 1, int pageSize = 50);
        IEnumerable<SchoolDto> DistrictSchools(int localityId);
    }
}
