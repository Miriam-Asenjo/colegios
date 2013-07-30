using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SchoolBlog.Controllers;
using Colegios.Query.Core.QueryInterfaces;
using log4net;
using Colegios.Web.Models;
using Colegios.Query.Core.Criteria;
using Colegios.Web.Utils;
using Colegios.Query.Core.DTOs;
using System.Configuration;
using Colegios.Web.Enums;
using Colegios.Command.Core.DataInterfaces;
using Colegios.Web.Mappers;
using Colegios.ApplicationServices.Interfaces;
using Colegios.ApplicationServices;

namespace Colegios.Web.Controllers
{
    public class NurseryController : BaseController
    {
        private INurseryQuery nurseryQuery;
        private INurseryModifiedRepository nurseryModifiedRepository;
        private INurseryService nurseryService;
        private static readonly ILog log = LogManager.GetLogger(typeof(NurseryController));

        public NurseryController(ILocationQuery locationQuery, IEducationQuery educationQuery, INurseryQuery nurseryQuery, INurseryModifiedRepository nurseryModifiedRepository, INurseryService nurseryService): base(locationQuery, educationQuery)
        {
            this.nurseryQuery = nurseryQuery;
            this.nurseryModifiedRepository = nurseryModifiedRepository;
            this.nurseryService = nurseryService;
        }

        public ActionResult Index()
        {
            var baseModel = getSearchBaseModel(PlaceType.Nursery);

            var nurserySearchModel = new NurserySearchModel();

            var cityId = 0;
            var townId = 0;
            int[] localities = { };

            if (baseModel.Cities.Count() == 1)
            {
                nurserySearchModel.CityId = baseModel.Cities.First().Id;
                nurserySearchModel.TownId = baseModel.Towns[baseModel.Cities.First().Id.ToString()].First().Id;
            }

            int numTotalNurseries = 0;
            IList<HighLightNurseryDto> highLightedNurseries = new List<HighLightNurseryDto>();
            NurseryCriteria criteria = new NurseryCriteria(localities, nurserySearchModel.TownId, null, null, false);
            var lstNurseries = nurseryQuery.FindNurseries(criteria, 5, out numTotalNurseries, out highLightedNurseries, Int32.Parse(ConfigurationManager.AppSettings["highLightedNurseries"]));
            var nurseries = new PageList<NurseryDto>(lstNurseries, 1, numTotalNurseries,highLightedNurseries.Count());
            HttpContext.Session.Add("searchParametersNurseries", nurserySearchModel);
            var nurseryViewModel = new NurseriesViewModel(baseModel, nurserySearchModel, nurseries, highLightedNurseries);
            return View(nurseryViewModel);
        }

        public PageList<NurseryDto> LookForANursery(SearchBaseModel baseModel, NurserySearchModel nurserySearchModel,out IList<HighLightNurseryDto> highLightedNurseries)
        {
            int[] localities = { };

            if (baseModel.Cities.Count() == 1)
            {
                nurserySearchModel.CityId = baseModel.Cities.First().Id;
                if (nurserySearchModel.TownId == 0)
                    nurserySearchModel.TownId = baseModel.Towns[baseModel.Cities.First().Id.ToString()].First().Id;
            }

            if (!String.IsNullOrEmpty(nurserySearchModel.Locations))
            {
                localities = (from locality in nurserySearchModel.Locations.Split(',')
                              select Int32.Parse(locality)).ToArray();
            }

            int numTotalNurseries = 0;

            var criteria = new NurseryCriteria(localities, nurserySearchModel.TownId, nurserySearchModel.Name, nurserySearchModel.EducationTypeId, nurserySearchModel.IsBilingual);
            var lstNurseries = nurseryQuery.FindNurseries(criteria, 5, out numTotalNurseries, out highLightedNurseries, Int32.Parse(ConfigurationManager.AppSettings["highLightedNurseries"]));
            return new PageList<NurseryDto>(lstNurseries, 1, numTotalNurseries, highLightedNurseries.Count());
        }


        [HttpPost]
        public ActionResult Index(NurserySearchModel nurserySearchModel)
        {
            PageList<NurseryDto> nurseries = null;
            var baseModel = getSearchBaseModel(PlaceType.Nursery);
            IList<HighLightNurseryDto> highLightedNurseries = new List<HighLightNurseryDto>();
            NurseriesViewModel model;
            if (!ModelState.IsValid)
            {
                nurseries = new PageList<NurseryDto>(null, 0, 0,0);
                model = new NurseriesViewModel(baseModel, nurserySearchModel, nurseries, highLightedNurseries);

                return View(model);
            }


                //int[] localities = { };

                //if (baseModel.Cities.Count() == 1)
                //{
                //    nurserySearchModel.CityId = baseModel.Cities.First().Id;
                //    if (nurserySearchModel.TownId == 0)
                //        nurserySearchModel.TownId = baseModel.Towns[baseModel.Cities.First().Id.ToString()].First().Id;
                //}

                //if (!String.IsNullOrEmpty(nurserySearchModel.Locations))
                //{
                //    localities = (from locality in nurserySearchModel.Locations.Split(',')
                //                  select Int32.Parse(locality)).ToArray();
                //}

                //int numTotalNurseries = 0;

                //var criteria = new NurseryCriteria(localities, nurserySearchModel.TownId, nurserySearchModel.Name, nurserySearchModel.EducationTypeId, nurserySearchModel.IsBilingual);
                //var lstNurseries = nurseryQuery.FindNurseries(criteria, 5, out numTotalNurseries, out highLightedNurseries, Int32.Parse(ConfigurationManager.AppSettings["highLightedNurseries"]));
                //nurseries = new PageList<NurseryDto>(lstNurseries, 1, numTotalNurseries,highLightedNurseries.Count()
                //    );

            nurseries = LookForANursery(baseModel, nurserySearchModel, out highLightedNurseries);
            HttpContext.Session.Add("searchParametersNurseries", nurserySearchModel);


            model = new NurseriesViewModel(baseModel, nurserySearchModel, nurseries, highLightedNurseries);

            return View(model);
        }


        public ActionResult Locality(int localityId)
        {
            var locality = locationQuery.GetLocation(localityId);

            if (locality == null)
                return RedirectToAction("Index");

            var baseModel = getSearchBaseModel(PlaceType.Nursery);
            IList<HighLightNurseryDto> highLightedNurseries = new List<HighLightNurseryDto>();
            var nurserySearchModel = new NurserySearchModel() { TownId = localityId, CityId = locality.Town.City.Id };
            var nurseries = LookForANursery(baseModel, nurserySearchModel, out highLightedNurseries);
            HttpContext.Session.Add("searchParametersNurseries", nurserySearchModel);


            var model = new NurseriesViewModel(baseModel, nurserySearchModel, nurseries, highLightedNurseries);

            return View("Index", model);

        } 



        //
        // GET: /Nursery/

        public ActionResult List(int pageNo)
        {
            PageList<NurseryDto> schools = null;
            var searchViewModel = (NurserySearchModel)HttpContext.Session.Contents["searchParametersNurseries"];
            var baseModel = getSearchBaseModel(PlaceType.Nursery);
            var numTotalNurseries = 0;
            IList<HighLightNurseryDto> highLightedNurseries = new List<HighLightNurseryDto>();
            if (searchViewModel != null)
            {

                int[] localities = { };

                if (!String.IsNullOrEmpty(searchViewModel.Locations))
                {
                    localities = (from locality in searchViewModel.Locations.Split(',')
                                  select Int32.Parse(locality)).ToArray();
                }

                NurseryCriteria criteria = new NurseryCriteria(localities, searchViewModel.TownId, searchViewModel.Name, searchViewModel.EducationTypeId, searchViewModel.IsBilingual);

                var lstSchools = nurseryQuery.FindNurseries(criteria, 5, out numTotalNurseries, out highLightedNurseries, Int32.Parse(ConfigurationManager.AppSettings["highLightedNurseries"]), pageNo);
                schools = new PageList<NurseryDto>(lstSchools, pageNo, numTotalNurseries,highLightedNurseries.Count());
            }
            else
            {
                schools = new PageList<NurseryDto>(null, 0, 0, 0);
                searchViewModel = new NurserySearchModel();
                if (baseModel.Cities.Count() == 1)
                {
                    searchViewModel.CityId = baseModel.Cities.First().Id;
                    if (searchViewModel.TownId == 0)
                        searchViewModel.TownId = baseModel.Towns[baseModel.Cities.First().Id.ToString()].First().Id;
                }

                NurseryCriteria criteria = new NurseryCriteria(new int[]{}, searchViewModel.TownId, searchViewModel.Name, searchViewModel.EducationTypeId, searchViewModel.IsBilingual);

                var lstSchools = nurseryQuery.FindNurseries(criteria, 5, out numTotalNurseries, out highLightedNurseries, Int32.Parse(ConfigurationManager.AppSettings["highLightedNurseries"]), pageNo);
                schools = new PageList<NurseryDto>(lstSchools, pageNo, numTotalNurseries,highLightedNurseries.Count());
            }

            var model = new NurseriesViewModel(baseModel, searchViewModel, schools, highLightedNurseries);
            return View("Index", model);
        }


        public ActionResult View(string nurseryName, int nurseryId)
        {
            var searchViewModel = (NurserySearchModel)HttpContext.Session.Contents["searchParametersNurseries"];

            var baseModel = getSearchBaseModel(PlaceType.Nursery);
            if (searchViewModel != null)
            {
                int[] localities = { };

                if (!String.IsNullOrEmpty(searchViewModel.Locations))
                {
                    localities = (from locality in searchViewModel.Locations.Split(',')
                                  select Int32.Parse(locality)).ToArray();
                }
                var nursery = nurseryQuery.GetNursery(nurseryId);
                var districtNurseries = (nursery != null) ? nurseryQuery.DistrictNurseries(nursery.Locality.Id) : new List<NurseryDto>();

                var title = "Escuela Infantil " +  nursery.Name + " en " + nursery.Locality.Name +  " " +  nursery.Locality.Town.City.Name;
                var description = "Información de la Guarderia " + nursery.Name + " del distrito : " + nursery.Locality.Name + " Horario, horario ampliado, cuota, idiomas, bilingüe y muchos más detalles.";
                var keywords = "Escuela Infantil " + nursery.Name + ", Escuela Infantil " + nursery.Locality.Town.Name +
                               ", Guarderias " + nursery.Locality.Name;
                var seoModel = new SeoModel() { Title=title, Description=description, Keywords=keywords };

                var model = new NurseryDetailViewModel(baseModel,searchViewModel,nursery, districtNurseries,seoModel);
                return View (model);
            }
            else
            {
                var nursery = nurseryQuery.GetNursery(nurseryId);
                var title = "Escuela Infantil " + nursery.Name + " en " + nursery.Locality.Name + " " + nursery.Locality.Town.City.Name;
                var description = "Información de la Guarderia " + nursery.Name + " del distrito : " + nursery.Locality.Name + " Horario, horario ampliado, cuota, idiomas, bilingüe y muchos más detalles.";
                var keywords = "Escuela Infantil " + nursery.Name + ", Escuela Infantil " + nursery.Locality.Town.Name +
                               ", Guarderias " + nursery.Locality.Name;
                var seoModel = new SeoModel() { Title = title, Description = description, Keywords = keywords };
                var districtNurseries = (nursery != null) ? nurseryQuery.DistrictNurseries(nursery.Locality.Id) : new List<NurseryDto>();
                var model = new NurseryDetailViewModel (baseModel, new NurserySearchModel(), nursery, districtNurseries , seoModel);
                return View (model);
            }

        }


        public ActionResult Register()
        {

            var baseModel = getSearchBaseModel(PlaceType.Nursery);
            var categoryFields = nurseryQuery.GetCategoryFields();
            var model = new NurseryRegistrationViewModel(getSearchBaseModel(PlaceType.Nursery), categoryFields.ToList());
            //MailManager.SendMail("miriam.g.asenjo@gmail.com", "contacto@colesyguardes.es", String.Empty, "Testing email", "blah blah");
            return View(model);
        }

        public ActionResult SignUp(NurserySignUpModel model)
        {
            if (ModelState.IsValid)
            {
                var nursery = NurseryMapper.ToNurseryModifiedDto(model);
                var categoryValues = model.CategoryDetails.Where(x => String.IsNullOrEmpty(x.Value) == false).ToDictionary(x => x.CategoryFieldId, y => y.Value);
                var nurseryId = nurseryService.NurserySignUp(nursery, model.Institution.Town, model.Institution.District, categoryValues);

                if (nurseryId > 0)
                    return View("Confirmation");
                else
                {
                    ViewBag.ErrorMsg = "Se ha producido un error dando de alta su guardería, Por favor intentelo de nuevo";
                }
            }
            var baseModel = getSearchBaseModel(PlaceType.Nursery);
            var categoryFields = nurseryQuery.GetCategoryFields();
            var registrationModel = new NurseryRegistrationViewModel(getSearchBaseModel(PlaceType.Nursery), categoryFields.ToList());
            registrationModel.Registration = model.Registration;
            registrationModel.Institution = model.Institution;
            registrationModel.CategoryDetails = model.CategoryDetails;
            //var error = ModelState.Values.Where(x => x.Errors.Count() > 1).First();
            var numErrors = ModelState["CategoryDetails[0].Value"].Errors.Count();
            var errors = ModelState.Values.Any(x => x.Errors.Count >= 1);
            return View("Register", registrationModel);
        }

    }
}
