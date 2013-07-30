using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpArch.Domain.DomainModel;

namespace Colegios.Command.Core.Domain
{
    [Serializable]
    public class CategoryField: Entity
    {
        public virtual string Name { get; set; }
        public virtual string Description { get; set; }
        public virtual Category Category { get; set; }
    }
}
