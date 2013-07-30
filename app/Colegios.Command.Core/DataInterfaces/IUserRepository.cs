using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpArch.NHibernate.Contracts.Repositories;
using Colegios.Command.Core.Domain;

namespace Colegios.Command.Core.DataInterfaces
{
    public interface IUserRepository: INHibernateRepository<User>
    {

    }
}
