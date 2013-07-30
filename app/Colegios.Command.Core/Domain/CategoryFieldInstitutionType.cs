using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Colegios.Command.Core.Domain.Lookups;
using SharpArch.Domain.DomainModel;

namespace Colegios.Command.Core.Domain
{
    [Serializable]
    public class CategoryFieldInstitutionType: Entity
    {
        public virtual CategoryField CategoryField {get;set;}
        public virtual InstitutionType InstitutionType { get; set; }
        public virtual bool Mandatory { get; set; }
    }
}
