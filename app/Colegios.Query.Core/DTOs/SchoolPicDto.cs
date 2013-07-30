using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Colegios.Query.Core.DTOs
{
    [Serializable]
    public class SchoolPicDto
    {
        public virtual int Id { get; set; }
        public virtual string PictureName { get; set; }
    }
}
