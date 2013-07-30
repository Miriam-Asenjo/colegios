using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using N2;
using N2.Details;
using N2.Web.UI;
using Colegios.Web.CMS.Common;

namespace Colegios.Web.CMS
{
    [PageDefinition("Base Page", Name = "BasePage")]
    [TabContainer(Tabs.Content, "Content", 10)]
    [TabContainer(Tabs.Seo, "SEO", 20)]
    [WithEditableTitle(SortOrder = 0), WithEditableName(SortOrder = 1)]
    [WithEditablePublishedRange("Publish date", 200, ContainerName = Tabs.Content, HelpText = "Date and Time this page will be availability on the site")]
    public abstract class BasePage : ContentItem
    {
        [EditableTextBox(Title = "Page Title", Name = "Page Title", ContainerName = Tabs.Seo, SortOrder = 97)]
        public virtual string PageTitle { get; set; }

        [EditableTextBox(Title = "Meta Keywords", Name = "Meta Keywords", TextMode = TextBoxMode.MultiLine, ContainerName = Tabs.Seo, SortOrder = 98, Rows = 10, Columns = 20)]
        public virtual string MetaKeywords { get; set; }

        [EditableTextBox(Title = "Meta Description", Name = "Meta Description", TextMode = TextBoxMode.MultiLine, ContainerName = Tabs.Seo, SortOrder = 99, Rows = 10, Columns = 20)]
        public virtual string MetaDescription { get; set; }

    }
}