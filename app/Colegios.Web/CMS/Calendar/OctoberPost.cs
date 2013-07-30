using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using N2;

namespace Colegios.Web.CMS.Calendar
{
    [PageDefinition("October", Name = "OctPost", SortOrder = 10)]
    public class OctoberPost : MonthPosts
    {
        public override string Title
        {
            get { return "Octubre"; }
        }
    }
}