using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpArch.Domain.DomainModel;

namespace Colegios.Command.Core.Domain
{
    [Serializable]
    public class EducationType: Entity
    {
        public virtual string Name { get; set; }
        public virtual bool Enabled { get; set; }
        public virtual bool IsSchool { get; set; }
        public virtual bool IsNursery { get; set; }
    }
}
