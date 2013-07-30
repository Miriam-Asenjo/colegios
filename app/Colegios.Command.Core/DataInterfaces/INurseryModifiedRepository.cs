using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Colegios.Command.Core.Domain;
using SharpArch.NHibernate;

namespace Colegios.Command.Core.DataInterfaces
{
    public interface INurseryModifiedRepository 
    {
        void RegisterNursery(NurseryModified nursery, int townId, int districtId, IDictionary<int, string> categoryValues);
    }
}
