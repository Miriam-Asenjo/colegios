using System;

namespace Colegios.Query.Core.DTOs
{
    [Serializable]
    public class CityDto
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        
    }
}
