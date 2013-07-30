using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using N2;
using N2.Integrity;

namespace Colegios.Web.CMS.Blog
{
    [RestrictParents(typeof(BlogHomePage))]
    [PageDefinition("Activities Index Page", Name="ActivitiesIndexPage", Description="All Activities")]
    [RestrictCardinality]
    public class ActivitiesIndexPage: BasePage
    {

    }
}