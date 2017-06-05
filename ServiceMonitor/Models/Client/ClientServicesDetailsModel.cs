using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServiceMonitor.Models.Client
{
    public class ClientServicesDetailsModel
    {
        public ClientServicesDetailsGeneralModel Info { get; set; }

        public IEnumerable<ClientServicesDetailsViewModel> State { get; set; }
    }
}