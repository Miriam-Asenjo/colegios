using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpArch.Domain.DomainModel;

namespace Colegios.Command.Core.Domain
{
    [Serializable]
    public class Category: Entity
    {
        public virtual string Name { get; set; }
    }
}
