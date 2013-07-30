using System.Web.Mvc;

namespace Colegios.Web.Areas.Blog
{
    public class BlogAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "blog";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {

            context.MapRoute(null, "blog/posts/{year}/{month}", new {controller = "posts", action = "Index", month = UrlParameter.Optional, year = UrlParameter.Optional});

            context.MapRoute(null, "blog/actividades/{year}/{month}",
                             new
                                 {
                                     controller = "actividades",
                                     action = "Index",
                                     month = UrlParameter.Optional,
                                     year = UrlParameter.Optional
                                 });

            //context.MapRoute(null, "blog/categorias/{name}", new { controller = "topics", action = "Index", area="blog" });

            context.MapRoute(
                "Blog_default",
                "blog/{controller}/{action}/{id}",
                new { controller="Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
