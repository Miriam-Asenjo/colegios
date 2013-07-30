using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Colegios.Query.Core.Criteria;
using Colegios.Query.Core.DTOs;

namespace Colegios.Query.Core.QueryInterfaces
{
    public interface INurseryQuery
    {
        IEnumerable<NurseryDto> FindNurseries(NurseryCriteria criteria, int pageSize, out int totalCount, out IList<HighLightNurseryDto> highLightedNurseries, int highLightedNum, int pageNo = 1);
        NurseryDto GetNursery(int nurseryId);
        IEnumerable<NurseryDto> DistrictNurseries(int localityId);
        IList<CategoryFieldInstitutionTypeDto> GetCategoryFields();
    }
}
