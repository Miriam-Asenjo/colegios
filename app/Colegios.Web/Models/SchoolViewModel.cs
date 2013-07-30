using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Colegios.Query.Core.DTOs;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Colegios.Web.Utils;
using Colegios.Web.CMS;
using System.Runtime.Serialization;
using Colegios.Web.Enums;

namespace Colegios.Web.Models
{

    /*public class SchoolBaseModel
    {

        public SchoolBaseModel(IEnumerable<CityDto> cities, Dictionary<string, IEnumerable<TownDto>> towns, Dictionary<string, IEnumerable<LocalityDto>> districts, IEnumerable<EducationTypeDto> educationTypes)
        {
            this.Cities = cities;
            this.Towns = towns;
            this.Districts = districts;
            this.EducationTypes = educationTypes;
            //this.SearchModel = new SchoolSearchViewModel();
        }

        //public SchoolSearchViewModel SearchModel { get; set; }

        public IEnumerable<CityDto> Cities { get; set; }
        public Dictionary<string, IEnumerable<TownDto>> Towns { get; set; }
        public Dictionary<string, IEnumerable<LocalityDto>> Districts { get; set; }
        public IEnumerable<EducationTypeDto> EducationTypes { get; set; }

    }*/


    public class SchoolSearchViewModel
    {

        [Required(ErrorMessage="Selecciona una Ciudad")]
        [DisplayName("Ciudad")]
        public int CityId { get; set; }

        [Required(ErrorMessage="Selecciona un Municipio")]
        [DisplayName("Municipio")]
        public int TownId { get; set; }

        public string Locations { get; set; }

        [DisplayName("Tipo de Centro")]
        public int? EducationTypeId { get; set; }

        [DisplayName("¿Es Bilingüe?")]
        [Required]
        public bool IsBilingual { get; set; }

        [DisplayName("¿Qué Buscas?")]
        [StringLength(255)]
        public string Name { get; set; }

        [DisplayName("Jornada Continua")]
        public bool NoBreak { get; set;}

    }

    public class SchoolIndexViewModel
    {
        public SchoolIndexViewModel(SearchBaseModel baseModel, SchoolSearchViewModel searchModel, HomePage homePage)
        {
            this.BaseModel = baseModel;
            this.SearchModel = searchModel;
            this.HomePageCMS = homePage;
        }

        public SearchBaseModel BaseModel { get; set; }
        public SchoolSearchViewModel SearchModel { get; set; }
        public HomePage HomePageCMS { get; set; }

    }

    public class SchoolsViewModel
    {

        public SchoolsViewModel(SearchBaseModel baseModel, SchoolSearchViewModel searchModel, PageList<SchoolDto> schools, IList<HighLightSchoolDto> highLightedSchools)
        {
            this.IndexModel = new SchoolIndexViewModel(baseModel, searchModel, null);
            this.Schools = schools;
            var localities = new List<LocalityDto>();
            if (!String.IsNullOrEmpty(searchModel.Locations))
            {
                foreach (string localityId in searchModel.Locations.Split(','))
                {
                    localities.Add(baseModel.Districts[searchModel.TownId.ToString()].Where(x => x.Id == Int32.Parse(localityId)).SingleOrDefault());
                }

            }
            this.Localites = localities;
            this.HighLightedSchools = highLightedSchools;

        }

        public SchoolIndexViewModel IndexModel { get; set; }
        public PageList<SchoolDto> Schools { get; set; }
        public IEnumerable<LocalityDto> Localites { get; private set; }
        public IList<HighLightSchoolDto> HighLightedSchools { get; private set; }

    }

    public class SchoolDetailViewModel
    {

        public SchoolDetailViewModel(SearchBaseModel baseModel, SchoolSearchViewModel searchModel, SchoolDto school, IEnumerable<SchoolDto> districtSchools, int currentYear, SeoModel seoModel)
        {
            this.BaseModel = baseModel;
            this.SearchModel = searchModel;
            this.School = school;

            this.Locations = districtSchools.Where(x=> x.Id != school.Id).Select(x => new LocationModel() {Name=x.Name, Latitude=x.Latitude, Longitude = x.Longitude, Id = x.Id, Address= String.Concat(x.Address," ", x.PostCode, " ", x.Locality.Town.Name), Distance=Math.Sqrt(Math.Pow((double)(x.Latitude - school.Latitude),(double)2) + Math.Pow((double)(x.Longitude - school.Longitude),2)), TypeSchool = x.Type.Name}).OrderBy(x=> x.Distance);
            var schoolTypes = districtSchools.GroupBy(x => x.Type).Select(x => x.Key);
            this.MarkModel = new DistrictMarkModel(school, districtSchools, currentYear);
            this.Seo = seoModel;

            if ((school.HighLightedSchool != null) && (school.HighLightedSchool.Any()))
            {
                var highLightedSchool = school.HighLightedSchool.Where(x => x.StartDate <= DateTime.Now && x.EndDate >= DateTime.Now).FirstOrDefault();
                if (highLightedSchool != null)
                    this.LogoName = highLightedSchool.LogoName;
            }

        }

        public SchoolDto School { get; set; }
        public string LogoName { get; set; }
        public DistrictMarkModel MarkModel { get; set; }
        public SchoolSearchViewModel SearchModel { get; set; }
        public SearchBaseModel BaseModel { get; set; }
        public IEnumerable<LocationModel> Locations { get; set; }
        public SeoModel Seo { get; set; }

    }

    public class DistrictMarkModel {


        public DistrictMarkModel (SchoolDto school, IEnumerable<SchoolDto> districtSchools, int currentYear) {

            this.School = school;
            var schoolTypes = districtSchools.GroupBy(x => x.Type).Select(x => x.Key);
            this.SchoolTypeMarks = new Dictionary<EducationTypeDto, IEnumerable<MarkModel>>();

            if (!school.Marks.Where(x => x.YearExam == currentYear).Any() && school.Marks.Any())
                currentYear = currentYear -1;

            foreach (var schoolType in schoolTypes)
            {
                var marks = districtSchools.Where(x => x.Type.Id == schoolType.Id).SelectMany(x => x.Marks).Where(x => x.YearExam == currentYear).OrderByDescending(x => x.AverageMark).Select((x, index) => new MarkModel{ Mark = x, Index = index + 1, SchoolId = x.School.Id }).ToList();
                if (schoolType.Id == school.Type.Id)
                {
                    this.RankingTypeSchool = marks.Where(x => x.SchoolId == school.Id).Select(x => x.Index).FirstOrDefault();
                }
                this.SchoolTypeMarks.Add(schoolType, marks.Take(3));
            }

            this.DistrictMarks = districtSchools.SelectMany(x=> x.Marks).Where (x=> x.YearExam == currentYear).OrderByDescending(x=>x.AverageMark).Select((x,index) => new MarkModel {Mark=x, Index=index + 1, SchoolId=x.School.Id});
            this.GlobalDistrictRanking = this.DistrictMarks.Where (x=> x.SchoolId == school.Id).Select(x=> x.Index).FirstOrDefault();
            this.DistrictMarks = this.DistrictMarks.Take(2);

            this.YearMarks = currentYear;
        }


        public SchoolDto School { get; set; }
        public Dictionary<EducationTypeDto,IEnumerable<MarkModel>> SchoolTypeMarks { get; set; }
        public IEnumerable<MarkModel> DistrictMarks { get; set; }
        public int GlobalDistrictRanking { get; set; }
        public int GlobalRanking { get; set; }
        public int RankingTypeSchool { get; set; }
        public int YearMarks { get; set; }
    }

    public class MarkModel {
    
        public int Index {get; set;}
        public MarkDto Mark {get; set;}
        public int SchoolId {get; set;}
    }



}