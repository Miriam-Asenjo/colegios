using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using N2;
using N2.Integrity;
using Colegios.Web.CMS.Calendar;

namespace Colegios.Web.CMS.Blog
{
    [RestrictParents(typeof(BlogHomePage))]
    [PageDefinition("Articles Index Page", Name="ArticlesIndexPage", Description="All Articles")]
    [RestrictChildren(typeof(YearPosts))]
    [RestrictCardinality]
    public class ArticlesIndexPage: BasePage
    {
    }
}