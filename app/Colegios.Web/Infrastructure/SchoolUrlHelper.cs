using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Colegios.Web.Utils;
using Colegios.Web.Enums;

namespace Colegios.Web.Infrastructure
{
    public static class SchoolUrlHelper
    {
        public static string SchoolDetailsAction(this System.Web.Mvc.UrlHelper helper, string title, int id)
        {
            var convertedTitle = title.ConvertToUrlString();
            return helper.RouteUrl("SchoolDetails", new { schoolName = convertedTitle, schoolId = id, Area="" });

        }

        public static string SchoolListAction(this System.Web.Mvc.UrlHelper helper, int pageNo)
        {
            return helper.RouteUrl("SchoolList", new { pageNo = pageNo });
        }

        public static string SchoolsLocalityAction(this System.Web.Mvc.UrlHelper helper, string city, string town, int townId)
        {
            return helper.RouteUrl("SchoolsLocality", new { cityName = city.ToLower().StripeAcents().ConvertToUrlString(), townName = town.ToLower().StripeAcents().ConvertToUrlString(), townId = townId, Area = "" });

        }

        public static string SchoolDetailsByLocalityAction(this System.Web.Mvc.UrlHelper helper, string townName, string localityName, string schoolName, int schoolId)
        {
            return helper.RouteUrl("SchoolDetailsLocality", new { townName = townName.ToLower().StripeAcents().ConvertToUrlString(), localityName = localityName.ToLower().StripeAcents().ConvertToUrlString(), schoolName = schoolName.ToLower().StripeAcents().ConvertToUrlString(), schoolId = schoolId });
        }

        public static string SchoolLocalityMarks(this System.Web.Mvc.UrlHelper helper, MarkTypeEnum markType, int? educationType, int year, string city, int cityId, string town, int localityId, int[] distritos, int pageNo, int pageSize)
        {

            var lstDistritos = string.Empty;
            if ((distritos != null) && (distritos.Any()))
            {
                foreach (var distrito in distritos)
                {
                    lstDistritos=lstDistritos + ((string.IsNullOrEmpty(lstDistritos)) ? distrito.ToString() : "," + distrito.ToString()); 
                }
            }

            return helper.RouteUrl("SchoolLocalityMarks", new { examType = markType.ToString().ToLower(), year= year, cityName = city.ToLower().StripeAcents().ConvertToUrlString(), cityId = cityId, townName = town.ToLower().StripeAcents().ConvertToUrlString(), localityId = localityId, tipoCentro = educationType, pageNo = pageNo, distritos = lstDistritos, pageSize});


        }
    }
}