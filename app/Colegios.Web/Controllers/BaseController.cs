using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using N2;
using N2.Engine;
using N2.Web.Mvc;
using Colegios.Query.Core.QueryInterfaces;
using Colegios.Web.Models;
using Colegios.Web.Enums;
using Colegios.Query.Core.DTOs;

namespace SchoolBlog.Controllers
{
    public class BaseController : Controller
    {
        protected readonly ILocationQuery locationQuery;
        protected readonly IEducationQuery educationQuery;

        public BaseController(){}

        public BaseController(ILocationQuery locationQuery, IEducationQuery educationQuery)
        {
            this.locationQuery = locationQuery;
            this.educationQuery = educationQuery;
        }

        #region N2

        private ContentItem currentItem;
        private ContentItem currentPage;
        private ContentItem currentPart;
        private IEngine engine;

        /// <summary>
        /// Used to resolve services.
        /// </summary>
        public IEngine Engine
        {
            get { return engine ?? (engine = RouteExtensions.GetEngine(RouteData)); }
            set { engine = value; }
        }

        /// <summary> The content item associated with the requested path. </summary>
        private ContentItem CurrentItem
        {
            get { return currentItem ?? (currentItem = ControllerContext.RequestContext.CurrentItem()); }
            set { currentItem = value; }
        }


        /// <summary>The page beeing served by this request, or the page a part has been added to.</summary>
        public ContentItem CurrentPage
        {
            get
            {
                if (currentPage == null)
                    currentPage = GetCurrentPage();

                //if (currentPage != null)
                //    SetCurrentPage(currentPage);

                return currentPage;
            }
            set
            {
                currentPage = value;
                //SetCurrentPage(currentPage);
            }
        }

        /// <summary>The currently displayed part or null if a page is beeing executed.</summary>
        private ContentItem CurrentPart
        {
            get { return currentPart ?? (currentPart = ControllerContext.RequestContext.CurrentPart<ContentItem>()); }
            set { currentPart = value; }
        }

        /// <summary>
        /// Gets the current content page for this request
        /// </summary>
        /// <returns></returns>
        private ContentItem GetCurrentPage()
        {
            return RouteData.CurrentPage() ?? CurrentItem.ClosestPage() ?? Engine.UrlParser.CurrentPage;
        }

        /// <summary>
        /// Puts content item into the request cache so versioning and previewing work
        /// </summary>
        /// <param name="contentItem"></param>
        //private void SetCurrentPage(ContentItem contentItem)
        //{
        //    if (contentItem != null)
        //    {
        //        ControllerContext.RouteData.DataTokens[ContentRoute.ContentPageKey] = currentPage;
        //        ControllerContext.RouteData.DataTokens[ContentRoute.ContentItemKey] = currentPage;
        //        ControllerContext.HttpContext.Items["CurrentPage"] = currentPage;
        //    }
        //}

        /// <summary>
        /// Gets the current CMS page using safe casting
        /// </summary>
        public T CurrentPageAs<T>() where T : ContentItem
        {
            return CurrentPage as T;
        }

        #endregion

        protected SearchBaseModel getSearchBaseModel(PlaceType placeType)
        {
            var cities = locationQuery.GetCities();

            var towns = (from town in locationQuery.GetTowns()
                         group town by town.City.Id into lstTowns
                         select lstTowns).ToDictionary(x => x.Key.ToString(), x => x.AsEnumerable());

            var districts = (from district in locationQuery.GetLocations()
                             group district by district.Town.Id into lstDistricts
                             select lstDistricts).ToDictionary(x => x.Key.ToString(), x => x.AsEnumerable());

            IEnumerable<EducationTypeDto> educationTypes = null;

            if (placeType == PlaceType.Nursery)
                educationTypes = educationQuery.GetTypeEducations().Where(x=> x.IsNursery);
            else 
                educationTypes = educationQuery.GetTypeEducations().Where(x=> x.IsSchool);

            return new SearchBaseModel(cities, towns, districts, educationTypes);

        }

    }
}
