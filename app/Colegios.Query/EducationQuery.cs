using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Colegios.Query.Core.QueryInterfaces;
using Colegios.Query.Core.DTOs;
using SharpArch.NHibernate;

namespace Colegios.Query
{
    [SessionFactoryAttribute(Constants.QueryFactoryKey)]
    public class EducationQuery : NHibernateQuery, IEducationQuery
    {
        public IEnumerable<EducationTypeDto> GetTypeEducations()
        {
            return Session.QueryOver<EducationTypeDto>().List();
        }
    }
}
