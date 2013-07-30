using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using N2;

namespace Colegios.Web.CMS.Calendar
{
    [PageDefinition("August", Name = "AugPost", SortOrder = 8)]
    public class AugustPost : MonthPosts
    {
        public override string Title
        {
            get { return "Agosto"; }
        }
    }
}