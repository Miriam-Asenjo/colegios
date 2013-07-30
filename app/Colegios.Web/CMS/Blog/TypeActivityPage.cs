using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Colegios.Web.CMS.Common;
using N2;
using N2.Details;
using N2.Integrity;

namespace Colegios.Web.CMS.Blog
{
    [RestrictParents(typeof(TypeActivitiesIndexPage))]
    [PageDefinition("Type Activity Page", Name = "TypeActivityPage", Description = "TypeActivity")]
    [RestrictChildren(typeof(Activity))]
    public class TypeActivityPage : BasePage
    {
        [EditableTextBox(Title = "Type Activity Description", Name = "Type Activity Description", ContainerName = Tabs.Content, SortOrder = 97)]
        public virtual string Description { get; set; }
    }
}