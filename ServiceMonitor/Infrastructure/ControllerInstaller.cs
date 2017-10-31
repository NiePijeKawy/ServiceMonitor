using Castle.MicroKernel.Registration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using System.Web.Mvc;
using System.Web.Http.Controllers;

namespace ServiceMonitor.Infrastructure
{
    public class ControllerInstaller : IWindsorInstaller
    {
        //public void Install(IWindsorContainer container, IConfigurationStore store)
        //{
        //    container.Register(Classes.FromThisAssembly().BasedOn<IController>().LifestyleTransient());

        //    container.Register(Classes.FromThisAssembly().BasedOn<IHttpController>().LifestyleTransient());
        //}

        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
            Classes.
                FromThisAssembly().
                BasedOn<IController>(). //MVC
                If(c => c.Name.EndsWith("Controller")).
                LifestyleTransient());

            container.Register(
                Classes.
                    FromThisAssembly().
                    BasedOn<IHttpController>(). //Web API
                    If(c => c.Name.EndsWith("Controller")).
                    LifestyleTransient());
        }
    }
}