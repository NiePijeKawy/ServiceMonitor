using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ServiceMonitor.Startup))]
namespace ServiceMonitor
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
