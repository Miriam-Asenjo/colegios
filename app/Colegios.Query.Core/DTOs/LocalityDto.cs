using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Colegios.Query.Core.DTOs
{
    [Serializable]
    public class LocalityDto
    {
        public virtual int Id {get;set;}
        public virtual string Name {get; set;}
        public virtual TownDto Town { get; set; }
        public virtual LocalityTypeDto Type { get; set; }
    }
}
