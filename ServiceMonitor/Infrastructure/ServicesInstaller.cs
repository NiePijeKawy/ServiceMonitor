using Castle.MicroKernel.Registration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using ServiceMonitor.Repositories;
using ServiceMonitor.Repositories.Interface;

namespace ServiceMonitor.Infrastructure
{
    public class ServicesInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<IClientRepositories>().ImplementedBy<ClientRepositories>());

            container.Register(Component.For<IPerformerRepositories>().ImplementedBy<PerformerRepositories>());

            container.Register(Component.For<SDA_EndProjContext>());

  

        }
    }
}