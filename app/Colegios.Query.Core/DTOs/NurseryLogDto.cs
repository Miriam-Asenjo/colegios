using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Colegios.Query.Core.DTOs
{
    [Serializable]
    public class NurseryLogDto
    {
        public virtual int Id { get; set; }

        public virtual NurseryModifiedDto NurseryModified { get; set; }

        public virtual DateTime UpdatedOn { get; set; }


    }
}
