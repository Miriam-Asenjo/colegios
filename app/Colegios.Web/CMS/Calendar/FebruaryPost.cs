using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using N2;

namespace Colegios.Web.CMS.Calendar
{
    [PageDefinition("February", Name = "FebPost", SortOrder = 2)]
    public class FebruaryPost: MonthPosts
    {
        public override string Title
        {
            get { return "Febrero"; }
        }
    }
}