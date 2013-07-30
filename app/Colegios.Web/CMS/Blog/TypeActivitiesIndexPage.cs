using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using N2;
using N2.Integrity;

namespace Colegios.Web.CMS.Blog
{
    [RestrictParents(typeof(BlogHomePage))]
    [PageDefinition("Type Activities Index Page", Name = "TypeActivitiesPage", Description = "All Activity Types")]
    [RestrictChildren(typeof(TypeActivityPage))]
    [RestrictCardinality]
    public class TypeActivitiesIndexPage: BasePage
    {
    }
}