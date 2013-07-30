using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using N2;
using N2.Details;
using N2.Integrity;
using Colegios.Web.CMS.Common;

namespace Colegios.Web.CMS.Blog
{
    [RestrictParents(typeof(HomePage))]
    [PageDefinition("Blog Home Page", Name = "BlogHomePage", Description = "ColesyGuardes Blog HomePage")]
    [RestrictCardinality]
    public class BlogHomePage: BasePage
    {

    }
}