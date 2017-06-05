using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServiceMonitor.Models
{
    public class ClientListServicesViewModel
    {
        public int Id { get; set; }

        public string NameServices { get; set; }

        public string CompanyName  { get; set; }

        public double Price { get; set; }

        public string CurrentStatus { get; set; }

    }
}