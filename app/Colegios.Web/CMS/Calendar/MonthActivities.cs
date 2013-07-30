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
    [RestrictParents(typeof(YearActivities))]
    [RestrictChildren(typeof(Activity))]
    [WithEditableName, WithEditableTitle]
    public abstract class MonthActivities: ContentItem
    {
        //public override string Name
        //{
        //    get { return this.Title.ToLower(); }
        //}
    }
}