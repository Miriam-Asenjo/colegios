using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Colegios.Web.CMS.Blog;

namespace Colegios.Web.Areas.Blog.Models
{
    public class ActivitiesViewModel 
    {
        public IEnumerable<Activity> Activities {get; set;}
        public IEnumerable<TopicPage> Topics { get; set; }
        public int PageNo { get; set; }
        public int? PreviousPage { get; set; }
        public int? NextPage { get; set; }

        public ActivitiesViewModel()
        {
            this.Activities = new List<Activity>();
            this.Topics = new List<TopicPage>();
        }


    }
}
