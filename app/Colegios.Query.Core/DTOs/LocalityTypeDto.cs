using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Colegios.Query.Core.DTOs
{
    [Serializable]
    public class LocalityTypeDto
    {
        public virtual int Id {get; set;}
        public virtual string Name { get; set; }
    }
}
