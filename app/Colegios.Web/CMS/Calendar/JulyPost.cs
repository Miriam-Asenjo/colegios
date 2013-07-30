using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using N2;

namespace Colegios.Web.CMS.Calendar
{
    [PageDefinition("July", Name = "JulPost", SortOrder = 7)]
    public class JulyPost: MonthPosts
    {
        public override string Title
        {
            get { return "Julio"; }
        }
    }
}