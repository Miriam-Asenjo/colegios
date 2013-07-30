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
    [RestrictParents(typeof(TopicsIndexPage))]
    [PageDefinition("Topic Page", Name="TopicPage", Description="Topic")]
    public class TopicPage: BasePage
    {
        [EditableTextBox(Title = "Topic Description", Name = "Topic Description", ContainerName = Tabs.Content, SortOrder = 97)]
        public virtual string Description { get; set; }
    }
}