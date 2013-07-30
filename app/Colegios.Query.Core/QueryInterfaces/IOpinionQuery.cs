using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Colegios.Query.Core.QueryInterfaces
{
    public interface IOpinionQuery
    {
        void saveOpinion(string userName, string email, string opinion);
    }
}
