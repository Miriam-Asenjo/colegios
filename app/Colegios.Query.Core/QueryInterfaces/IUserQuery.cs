using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Colegios.Query.Core.QueryInterfaces
{
    public interface IUserQuery
    {
        bool IsUserNameAvailable (string userName);
        bool IsUserEmailAvailable(string email);
    }
}
