using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using N2.Web;
using Colegios.Web.CMS.Blog;
using SchoolBlog.Controllers;
using Colegios.Web.Areas.Blog.Models;

namespace Colegios.Web.Areas.Blog.Controllers
{
    [Controls(typeof(TopicPage))]
    public class TopicsController : BaseController
    {
        //
        // GET: /Blog/Topics/

        public ActionResult Index()
        {
            var topicPage = CurrentPageAs<TopicPage>();
            var model = new TopicsViewModel();
            if (topicPage != null)
            {
                var posts = N2.Find.Items.Where.Type.Eq(typeof(Article))
                       .OrderBy.Published.Desc.Select<Article>().
                       Where(x => x.Topics.Split(',').Contains(topicPage.ID.ToString()));


                var topics = N2.Find.Items.Where.Type.Eq(typeof(TopicPage)).Select<TopicPage>().OrderBy(x => x.Name);

                model = new TopicsViewModel (){Posts = posts, Topic = topicPage, Topics = topics};
            }
            return View("~/Areas/Blog/Views/Topics/Index.cshtml",model);
        }

    }
}
