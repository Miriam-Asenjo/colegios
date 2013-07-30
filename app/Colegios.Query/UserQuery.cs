using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Colegios.Query.Core.QueryInterfaces;
using SharpArch.NHibernate;

namespace Colegios.Query
{
    [SessionFactoryAttribute(Constants.QueryFactoryKey)]
    public class UserQuery: NHibernateQuery, IUserQuery
    {
        public bool IsUserNameAvailable(string userName)
        {
            var query = Session.CreateSQLQuery(@"select count(1) from Users where lower(UserName) like :name");
            var numUsers = query.SetString("name", userName.ToLower()).UniqueResult<long>();
            return (numUsers == 0);
        }

        public bool IsUserEmailAvailable(string email)
        {
            var query = Session.CreateQuery("select count(1) from Users where lower(Email) like :email");
            var numUsers = query.SetString("email", email.ToLower()).UniqueResult<long>();
            return (numUsers == 0);
        }
    }
}
