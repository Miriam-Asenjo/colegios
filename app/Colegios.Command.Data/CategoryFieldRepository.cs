using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpArch.NHibernate;
using Colegios.Command.Core.Domain;

namespace Colegios.Command.Data
{
    [SessionFactoryAttribute(Constants.QueryFactoryKey)]
    public class CategoryFieldRepository : NHibernateRepository<CategoryField>
    {
    }
}
