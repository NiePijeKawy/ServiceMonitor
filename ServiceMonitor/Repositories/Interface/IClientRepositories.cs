using ServiceMonitor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServiceMonitor.Models.Client;

namespace ServiceMonitor.Repositories
{
    public interface IClientRepositories
    {
        bool CheckClient(ClientGridViewModel model);
        IEnumerable<ClientListServicesViewModel> GetListServices(ClientGridViewModel model);
        IEnumerable<ClientServicesDetailsViewModel> GetDetails(int id);

        ClientServicesDetailsGeneralModel GetGeneralInfo(int id);
        ClientGridViewModel CheckClient2(ClientGridViewModel model);
        IEnumerable<ClientListServicesViewModel> GetListServices2(int id);
    }
}