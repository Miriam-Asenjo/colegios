using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using N2;
using N2.Details;
using N2.Integrity;
using Colegios.Web.CMS.Calendar;
using Colegios.Web.CMS.Common;

namespace Colegios.Web.CMS.Blog
{
    [RestrictParents(typeof(MonthPosts))]
    [PageDefinition("Article Page", Name = "ArticlePage", Description = "Blog Article")]
    public class Article : BasePage
    {
        [EditableTextBox("Heading", 10, HelpText = "Heading of the blog post", ContainerName = Tabs.Content, SortOrder = 2)]
        public virtual string Heading { get; set; }

        [EditableCheckBoxList(typeof(TopicPage), "Title", "Id", Name = "ListTopics", Title = "List Of Topics", HelpText = "Available topic tags", ContainerName = Tabs.Content, SortOrder = 5)]
        public virtual string Topics
        {
            get { return (string)(GetDetail("ListTopics", "")); }
            set { SetDetail("ListTopics", value, ""); }
        }

        [EditableTextBox("Introduction", 10, HelpText = "Introduction of the blog post", ContainerName = Tabs.Content, SortOrder = 10, TextMode = TextBoxMode.MultiLine)]
        public virtual string Introduction { get; set; }

        [EditableFreeTextArea(Title = "Post Content", Name="PostContent", HelpText = "Post content", ContainerName = Tabs.Content, SortOrder = 20)]
        public virtual string Body { get; set; }

        [EditableTextBox("Image Link", 30, SortOrder = 30, ContainerName = Tabs.Content)]
        public virtual string ImageLink { get; set; }

    }
}