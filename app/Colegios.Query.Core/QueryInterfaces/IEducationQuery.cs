using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Colegios.Query.Core.DTOs;

namespace Colegios.Query.Core.QueryInterfaces
{
    public interface IEducationQuery
    {
        IEnumerable<EducationTypeDto> GetTypeEducations();
    }
}
