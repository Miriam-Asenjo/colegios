using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Colegios.Query.Core.DTOs;

namespace Colegios.Web.Models
{
    public class MarksViewModel
    {
        public SearchBaseModel BaseModel { get; set; }
        public MarkSearchViewModel SearchModel { get; set; }

        public IList<SchoolDto> Schools { get; set; }

        public int NumTotalSchools { get; set; }
        public int PageNo { get; set; }
        public int PageSize { get; set; }

        public MarksViewModel(SearchBaseModel baseModel, MarkSearchViewModel searchModel, int pageSize)
        {
            this.BaseModel = baseModel;
            this.SearchModel = searchModel;
            this.Schools = new List<SchoolDto>();
            NumTotalSchools = 0;
            PageNo = 0;
            PageSize = pageSize;
        }

        public MarksViewModel(SearchBaseModel baseModel, MarkSearchViewModel searchModel, IList<SchoolDto> schools, int numTotalSchools, int pageNo, int pageSize)
        {
            this.BaseModel = baseModel;
            this.SearchModel = searchModel;
            this.Schools = schools;
            this.NumTotalSchools = numTotalSchools;
            this.PageNo = pageNo;
            this.PageSize = pageSize;
        }
    }
}