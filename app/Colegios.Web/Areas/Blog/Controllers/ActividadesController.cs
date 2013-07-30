using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Colegios.Web.CMS.Blog;
using Colegios.Web.Areas.Blog.Models;
using System.Configuration;

namespace Colegios.Web.Areas.Blog.Controllers
{
    public class ActividadesController : Controller
    {
        //
        // GET: /Blog/Activities/

        public ActionResult Index(int pageNo=1)
        {

            var activitiesPerPage = Int32.Parse(ConfigurationManager.AppSettings["activitiesPerPage"]);

            var numActivities = N2.Find.Items.Where.Type.Eq(typeof(Activity)).Select<Activity>()
                .Where(x => x.EndDate.Year > 0001).Where(x => x.EndDate >= DateTime.Now).Count();

            var lastPage = (numActivities%activitiesPerPage == 0)? (int)(numActivities/activitiesPerPage) : (int)(numActivities/activitiesPerPage) + 1;

            var activities =
                N2.Find.Items.Where.Type.Eq(typeof(Activity)).Select<Activity>()
                .Where(x => x.EndDate.Year > 0001).Where(x => x.EndDate >= DateTime.Now)
                .OrderByDescending(x => x.Published).Skip((pageNo-1)*activitiesPerPage).Take(activitiesPerPage);

            var topics = N2.Find.Items.Where.Type.Eq(typeof(TopicPage)).Select<TopicPage>().OrderBy(x => x.Name);

            return View(new ActivitiesViewModel() { Activities = activities, Topics = topics, PageNo = pageNo, PreviousPage = (pageNo > 1) ? pageNo-1: (int?)null, NextPage = (pageNo < lastPage)? pageNo + 1 :(int?)null });
        }

    }
}
