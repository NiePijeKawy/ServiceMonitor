using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServiceMonitor.Models
{
    public class ClientServicesDetailsViewModel
    {
        public string NameService { get; set; }

        public string Status { get; set; }

        public DateTime SinceWhen { get; set; }

        public DateTime? UntilWhen { get; set; }

        public string Description { get; set; }
    }
}