using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Colegios.Query.Core.DTOs;

namespace Colegios.Query.Core.Criteria
{
    public class SchoolCriteria
    {
        public int[] Localities { get; set; }
        public int TownId { get; set; }
        public string Keywords { get; set; }
        public int? EducationType { get; set; }
        public bool IsBilingue {get; set;}
        public bool NoBreak { get; set; }

        public SchoolCriteria(int[] localities,int townId, string keywords, int? educationType, bool isBilingue, bool noBreak)
        {
            Localities = localities;
            TownId = townId;
            Keywords = keywords;
            EducationType = educationType;
            IsBilingue = isBilingue;
            NoBreak = noBreak;

        }
    }
}
