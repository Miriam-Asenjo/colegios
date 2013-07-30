using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using N2;
using N2.Details;
using N2.Integrity;
using Colegios.Web.CMS.Blog;

namespace Colegios.Web.CMS.Calendar
{
    //Icon Url as a folder
    [RestrictParents(typeof(ActivitiesIndexPage))]
    [RestrictChildren(typeof(Activity))]
    [WithEditableName, WithEditableTitle]
    [PageDefinition("Year Blog Activities", Name = "YearActivities", Description = "Year Activities Posts")]
    public class YearActivities : ContentItem
    {
        public YearActivities()
        {
            Name = Title = DateTime.Now.Year.ToString();
        }
    }
}