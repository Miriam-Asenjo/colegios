using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Colegios.Query.Core.DTOs
{
    [Serializable]
    public class MarkDto
    {
        public virtual int Id { get; set; }
        public virtual decimal AverageMark { get; set; }
        public virtual decimal TotalPercentageSuccess { get; set; }
        public virtual decimal LanguageTotalPercentageSuccess { get; set; }
        public virtual decimal? CultureTotalPercentageSuccess { get; set; }
        public virtual decimal? MathsTotalPercentageSuccess { get; set; }
        public virtual int YearExam { get; set; }
        public virtual int? TotalStudents { get; set; }
        public virtual SchoolDto School { get; set; }
    }
}
