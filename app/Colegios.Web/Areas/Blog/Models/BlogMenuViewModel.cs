using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Colegios.Web.CMS.Blog;

namespace Colegios.Web.Areas.Blog.Models
{
    public class BlogMenuViewModel
    {
        public IEnumerable<TopicPage> Topics { get; set; }
        public IEnumerable<Article> Posts { get; set; }
        public bool ShowActivities { get; set; }
        public IEnumerable<BannerModel> Banners { get; set; }

        public BlogMenuViewModel (bool showActivities=true) {
            this.Topics = new List<TopicPage> ();
            this.Posts = new List<Article>();
            this.ShowActivities = showActivities;
            this.Banners = new List<BannerModel>();
        }

        
    }
}