using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpArch.Domain.DomainModel;

namespace Colegios.Command.Core.Domain
{
    [Serializable]
    public class NurseryDetailsModified: Entity
    {
        public virtual NurseryModified Nursery { get; set; }
        public virtual CategoryField CategoryField { get; set; }
        public virtual string Value { get; set; }
    }
}
