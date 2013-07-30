using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Colegios.Query.Core.Criteria
{
    public class NurseryCriteria
    {
        public int[] Localities { get; set; }
        public int TownId { get; set; }
        public string Keywords { get; set; }
        public int? EducationType { get; set; }
        public bool IsBilingue { get; set; }

        public NurseryCriteria(int[] localities, int townId, string keywords, int? educationType, bool isBilingue)
        {
            Localities = localities;
            TownId = townId;
            Keywords = keywords;
            EducationType = educationType;
            IsBilingue = isBilingue;

        }
    }
}
