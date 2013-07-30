using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Colegios.Command.Core.DataInterfaces
{
    public interface IPostRepository
    {
        void SaveOrUpdate(int postId);
    }
}
