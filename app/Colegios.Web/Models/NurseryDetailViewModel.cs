using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Colegios.Query.Core.DTOs;

namespace Colegios.Web.Models
{
    public class NurseryDetailViewModel
    {
        public NurseryDetailViewModel(SearchBaseModel baseModel, NurserySearchModel searchModel, NurseryDto nursery, IEnumerable<NurseryDto> districtNurseries, SeoModel seoModel)
        {
            this.BaseModel = baseModel;
            this.SearchModel = searchModel;
            this.Nursery = nursery;
            this.Locations = districtNurseries.Where(x => x.Id != nursery.Id).Select(x => new LocationModel() { Name = x.Name, Latitude = x.Latitude, Longitude = x.Longitude, Id = x.Id, Address = String.Concat(x.Address, " ", x.PostCode, " ", x.Locality.Town.Name), Distance = Math.Sqrt(Math.Pow((double)(x.Latitude - nursery.Latitude), (double)2) + Math.Pow((double)(x.Longitude - nursery.Longitude), 2)), TypeSchool = x.Type.Name }).OrderBy(x => x.Distance);
            this.Seo = seoModel;

            if ((nursery.HighLightedNursery != null) && (nursery.HighLightedNursery.Any()))
            {
                var highLightedNursery = nursery.HighLightedNursery.Where(x=> x.StartDate <= DateTime.Now && x.EndDate >= DateTime.Now).FirstOrDefault();
                if (highLightedNursery != null)
                    this.LogoName = highLightedNursery.LogoName;
            }
        }

        public NurseryDto Nursery { get; set; }
        public NurserySearchModel SearchModel { get; set; }
        public SearchBaseModel BaseModel { get; set; }
        public IEnumerable<LocationModel> Locations { get; set; }
        public SeoModel Seo { get; set; }
        public string LogoName { get; set; }
    }

}