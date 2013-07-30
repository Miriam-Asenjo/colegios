using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using N2;

namespace Colegios.Web.CMS.Calendar
{
    [PageDefinition("September", Name = "SepPost", SortOrder = 9)]
    public class SeptemberPost : MonthPosts
    {
        public override string Title
        {
            get { return "Septiembre"; }
        }
    }
}