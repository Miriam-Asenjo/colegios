using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using N2;
using N2.Integrity;

namespace Colegios.Web.CMS.Blog
{
    [RestrictParents(typeof(BlogHomePage))]
    [PageDefinition("Topics Index Page", Name="TopicsPage", Description = "All Topics")]
    [RestrictCardinality]
    public class TopicsIndexPage: BasePage
    {
    }
}