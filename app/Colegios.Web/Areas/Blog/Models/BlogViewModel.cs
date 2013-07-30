using System.Collections.Generic;
using Colegios.Web.CMS.Blog;
using Colegios.Web.Models;

namespace Colegios.Web.Areas.Blog.Models
{
    public class BlogViewModel
    {
        public IEnumerable<Article> Posts { get; set; }
        public IEnumerable<TopicPage> Topics { get; set; }
        public IEnumerable<Activity> Activities { get; set; }
        public int PageNo { get; set; }
        public int? PreviousPage { get; set; }
        public int? NextPage { get; set; }

        public BlogViewModel()
        {
            this.Posts = new List<Article>();
            this.Topics = new List<TopicPage>();
            this.Activities = new List<Activity>();
            this.PageNo = 0;
        }
       
    }
}