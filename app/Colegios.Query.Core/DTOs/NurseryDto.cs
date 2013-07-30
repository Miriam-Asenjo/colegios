using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Colegios.Query.Core.DTOs
{
    [Serializable]
    public class NurseryDto
    {
        public virtual int Id { get; set; }
        public virtual EducationTypeDto Type { get; set; }
        public virtual String Name { get; set; }
        public virtual String Address { get; set; }
        public virtual int PostCode { get; set; }
        public virtual String Phone { get; set; }
        public virtual String Fax { get; set; }
        public virtual String Email { get; set; }
        public virtual String Web { get; set; }
        public virtual LocalityDto Locality { get; set; }
        public virtual decimal Latitude { get; set; }
        public virtual decimal Longitude { get; set; }
        public virtual string OpenDay { get; set; }
        public virtual IList<NurseryDetailsDto> Details { get; set; }
        public virtual IList<NurseryPicDto> Pics { get; set; }
        public virtual bool IsBilingue { get; set; }
        public virtual string YoutubeId { get; set; }
        public virtual IList<HighLightNurseryDto> HighLightedNursery { get; set; }
    }
}
