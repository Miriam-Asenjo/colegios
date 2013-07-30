using System;
using System.Web.Mvc;
using System.Web.Routing;
using Colegios.IoC;
using Colegios.Web.Controllers;
using N2.Engine;
using N2.Web.Mvc;
using NHibernate;
using SharpArch.NHibernate;
using SharpArch.NHibernate.Web.Mvc;
using log4net;

namespace Colegios.Web
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        private WebSessionStorage webSessionStorage;
        //private WebDbContextStorage webDbContextStorage;
        private static readonly ILog log = LogManager.GetLogger(typeof(MvcApplication));

        public override void Init()
        {
            base.Init();
            //the WebSessionStorage must be created during the Init() to tie in HttpApplication Events
            webSessionStorage = new WebSessionStorage(this);
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            log.Error("Start BeginRequest");
            try
            {
                NHibernateInitializer.Instance().InitializeNHibernateOnce(InitializeNHibernateSession);
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                log.Error(ex.InnerException);
                log.Error("ERROR");
                log.Error(ex.StackTrace);
            }
            log.Error("Final BeginRequest");
        }

        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes, IEngine cmsEngine)
        {
            routes.IgnoreRoute("{*favicon}", new { favicon = @"(.*/)?favicon.ico(/.*)?" });
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapContentRoute("Content", cmsEngine);

            routes.MapRoute("SchoolMarks", "colegios/notas", new { Controller = "School", Action = "Marks" });
            routes.MapRoute("SchoolDetails", "colegio/{schoolName}/{schoolId}", new { Controller = "School", Action = "View" }, new {schoolId="\\d+"}, new[] { "Colegios.Web.Controllers" });
            routes.MapRoute("SchoolDetailsLocality", "colegio/{townName}/{localityName}/{schoolName}/{schoolId}", new { Controller = "School", Action = "View" }, new { schoolId = "\\d+" }, new[] { "Colegios.Web.Controllers" });

            routes.MapRoute("SchoolSearch", "colegios", new { Controller = "School", Action = "Search" });
            routes.MapRoute("SchoolsLocality", "colegios/{cityName}/{townName}/{townId}", new { Controller = "School", Action = "Locality" }, new { townId = "\\d+"}, new[] { "Colegios.Web.Controllers" });
            routes.MapRoute("SchoolList", "colegios/{pageNo}", new { Controller = "School", Action = "List" });
            routes.MapRoute("SchoolLocalityMarks", "colegios/notas/{examType}/{year}/{cityName}/{cityId}/{townName}/{localityId}", new { Controller = "School", Action = "MarksLocality" }, new { localityId = "\\d+", year = "\\d+", cityId = "\\d+"}, new[] { "Colegios.Web.Controllers" });


            routes.MapRoute("NurserySearch", "guarderias", new { Controller = "Nursery", Action = "Index" });
            routes.MapRoute("NurseryList", "guarderias/{pageNo}", new { Controller = "Nursery", Action = "List" });
            routes.MapRoute("NurseriesLocality", "guarderias/{townName}/{localityName}/{localityId}", new { Controller = "Nursery", Action = "Locality" }, new { localityId = "\\d+" }, new[] { "Colegios.Web.Controllers" });

            routes.MapRoute("NurseryDetails", "guarderia/{nurseryName}/{nurseryId}", new { Controller = "Nursery", Action = "View" }, new { nurseryId = "\\d+" }, new[] { "Colegios.Web.Controllers" });
            routes.MapRoute("NurseryDetailsLocality", "guarderia/{townName}/{localityName}/{nurseryName}/{nurseryId}", new { Controller = "Nursery", Action = "View" }, new { nurseryId = "\\d+" }, new[] { "Colegios.Web.Controllers" });

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "School", action = "Index", id = UrlParameter.Optional },
                new[] { "Colegios.Web.Controllers" }// Parameter defaults
            );

        }

        //This method is called once per running web application, whenever application pool is recycled or
        //webapplication is restarted this event is called again
        //only called when created first instance of httpapplication
        protected void Application_Start()
        {
            //log4 set up
            log4net.Config.XmlConfigurator.Configure();

            RegisterGlobalFilters(GlobalFilters.Filters);

            var registering = new RegisteringComponents();

            ControllerBuilder.Current.SetControllerFactory(RegisteringComponents.GetControllerFactory(typeof(SchoolController).Assembly));
            var engine = registering.InitializeN2CMS();


            AreaRegistration.RegisterAllAreas();
            RegisterRoutes(RouteTable.Routes, engine);

        }

        private void InitializeNHibernateSession()
        {
            RegisteringComponents.InitializeNHibernate(webSessionStorage);

        }


        //private WebSessionStorage webSessionStorage;
        //private WebDbContextStorage webDbContextStorage;

        //public override void Init()
        //{
        //    base.Init();

        //     The WebSessionStorage must be created during the Init() to tie in HttpApplication events
        //    webSessionStorage = new WebSessionStorage(this);
        //    webDbContextStorage = new WebDbContextStorage(this);
        //}

        //protected void Application_Start()
        //{
        //    log4net.Config.XmlConfigurator.Configure();

        //    var componenetRegister = InitializeServiceLocator();

        //    var engine = componenetRegister.InitializeN2Engine();

        //    AreaRegistration.RegisterAllAreas(new AreaRegistrationState(engine));

        //    RegisterRoutes(RouteTable.Routes, engine);

        //    RegisterGlobalFilters(GlobalFilters.Filters);

        //    RegisterModelBinders(ModelBinders.Binders);

        //    RegisterCacheManagers(engine);

        //    Register the aspose license
        //    AsposeLicenseHelper.EnsureLicense();
        //}

        //private void RegisterModelBinders(ModelBinderDictionary binders)
        //{
        //    binders.DefaultBinder = new ReedModelBinder();
        //    binders.Add(typeof(JobSearchCriteria), new JobSearchCriteriaModelBinder(CookieLibrary.ReedPageSize));
        //    binders.Add(typeof(ReedOfficeSearchCriteria), new OfficeFinderSearchCriteriaModelBinder());
        //    binders.Add(typeof(RecruiterDirectoryCriteria), new RecruiterDirectoryCriteriaModelBinder());
        //    binders.Add(typeof(RegistrationModel), new RegistrationModelBinder());
        //    binders.Add(typeof(EmploymentViewModel), new EmploymentModelBinder());
        //    binders.Add(typeof(CourseSearchCriteria), new CourseSearchCriteriaModelBinder());
        //}

        //protected void Application_BeginRequest(object sender, EventArgs e)
        //{
        //    NHibernateInitializer.Instance().InitializeNHibernateOnce(InitializeNHibernateSession);
        //}

        protected void Application_Error(object sender, EventArgs e)
        {
            log.Error("Application Error");
            log.Error(sender.ToString());
            log.Error(e.ToString());
            return;
            //ExceptionLogModule.LogErrors(sender, e);
        }

        //private static readonly Type AreasJobHomeController = typeof(Areas.Job.Controllers.HomeController);

        //private static void RegisterGlobalFilters(GlobalFilterCollection filters)
        //{
        //    filters.Add(new ReloadAuthenticatedSessionAttribute());
        //    filters.Add(new DebugAttribute());

        //    var conditions = new Func<ControllerContext, ActionDescriptor, object>[]
        //                         {
        //                             (controllerContent, actionDescriptor) =>
        //                                 {
        //                                     var controllerType = controllerContent.Controller.GetType();
        //                                     var actionName = actionDescriptor.ActionName;

        //                                     if ((controllerType == AreasJobHomeController && actionName.StartsWith("view", StringComparison.OrdinalIgnoreCase)) ||
        //                                         controllerType.Namespace.StartsWith(ApiAreaRegistration.Namespace))
        //                                     {
        //                                         return null;
        //                                     }

        //                                     return new BasePageContentAttribute();
        //                                 }
        //                         };

        //    FilterProviders.Providers.Add(new ConditionalFilterProvider(conditions));
        //}

        //private static void RegisterCacheManagers(IEngine engine)
        //{
        //    engine.Persister.ItemMoved += (obj, sender) => CacheManager.ClearCache();
        //    engine.Persister.ItemSaved += (obj, sender) => CacheManager.ClearCache();
        //    engine.Persister.ItemDeleted += (obj, sender) => CacheManager.ClearCache();
        //    engine.Persister.ItemCopied += (obj, sender) => CacheManager.ClearCache();
        //}

        //private static void RegisterRoutes(RouteCollection routes, IEngine engine)
        //{
        //    var namespaces = new[] { "Reed.Web.Controllers" };

        //    routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
        //    routes.IgnoreRoute("{*favicon}", new { favicon = @"(.*/)?favicon.ico(/.*)?" });
        //    routes.IgnoreRoute("{folder}/{*pathInfo}", new { folder = "resources" });

        //    routes.MapContentRoute("Content", engine);

        //     >>> Here be Dragon's, get your Dragoon's on <<<

        //    routes.MapRoute("V9LocalJobs", "localjobs/{location}", new { controller = "v9tov10redirect", action = "localjobs", location = UrlParameter.Optional }, namespaces);
        //    routes.MapRoute("V9JobSearch", "job/searchresults.aspx", new { controller = "v9tov10redirect", action = "searchresults" });
        //    routes.MapRoute("V9JobDetails", "job/details.aspx", new { controller = "v9tov10redirect", action = "jobdetails" });
        //    routes.MapRoute("V9JobVacancies", "job-vacancies/{title}/{profileid}", new { controller = "v9tov10redirect", action = "jobvacancies", title = UrlParameter.Optional, profileid = UrlParameter.Optional });

        //     >>> End of Dragon Danger <<<

        //    routes.MapRoute("About", "about", new { controller = "home", action = "about" }, namespaces);
        //    routes.MapRoute("Location", "location/{location}/{pageno}", new { controller = "home", action = "location", location = UrlParameter.Optional, pageno = UrlParameter.Optional }, namespaces);
        //    routes.MapRoute("InternationalJobs", "internationaljobs", new { controller = "home", action = "internationaljobs" }, namespaces);

        //    routes.MapRoute(null, "office/{location}/{specialism}", new { controller = "office", action = "index" }, new { specialism = new OfficeSpecialismRouteConstraint() }, namespaces);
        //    routes.MapRoute(null, "office/{specialism}", new { controller = "office", action = "index" }, new { specialism = new OfficeSpecialismRouteConstraint() }, namespaces);
        //    routes.MapRoute(null, "office/{location}", new { controller = "office", action = "index" }, namespaces);

        //    routes.MapRoute("Home", "{controller}/{action}/{id}", new { controller = "home", action = "index", id = UrlParameter.Optional }, namespaces);
        //}

        //protected virtual ComponentRegistrar InitializeServiceLocator()
        //{
        //    var componentRegistrar = new ComponentRegistrar();

        //    componentRegistrar.RegisterComponent<IWebSearchService, WebSearchService>("webSearchService");
        //    componentRegistrar.RegisterComponent<IRecruiterDirectorySearchService, RecruiterDirectorySearchService>("recruiterDirectorySearchService");
        //    componentRegistrar.RegisterComponent<ISavedJobsSessionService, SavedJobsSessionService>("savedJobsSessionService");
        //    componentRegistrar.RegisterComponent<IWebAuthenticationService, WebAuthenticationService>("webAuthenticationService");
        //    ControllerBuilder.Current.SetControllerFactory(componentRegistrar.GetControllerFactory(typeof(HomeController).Assembly));

        //    return componentRegistrar;
        //}

        //private void InitializeNHibernateSession()
        //{
        //    ComponentRegistrar.InitializeNHibernate(webSessionStorage);
        //    NhibernateDbContext.Init(webDbContextStorage);
        //}
    }
}