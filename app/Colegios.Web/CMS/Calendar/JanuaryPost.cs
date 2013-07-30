using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using N2;
using N2.Details;
using N2.Integrity;

namespace Colegios.Web.CMS.Calendar
{
    //[RestrictParents(typeof(Year))]
    [WithEditableName, WithEditableTitle]
    [PageDefinition("January", Name = "JanPost", SortOrder = 1)]
    public class JanuaryPost: MonthPosts
    {
        public override string Title
        {
            get { return "Enero"; }
        }
    }
}