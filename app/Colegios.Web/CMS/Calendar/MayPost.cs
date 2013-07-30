using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using N2;

namespace Colegios.Web.CMS.Calendar
{
    [PageDefinition("May", Name = "MayPost", SortOrder = 5)]
    public class MayPost : MonthPosts
    {
        public override string Title
        {
            get { return "Mayo"; }
        }
    }
}