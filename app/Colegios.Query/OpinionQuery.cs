using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpArch.NHibernate;
using Colegios.Query.Core.QueryInterfaces;
using Colegios.Query.Core.DTOs;

namespace Colegios.Query
{
    [SessionFactoryAttribute(Constants.QueryFactoryKey)]
    public class OpinionQuery: NHibernateRepository<UserOpinionDto>, IOpinionQuery
    {
        public void saveOpinion(string userName, string email, string opinion)
        {
            var op = new UserOpinionDto() { UserName = userName, Email = email, Opinion = opinion, CreationDate = DateTime.Now};
            base.Save(op);
        }
    }
}
