using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Colegios.Command.Core.Domain;

namespace Colegios.ApplicationServices.Interfaces
{
    public interface INurseryService
    {
        int NurserySignUp(NurseryModified nursery, int townId, int districtId, IDictionary<int, string> categoryValues);
    }
}
