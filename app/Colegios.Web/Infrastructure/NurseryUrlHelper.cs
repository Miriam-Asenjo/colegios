using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Colegios.Web.Utils;

namespace Colegios.Web.Infrastructure
{
    public static class NurseryUrlHelper
    {
        public static string NurseryListAction(this System.Web.Mvc.UrlHelper helper, int pageNo)
        {
            return helper.RouteUrl("NurseryList", new { pageNo = pageNo });
        }

        public static string NurseryDetailsAction(this System.Web.Mvc.UrlHelper helper, string title, int id)
        {
            var convertedTitle = title.ConvertToUrlString();
            return helper.RouteUrl("NurseryDetails", new { nurseryName = convertedTitle, nurseryId = id, Area = "" });

        }

        public static string NurseriesLocalityAction(this System.Web.Mvc.UrlHelper helper, string town, string locality, int localityId)
        {
            return helper.RouteUrl("NurseriesLocality", new { townName = town.ToLower().StripeAcents().ConvertToUrlString(), localityName = locality.ToLower().StripeAcents().ConvertToUrlString(), localityId = localityId, Area = "" });

        }

        public static string NurseryDetailsByLocalityAction(this System.Web.Mvc.UrlHelper helper, string townName, string localityName, string nurseryName, int nurseryId)
        {
            return helper.RouteUrl("NurseryDetailsLocality", new { townName = townName.ToLower().StripeAcents().ConvertToUrlString(), localityName = localityName.ToLower().StripeAcents().ConvertToUrlString(), nurseryName = nurseryName.ToLower().StripeAcents().ConvertToUrlString(), nurseryId = nurseryId });
        } 
    }
}