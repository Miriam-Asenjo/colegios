using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Colegios.Query.Core.QueryInterfaces;
using Colegios.Web.Models;
using Colegios.Query.Core.DTOs;
using Colegios.Web.Utils;
using Colegios.Query.Core.Criteria;
using System.Configuration;
using SchoolBlog.Controllers;
using Colegios.Web.CMS;
using log4net;
using Colegios.Web.Enums;

namespace Colegios.Web.Controllers
{
    public class SchoolController : BaseController
    {
        private readonly ISchoolQuery schoolQuery;

        private static readonly ILog log = LogManager.GetLogger(typeof(SchoolController));

        public SchoolController(ILocationQuery locationQuery, IEducationQuery educationQuery, ISchoolQuery schoolQuery)
            : base(locationQuery, educationQuery)
        {
            this.schoolQuery = schoolQuery;
        }

        //
        // GET: /School/
        public ActionResult Index()
        {
            var baseModel = getSearchBaseModel(PlaceType.School);

            var homePage = CurrentPageAs<HomePage>();

            if (baseModel.Cities.Count() == 1)
            {
                var searchModel = new SchoolSearchViewModel() { CityId = baseModel.Cities.First().Id, TownId = baseModel.Towns[baseModel.Cities.First().Id.ToString()].First().Id };
                return View (new SchoolIndexViewModel(baseModel,searchModel, homePage));
            }
            else { 
                var searchModel = new SchoolSearchViewModel();
                return View(new SchoolIndexViewModel(baseModel,searchModel, homePage));
            }
        }

        private PageList<SchoolDto> LookForSchools(SearchBaseModel baseModel, SchoolSearchViewModel searchViewModel, out IList<HighLightSchoolDto> highLightedSchools)
        {
            PageList<SchoolDto> schools = null;
            int[] localities = { };
            var cityId = 0;

            if (baseModel.Cities.Count() == 1)
            {
                searchViewModel.CityId = baseModel.Cities.First().Id;
                if (searchViewModel.TownId == 0)
                    searchViewModel.TownId = baseModel.Towns[baseModel.Cities.First().Id.ToString()].First().Id;
            }

            if (!String.IsNullOrEmpty(searchViewModel.Locations))
            {
                localities = (from locality in searchViewModel.Locations.Split(',')
                              select Int32.Parse(locality)).ToArray();
            }

            int numTotalSchools = 0;

            SchoolCriteria criteria = new SchoolCriteria(localities, searchViewModel.TownId, searchViewModel.Name, searchViewModel.EducationTypeId, searchViewModel.IsBilingual, searchViewModel.NoBreak);
            var lstSchools = schoolQuery.FindSchools(criteria, 5, out numTotalSchools, out highLightedSchools, Int32.Parse(ConfigurationManager.AppSettings["highLightedSchools"]));
            schools = new PageList<SchoolDto>(lstSchools, 1, numTotalSchools, highLightedSchools.Count());

            return schools;

        }

        public ActionResult Search(SchoolSearchViewModel searchViewModel)
        {
            PageList<SchoolDto> schools = null;
            var baseModel = getSearchBaseModel(PlaceType.School);
            IList<HighLightSchoolDto> highLightedSchools = new List<HighLightSchoolDto>();
            SchoolsViewModel model = null;
            if (!ModelState.IsValid)
            {
                schools = new PageList<SchoolDto>(null, 0, 0, 0);
                model = new SchoolsViewModel(baseModel, searchViewModel, schools, highLightedSchools);

                return View(model);
            }
            
            //int[] localities = {};
            
            //if (baseModel.Cities.Count() == 1)
            //{
            //    searchViewModel.CityId = baseModel.Cities.First().Id;
            //    if (searchViewModel.TownId == 0)
            //        searchViewModel.TownId = baseModel.Towns[baseModel.Cities.First().Id.ToString()].First().Id;
            //}

            //if (!String.IsNullOrEmpty(searchViewModel.Locations))
            //{
            //    localities = (from locality in searchViewModel.Locations.Split(',')
            //                    select Int32.Parse(locality)).ToArray();
            //}

            //int numTotalSchools = 0;

            //SchoolCriteria criteria = new SchoolCriteria(localities, searchViewModel.TownId, searchViewModel.Name, searchViewModel.EducationTypeId, searchViewModel.IsBilingual, searchViewModel.NoBreak);
            //var lstSchools = schoolQuery.FindSchools(criteria, 5, out numTotalSchools, out highLightedSchools, Int32.Parse(ConfigurationManager.AppSettings["highLightedSchools"]));
            //schools = new PageList<SchoolDto>(lstSchools, 1, numTotalSchools, highLightedSchools.Count());

            //log.Info("Search method: Adding searchParameters to session: " + searchViewModel.Name + "Locations: " + searchViewModel.Locations + " localities: " + localities.Count());

            //log.Info("Session Id: " + Session.SessionID);
            schools = LookForSchools(baseModel, searchViewModel, out highLightedSchools);
            HttpContext.Session.Add("searchParameters", searchViewModel);

            model = new SchoolsViewModel(baseModel, searchViewModel, schools, highLightedSchools);
            
            return View(model);

        }

        public ActionResult Locality(int townId)
        {
            var town = locationQuery.GetTown(townId);

            if (town== null)
                return RedirectToAction("Search");

            var baseModel = getSearchBaseModel(PlaceType.School);
            IList<HighLightSchoolDto> highLightedSchools = new List<HighLightSchoolDto>();

            var locations=string.Empty;

            if (baseModel.Districts.ContainsKey(townId.ToString())){
                var localities = baseModel.Districts[townId.ToString()];

                if (localities != null && localities.Any())
                    locations = localities.First().Id.ToString();
            }

            var searchViewModel = new SchoolSearchViewModel() { TownId = townId , CityId = town.City.Id, Locations = locations};
            var schools = LookForSchools(baseModel, searchViewModel, out highLightedSchools);

            HttpContext.Session.Add("searchParameters", searchViewModel);

            return View("Search", new SchoolsViewModel(baseModel, searchViewModel, schools, highLightedSchools));
        }
        
        public ActionResult List(int pageNo)
        {
            PageList<SchoolDto> schools = null;
            var searchViewModel = (SchoolSearchViewModel)HttpContext.Session.Contents["searchParameters"];
            var baseModel = getSearchBaseModel(PlaceType.School);
            var numTotalSchools = 0;
            IList<HighLightSchoolDto> highLightedSchools = new List<HighLightSchoolDto>();
            if (searchViewModel != null)
            {
                int[] localities = { };

                if (!String.IsNullOrEmpty(searchViewModel.Locations))
                {
                    localities = (from locality in searchViewModel.Locations.Split(',')
                                  select Int32.Parse(locality)).ToArray();
                }

                log.Info("List method: Getting out searchParameters objects from session to session: " + "Name: " + " " + searchViewModel.Name + " localities: " + localities);
                //log.Info("Session Id: " + Session.SessionID);
                SchoolCriteria criteria = new SchoolCriteria(localities, searchViewModel.TownId, searchViewModel.Name, searchViewModel.EducationTypeId, searchViewModel.IsBilingual, searchViewModel.NoBreak);
                //log.Info("CRITERIA KEYWORDS: " + criteria.Keywords);
                //log.Info("CRITERIA TOWNID: " + criteria.TownId);
                //foreach (var locality in criteria.Localities)
                //    log.Info("CRITERIA LOCALITIY: " + locality);

                var lstSchools = schoolQuery.FindSchools(criteria, 5, out numTotalSchools, out highLightedSchools, Int32.Parse(ConfigurationManager.AppSettings["highLightedSchools"]), pageNo);
                schools = new PageList<SchoolDto>(lstSchools, pageNo, numTotalSchools, highLightedSchools.Count());

            }
            else
            {
                schools = new PageList<SchoolDto>(null, 0, 0, 0);
                searchViewModel = new SchoolSearchViewModel();

                if (baseModel.Cities.Count() == 1)
                {
                    searchViewModel.CityId = baseModel.Cities.First().Id;
                    if (searchViewModel.TownId == 0)
                        searchViewModel.TownId = baseModel.Towns[baseModel.Cities.First().Id.ToString()].First().Id;
                }

                SchoolCriteria criteria = new SchoolCriteria(new int[]{}, searchViewModel.TownId, searchViewModel.Name, searchViewModel.EducationTypeId, searchViewModel.IsBilingual, searchViewModel.NoBreak);
                var lstSchools = schoolQuery.FindSchools(criteria, 5, out numTotalSchools, out highLightedSchools, Int32.Parse(ConfigurationManager.AppSettings["highLightedSchools"]), pageNo);
                schools = new PageList<SchoolDto>(lstSchools, pageNo, numTotalSchools, highLightedSchools.Count());

            }

            var model = new SchoolsViewModel(baseModel, searchViewModel, schools,highLightedSchools);
            return View("Search",model);
        }

        public ActionResult View(string schoolName, int schoolId)
        {
            var searchViewModel = (SchoolSearchViewModel)HttpContext.Session.Contents["searchParameters"];

            var baseModel = getSearchBaseModel(PlaceType.School);
            var lastYearMarks = Int32.Parse(ConfigurationManager.AppSettings[Constants.LastYearMarks]);
            if (searchViewModel != null)
            {
                int[] localities = { };

                if (!String.IsNullOrEmpty(searchViewModel.Locations))
                {
                    localities = (from locality in searchViewModel.Locations.Split(',')
                                  select Int32.Parse(locality)).ToArray();
                }

                int numTotalSchools = 0;

                var school = schoolQuery.GetSchool(schoolId);
                var districtSchools = (school != null) ? schoolQuery.DistrictSchools(school.Locality.Id) : new List<SchoolDto>();

                var title = "Colegio " +  school.Name + " en " + school.Locality.Name +  " " +  school.Locality.Town.City.Name;
                var description = "Información del Colegio " + school.Name + " del distrito DISTRITO: " + school.Locality.Name + " Horario, horario ampliado, actividades extraescolares, cuota, idiomas, bilingüe, comedor, nota media y muchos más detalles.";
                var keywords = "Colegio " + school.Name + ", Colegio " +  school.Locality.Town.Name + ", Colegios " +  school.Locality.Name + ", Mejor Colegio " + school.Locality.Name;
                var seoModel = new SeoModel() { Title=title, Description=description, Keywords=keywords };

                var model = new SchoolDetailViewModel(baseModel,searchViewModel,school, districtSchools, lastYearMarks, seoModel);
                return View (model);
            }
            else
            {
                var school = schoolQuery.GetSchool(schoolId);
                var title = "Colegio " + schoolName;
                var description = "Información del Colegio " + schoolName;
                var keywords = "Colegio " + schoolName;
                var districtSchools = (school != null) ? schoolQuery.DistrictSchools(school.Locality.Id) : new List<SchoolDto>();
                var seoModel = new SeoModel() { Title = title, Description = description, Keywords = keywords };
                var model = new SchoolDetailViewModel(baseModel,new SchoolSearchViewModel(), school, districtSchools, lastYearMarks,seoModel);
                return View (model);
            }

        }

        public ActionResult Marks()
        {
            var baseModel = getSearchBaseModel(PlaceType.School); 
            var lastYearMarks = Int32.Parse(ConfigurationManager.AppSettings[Constants.LastYearMarks]);
            var model = new MarksViewModel(baseModel, new MarkSearchViewModel() { MarkType = MarkTypeEnum.Cdi, CityId = baseModel.Cities.First().Id, TownId = baseModel.Towns[baseModel.Cities.First().Id.ToString()].First().Id, Year = lastYearMarks }, Int32.Parse(ConfigurationManager.AppSettings[Constants.RankingMarksPageSize]));


            int numTotalSchools = 0;
            model.Schools = schoolQuery.Ranking(lastYearMarks, (int)MarkTypeEnum.Cdi, null, model.SearchModel.TownId, new int[]{}, out numTotalSchools).ToList();
            model.PageNo = 1;

            
            return View(model);
        }

        [HttpPost]
        public ActionResult Marks(MarkSearchViewModel searchViewModel)
        {
            int[] localities = { };


            var baseModel = getSearchBaseModel(PlaceType.School);

            if (searchViewModel.Locations != null && searchViewModel.Locations.Any())
            {
                //localities = (from locality in searchViewModel.Locations
                //              select Int32.Parse(locality)).ToArray();
                localities = searchViewModel.Locations;
            }

            var pageSize = ConfigurationManager.AppSettings[Constants.RankingMarksPageSize];

            int numTotalSchools = 0;
            var schools = schoolQuery.Ranking(searchViewModel.Year, (int)searchViewModel.MarkType, searchViewModel.EducationTypeId, searchViewModel.TownId, localities, out numTotalSchools, 1, pageSize != null ? Int32.Parse(pageSize) : 50);


            var model = new MarksViewModel(baseModel, searchViewModel, schools.ToList(), numTotalSchools, 1, Int32.Parse(ConfigurationManager.AppSettings[Constants.RankingMarksPageSize]));
            return View(model);
        }

        public ActionResult MarksLocality(MarkTypeEnum examType, int year, int? tipoCentro, string cityName, int cityId, string townName, int localityId, string distritos, int pageNo, int pageSize)
        {

            var localities = new List<int>();
            if (!string.IsNullOrEmpty(distritos))
            {
                var localitiesIds = distritos.Split(',');
                
                foreach (var locality in localitiesIds)
                {
                    int id;
                    if (Int32.TryParse(locality, out id))
                        localities.Add(id);
                }
            }


            int numTotalSchools = 0;
            var schools = schoolQuery.Ranking(year, (int)examType, tipoCentro, localityId, localities.ToArray(), out numTotalSchools, pageNo, pageSize);

            var baseModel = getSearchBaseModel(PlaceType.School);
                        
            var searchModel = new MarkSearchViewModel() 
                { MarkType = examType, 
                  CityId = cityId, 
                  TownId = localityId, 
                  Year = year,
                  Locations = localities.ToArray()};
            var model = new MarksViewModel(baseModel, searchModel, schools.ToList(), numTotalSchools, pageNo, pageSize);
            return View("Marks", model);
        }

    }
}
