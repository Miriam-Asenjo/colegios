using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Colegios.Query.Core.DTOs
{
    [Serializable]
    public class NurseryDetailsDto
    {
        public virtual int Id { get; set; }
        public virtual NurseryDto Nursery { get; set; }
        public virtual CategoryFieldDto CategoryField { get; set; }
        public virtual string Value { get; set; }
    }
}
