using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Colegios.Query.Core.DTOs
{
    [Serializable]
    public class PostReadDto
    {
        public virtual int PostId { get; set; }
        public virtual int Count { get; set; }
        public virtual bool Enabled { get; set; }
    }
}
