using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Colegios.Query.Core.DTOs
{
    [Serializable]
    public class MarkLeaDto
    {
        public virtual int Id { get; set; }
        public virtual decimal TotalAverageMark { get; set; }
        public virtual decimal AverageMarkMaths { get; set; }
        public virtual decimal AverageMarkLanguage { get; set; }
        public virtual int YearExam { get; set; }
        public virtual SchoolDto School { get; set; }
    }
}
