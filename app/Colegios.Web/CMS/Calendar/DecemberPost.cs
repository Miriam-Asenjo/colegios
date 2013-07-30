using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using N2;

namespace Colegios.Web.CMS.Calendar
{
    [PageDefinition("December", Name = "DecPost", SortOrder = 12)]
    public class DecemberPost : MonthPosts
    {
        public override string Title
        {
            get { return "Diciembre"; }
        }
    }
}