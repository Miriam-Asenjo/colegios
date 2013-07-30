using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Colegios.Query.Core.DTOs
{
    [Serializable]
    public class TownDto
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual CityDto City { get; set; }
        public virtual IEnumerable<LocalityDto> Locations{get; set;}
    }
}
