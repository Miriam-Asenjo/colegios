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
    [RestrictParents(typeof(ArticlesIndexPage))]
    [RestrictChildren(typeof(MonthPosts))]
    [WithEditableName, WithEditableTitle]
    [RestrictCardinality]
    [PageDefinition("Year Blog Posts", Name="YearPost", Description = "Year Blog Posts")]
    public class YearPosts: ContentItem
    {
        public YearPosts()
        {
            Name = Title = DateTime.Now.Year.ToString();
        }
    }
}