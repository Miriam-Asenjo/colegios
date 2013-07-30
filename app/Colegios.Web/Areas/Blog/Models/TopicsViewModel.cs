using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Colegios.Web.CMS.Blog;

namespace Colegios.Web.Areas.Blog.Models
{
    public class TopicsViewModel
    {
        public IEnumerable<Article> Posts { get; set; }
        public IEnumerable<TopicPage> Topics { get; set; }
        public TopicPage Topic { get; set; }

        public TopicsViewModel()
        {
            this.Posts = new List<Article>();
            this.Topics = new List<TopicPage>();
        }
    }
}