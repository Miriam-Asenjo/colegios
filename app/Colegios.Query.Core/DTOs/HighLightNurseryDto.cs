using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Colegios.Query.Core.DTOs
{
    [Serializable]
    public class HighLightNurseryDto
    {
        public virtual int Id { get; set; }
        public virtual DateTime StartDate { get; set; }
        public virtual DateTime EndDate { get; set; }
        public virtual NurseryDto Nursery { get; set; }
        public virtual string LogoName { get; set; }
    }
}
