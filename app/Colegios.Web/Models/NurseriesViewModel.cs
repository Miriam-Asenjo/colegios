using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Colegios.Web.Utils;
using Colegios.Query.Core.DTOs;

namespace Colegios.Web.Models
{
    public class NurseriesViewModel
    {
        public NurseriesViewModel(SearchBaseModel searchBaseModel, NurserySearchModel nurserySearchModel, PageList<NurseryDto> nurseries, IList<HighLightNurseryDto> highLightedNurseries)
        {
            this.SearchBaseModel = searchBaseModel;
            this.NurserySearchModel = nurserySearchModel;
            this.Nurseries = nurseries;
            var localities = new List<LocalityDto>();
            if (!String.IsNullOrEmpty(nurserySearchModel.Locations))
            {
                foreach (string localityId in nurserySearchModel.Locations.Split(','))
                {
                    localities.Add(searchBaseModel.Districts[nurserySearchModel.TownId.ToString()].Where(x => x.Id == Int32.Parse(localityId)).SingleOrDefault());
                }
            }
            this.Localites = localities;
            this.HighLightedNurseries = highLightedNurseries;
        }

        public SearchBaseModel SearchBaseModel {get;set;}
        public NurserySearchModel NurserySearchModel {get;set;}
        public PageList<NurseryDto> Nurseries { get; set; }
        public IEnumerable<LocalityDto> Localites { get; private set; }
        public IList<HighLightNurseryDto> HighLightedNurseries { get; private set; }

    }
}