using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using N2;

namespace Colegios.Web.CMS.Calendar
{
    [PageDefinition("June", Name = "JunPost", SortOrder = 6)]
    public class JunePost : MonthPosts
    {
        public override string Title
        {
            get { return "Junio"; }
        }
    }
}