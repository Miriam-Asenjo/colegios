using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Colegios.Query.Core.DTOs
{
    [Serializable]
    public class EducationTypeDto
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual bool Enabled { get; set; }
        public virtual bool IsSchool { get; set; }
        public virtual bool IsNursery { get; set; }
    }
}
