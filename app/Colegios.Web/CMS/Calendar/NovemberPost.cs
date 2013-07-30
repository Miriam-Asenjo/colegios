using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using N2;

namespace Colegios.Web.CMS.Calendar
{
    [PageDefinition("November", Name = "NovPost", SortOrder = 11)]
    public class NovemberPost : MonthPosts
    {
        public override string Title
        {
            get { return "Noviembre"; }
        }
    }
}