using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Colegios.Web.CMS.Blog;

namespace Colegios.Web.Areas.Blog.Models
{
    public class PostViewModel
    {
        public IEnumerable<TopicPage> Topics { get; set; }
        public Article Post { get; set; }
        public IEnumerable<TopicPage> PostTopics { get; set; } 

        public PostViewModel()
        {
            this.Topics = new List<TopicPage>();
            this.PostTopics = new List<TopicPage>();
        }
    }
}