using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using N2;

namespace Colegios.Web.CMS.Calendar
{
    [PageDefinition("April", Name = "ApPost", SortOrder = 4)]
    public class AprilPost : MonthPosts
    {
        public override string Title
        {
            get { return "Abril"; }
        }
    }
}