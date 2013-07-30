using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Colegios.Query.Core.Entities.Lookups;

namespace Colegios.Query.Core.DTOs
{
    [Serializable]
    public class CategoryFieldInstitutionTypeDto
    {
        public virtual int Id { get; set; }
        public virtual CategoryFieldDto CategoryField {get;set;}
        public virtual InstitutionType InstitutionType { get; set; }
        public virtual bool Mandatory { get; set; }
    }
}
