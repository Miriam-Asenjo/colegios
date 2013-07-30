using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using N2.Collections;
using N2.Web;
using Colegios.Web.Areas.Blog.Models;
using Colegios.Web.CMS.Blog;
using Colegios.Web.Controllers;
using SchoolBlog.Controllers;
using Colegios.Command.Core.DataInterfaces;
using SharpArch.NHibernate.Web.Mvc;

namespace Colegios.Web.Areas.Blog.Controllers
{
    [Controls(typeof(Article))]
    public class PostsController : BaseController
    {
        private IPostRepository postRepository;

        public PostsController(IPostRepository postRepository){
            this.postRepository = postRepository;
        }
        //
        // GET: /Blog/Posts/
        [Transaction]
        public ActionResult Index(int pageNo = 1)
        {
            var topics = N2.Find.Items.Where.Type.Eq(typeof(TopicPage)).Select<TopicPage>().OrderBy(x => x.Name);

            if (CurrentPageAs<Article>() != null)
            {
                var post = CurrentPageAs<Article>();
                var postTopics = new List<TopicPage>();

                var idTopics = post.Topics.Split(',');

                foreach (var id in idTopics)
                {
                    var topic = topics.Where(x => x.ID.ToString() == id).FirstOrDefault();
                    if (topic != null)
                        postTopics.Add(topic);
                }

                this.postRepository.SaveOrUpdate(post.ID);

                var postModel = new PostViewModel() { Post = CurrentPageAs<Article>(), Topics = topics, PostTopics = postTopics};
                return View("~/Areas/Blog/Views/Posts/View.cshtml", postModel);
            }

            var postsPerPage = Int32.Parse(ConfigurationManager.AppSettings["postsPerPage"]);

            //TODO publishedfilter can't be applied for count items
            var numPosts = N2.Find.Items.Where.Type.Eq(typeof (Article)).Count();


            var lastPage = (numPosts%postsPerPage == 0)? (int)(numPosts/postsPerPage) : (int)(numPosts/postsPerPage) + 1;

            var posts = N2.Find.Items.Where.Type.Eq(typeof(Article))
                .OrderBy.Published.Desc
                .Filters(new PublishedFilter())
                .Select<Article>().Skip((pageNo-1)*postsPerPage).Take(pageNo*postsPerPage);


            
            var model = new BlogViewModel() {Posts = posts, Topics = topics, PageNo = pageNo, PreviousPage = (pageNo > 1) ? pageNo-1: (int?)null, NextPage = (pageNo < lastPage)? pageNo + 1 :(int?)null};
            return View(model);
        }

    }
}
