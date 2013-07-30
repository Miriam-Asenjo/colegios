using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Colegios.Query.Core.DTOs
{
    [Serializable]
    public class SchoolDto
    {
        public virtual int Id { get; set; }
        public virtual EducationTypeDto Type { get; set; }
        public virtual string Name { get; set; }
        public virtual string Address { get; set; }
        public virtual int PostCode { get; set; }
        public virtual string Phone { get; set; }
        public virtual string Fax { get; set; }
        public virtual string Email { get; set; }
        public virtual string Web { get; set; }
        public virtual decimal Latitude { get; set; }
        public virtual decimal Longitude { get; set; }
        public virtual string OpenDay { get; set; }
        public virtual bool isBilingue { get; set; }
        public virtual bool ContinuousTime { get; set; }
        public virtual string Twitter { get; set; }
        public virtual string Video { get; set; }
        public virtual string YoutubeId { get; set; }
        public virtual string PicTitle { get; set; }
        public virtual string PicName { get; set; }
        public virtual LocalityDto Locality { get; set; }
        public virtual IList<SchoolDetailsDto> Details { get; set; }
        public virtual IList<SchoolPicDto> Pics { get; set; }
        public virtual IList<MarkDto> Marks { get; set; }
        public virtual IList<MarkLeaDto> MarksLea { get; set; }
        public virtual IList<HighLightSchoolDto> HighLightedSchool { get; set; }
        public virtual NurseryDto Nursery { get; set; }

    }
}
