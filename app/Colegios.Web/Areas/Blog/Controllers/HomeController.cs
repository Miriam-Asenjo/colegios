using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Colegios.Web.CMS.Blog;
using Colegios.Web.Areas.Blog.Models;
using Colegios.Query.Core.QueryInterfaces;
using System.Configuration;
using N2.Collections;

namespace Colegios.Web.Areas.Blog.Controllers
{
    public class HomeController : Controller
    {
        private IPostQuery postQuery;

        public HomeController(IPostQuery postQuery)
        {
            this.postQuery = postQuery;
        }
        //
        // GET: /Blog/Home/

        public ActionResult Index()
        {
            var numPosts = Int32.Parse(ConfigurationManager.AppSettings["homePageNumPosts"]);

            var posts = N2.Find.Items.Where.Type.Eq(typeof(Article))
                .OrderBy.Published.Desc
                //.Filters(new PublishedFilter())
                .Select<Article>().Take(numPosts);


            var topics = N2.Find.Items.Where.Type.Eq(typeof(TopicPage)).Select<TopicPage>().OrderBy(x => x.Name);

            var numActivities = Int32.Parse(ConfigurationManager.AppSettings["homePageNumActivities"]);

            var activities =
                N2.Find.Items.Where.Type.Eq(typeof (Activity)).Select<Activity>()
                .Where( x=> x.EndDate.Year > 0001).Where(x=> x.EndDate >= DateTime.Now)
                .OrderByDescending(x => x.Published).Take(numActivities);

            var model = new BlogViewModel() { Posts = posts, Topics = topics, Activities = activities};
            return View(model);
        }

        public ActionResult RightMenu(bool showActivities=true)
        {
            var topics = N2.Find.Items.Where.Type.Eq(typeof(TopicPage)).Select<TopicPage>().OrderBy(x => x.Name);

            var numPosts = Int32.Parse(ConfigurationManager.AppSettings["TopPostsRead"]);

            var topPosts = this.postQuery.GetTopPostRead(numPosts);

            //var idTopPosts = topPosts.Select(x=> x.PostId).ToArray();


            var posts = new List<Article>();
            foreach (var post in topPosts){
                posts.Add(N2.Find.Items.Where.Type.Eq(typeof(Article))
                        .OrderBy.Published.Desc
                        .Filters(new PublishedFilter())
                        .Select<Article>().Where(x => x.ID == post.PostId).SingleOrDefault());
            }

            var model = new BlogMenuViewModel() { Posts = posts, Topics = topics, ShowActivities = showActivities };

            if (Request.CurrentExecutionFilePath != null && Request.CurrentExecutionFilePath.EndsWith("/matronatacion"))
            {
                var banners = new List<BannerModel>() { new BannerModel() { Image = "/Images/Banners/PequenioKoala.png", Link = "http://www.pequeñokoala.es/escuela-de-natacion.html" },
                                                        new BannerModel() { Image = "/Images/Banners/BabyOcean.png", Link = "http://www.babyocean.es"}};

                model.Banners = banners;
            }

            if (Request.CurrentExecutionFilePath != null && Request.CurrentExecutionFilePath.EndsWith("/campamentos"))
            {
                var banners = new List<BannerModel>() { new BannerModel() { Image = "/Images/Banners/madridEnRuta_Banner.jpg", Link = "http://www.madridenruta.com" } };

                banners.Add(new BannerModel() { Image = "/Images/Banners/LosPinos_KidsCamp.jpeg", Link = "http://lospinoskidscamp.es" });
                banners.Add(new BannerModel() { Image = "/Images/Banners/BritishNursery_SummerCamp_Banner.jpg", Link = "http://www.britishnursery.com/campamento_de_verano.php" });
                banners.Add(new BannerModel() { Image = "/Images/Banners/PipoLogoCampamento.png", Link = "http://www.escuelainfantilpipos.es/" });
                banners.Add(new BannerModel() { Image = "/Images/Banners/natuAventura.png", Link = "http://www.natuaventura.com/" });           

                model.Banners = banners;
            }

            if (Request.CurrentExecutionFilePath != null && Request.CurrentExecutionFilePath.EndsWith("/campamentos-de-verano-fuera-de-madrid-2013"))
            {
                var banners = new List<BannerModel>();
                banners.Add(new BannerModel() { Image = "/Images/Banners/natuAventura.png", Link = "http://www.natuaventura.com/" });

                model.Banners = banners;
            }

            return PartialView("_RightMenu", model);
        }

    }
}
