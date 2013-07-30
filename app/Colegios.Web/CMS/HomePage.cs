using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using N2;
using N2.Details;
using N2.Edit.Web;
using N2.Integrity;
using Colegios.Web.CMS.Blog;
using Colegios.Web.CMS.Calendar;

namespace Colegios.Web.CMS
{

    [RestrictParents(typeof(RootNode))]//break down when creating from scratch N2 database
    [PageDefinition("Home Page",Name="HomePage", Description="Coles y guardes Home Page")]
    [RestrictCardinality]
    public class HomePage : BasePage
    {
        [EditableFreeTextArea("WelcomeText", 100)]
        public virtual string WelcomeText
        {
            get { return (string)(GetDetail("WelcomeText") ?? string.Empty); }
            set { SetDetail("WelcomeText", value, string.Empty); }
        }
    }
}
