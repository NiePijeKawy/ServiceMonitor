using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Castle.Windsor;
using Castle.Windsor.Installer;
using ServiceMonitor.Repositories;
using ServiceMonitor.Infrastructure;

namespace ServiceMonitor
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            BoostratpContainer();
        }

        protected void Application_End()
        {
            _container.Dispose();
        }

        private static IWindsorContainer _container;

        private static void BoostratpContainer()
        {
            _container = new WindsorContainer().Install(FromAssembly.This());

            //_container.Install(new ServicesInstaller());
            //_container.Install(new ControllerInstaller());

            var controlleFactory = new WindsorControllerFactory(_container.Kernel);

            ControllerBuilder.Current.SetControllerFactory(controlleFactory);
        }
    }
}
