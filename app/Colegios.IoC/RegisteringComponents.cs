using System.Reflection;
using System.Web.Hosting;
using System.Web.Mvc;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Colegios.ApplicationServices;
using Colegios.ApplicationServices.Interfaces;
using Colegios.Query.Core.QueryInterfaces;
using CommonServiceLocator.WindsorAdapter;
using Microsoft.Practices.ServiceLocation;
using Colegios.Query;
using N2.Engine;
using N2.Engine.Castle;
using N2.Web.Mvc;
using SharpArch.NHibernate;
using SharpArch.Web.Mvc.Castle;
using Colegios.Command.Core.DataInterfaces;
using Colegios.Command.Data;

namespace Colegios.IoC
{
    public class RegisteringComponents
    {
 
        protected static IWindsorContainer windsorContainer = new WindsorContainer();

        public RegisteringComponents()
        {
            AddRepositoriesTo();
            AddApplicationServicesTo();
            windsorContainer.Register(Component.For<ISessionFactoryKeyProvider>().ImplementedBy<DefaultSessionFactoryKeyProvider>());
            ServiceLocator.SetLocatorProvider(() => new WindsorServiceLocator(windsorContainer));
        }

        private void AddApplicationServicesTo()
        {
            windsorContainer.Register(Component.For<ISchoolService>().ImplementedBy<SchoolService>());
            windsorContainer.Register(Component.For<INurseryService>().ImplementedBy<NurseryService>());
            
        }

        public IEngine InitializeN2CMS ()
        {
            var engine = MvcEngine.Create(new WindsorServiceContainer(windsorContainer));

            if (engine != null)
                engine.Initialize();

            return engine;
        }

        public static IControllerFactory GetControllerFactory(Assembly assemblyToRegister)
        {
            windsorContainer.RegisterControllers(assemblyToRegister);
            return new WindsorControllerFactory(windsorContainer);
        }

        private void AddRepositoriesTo()
        {
            windsorContainer.Register(Component.For<ILocationQuery>().ImplementedBy<LocationQuery>());
            windsorContainer.Register(Component.For<IEducationQuery>().ImplementedBy<EducationQuery>());
            windsorContainer.Register(Component.For<ISchoolQuery>().ImplementedBy<SchoolQuery>());
            windsorContainer.Register(Component.For<IOpinionQuery>().ImplementedBy<OpinionQuery>());
            windsorContainer.Register(Component.For<IPostQuery>().ImplementedBy<PostQuery>());

            windsorContainer.Register(Component.For<INurseryQuery>().ImplementedBy<NurseryQuery>());
            windsorContainer.Register(Component.For<IPostRepository>().ImplementedBy<PostRepository>());
            windsorContainer.Register(Component.For<IUserQuery>().ImplementedBy<UserQuery>());

            windsorContainer.Register(Component.For<INurseryModifiedRepository>().ImplementedBy<NurseryModifiedRepository>());
            windsorContainer.Register(Component.For<LocalityRepository>().ImplementedBy<LocalityRepository>());
            windsorContainer.Register(Component.For<CategoryFieldRepository>().ImplementedBy<CategoryFieldRepository>());

            windsorContainer.Register(Component.For<IUserRepository>().ImplementedBy<UserRepository>());
        }

        public static void InitializeNHibernate(ISessionStorage sessionStorage)
        { 
            //configure mapping files
            NHibernateSession.ConfigurationCache = new NHibernateConfigurationFileCache(new[] { "Colegios.Command.Core", "Colegios.Query.Core"});

            NHibernateSession.Init(
                sessionStorage,
                new[] { HostingEnvironment.MapPath("~/bin/Colegios.Command.Data.dll"), HostingEnvironment.MapPath("~/bin/Colegios.Query.dll") },
                HostingEnvironment.MapPath("~/NHibernate.config"));

        }

    }
}
