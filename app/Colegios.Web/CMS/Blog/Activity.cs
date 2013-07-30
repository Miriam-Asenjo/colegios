using System;
using Colegios.Web.CMS.Common;
using N2;
using N2.Details;
using N2.Integrity;
using Colegios.Web.CMS.Calendar;

namespace Colegios.Web.CMS.Blog
{
    [RestrictParents(typeof(YearActivities))]
    [PageDefinition("Activity Page",Name="ActivityPage", Description="Activity Page")]
    [WithEditableTitle, WithEditableName]
    public class Activity: BasePage
    {
        [EditableTextBox("Heading", 2, HelpText = "Heading of the Activity(Title)", ContainerName = Tabs.Content)]
        public virtual string Heading { get; set; }

        [EditableCheckBoxList(typeof(TypeActivityPage), "Title", "Id", Name = "ListTypeActivities", Title = "List Of Activity Types", HelpText = "Available type activities", ContainerName = Tabs.Content, SortOrder = 3)]
        public virtual string TypeActivities
        {
            get { return (string)(GetDetail("ListTypeActivities", "")); }
            set { SetDetail("ListTypeActivities", value, ""); }
        }

        [EditableTextBox("Audience", 4, HelpText = "Recommended audience", ContainerName = Tabs.Content)]
        public virtual string RecommendedPublic { get; set; }

        [EditableFreeTextArea("Description", 5, HelpText = "Activity Description", ContainerName = Tabs.Content)]
        public virtual string Description { get; set; }

        [EditableDate("Start Date", 6, HelpText = "Start Date", ContainerName = Tabs.Content)]
        public virtual DateTime StartDate { get; set; }

        [EditableDate("End Date", 7, HelpText = "End Date", ContainerName = Tabs.Content)]
        public virtual DateTime EndDate { get; set; }

        [EditableTextBox("Hours", 8, HelpText = "Hours", ContainerName = Tabs.Content)]
        public virtual string Hours { get; set; }

        [EditableTextBox("Place", 9, HelpText = "Activity Place", ContainerName = Tabs.Content)]
        public virtual string Place { get; set; }

        [EditableTextBox("Address", 10, HelpText = "Activity Address", ContainerName = Tabs.Content)]
        public virtual string Address { get; set; }

        [EditableTextBox("Price", 11, HelpText = "Activity Price", ContainerName = Tabs.Content)]
        public virtual string Price { get; set; }

        [EditableFreeTextArea("OtherInformation", 12, HelpText = "More Information Activity", ContainerName = Tabs.Content)]
        public virtual string OtherInformation { get; set; }

        [EditableLink("Web page", 13, HelpText = "Activity Web Page", ContainerName = Tabs.Content)]
        public virtual string WebPage { get; set; }



    }
}