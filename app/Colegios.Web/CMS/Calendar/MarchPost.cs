using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using N2;

namespace Colegios.Web.CMS.Calendar
{
    [PageDefinition("March", Name = "MarPost", SortOrder = 3)]
    public class MarchPost : MonthPosts
    {
        public override string Title
        {
            get { return "Marzo"; }
        }
    }
}