using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServiceMonitor.Models.Performer
{
    public class PerformerGenerelListServices
    {
        public int Id { get; set; }
        public string CustomerName { get; set; }
        public string CustomerLastName { get; set; }
        public string CustomerPhoneNumber { get; set; }
        public string KindWork { get; set; }
        public double? Price { get; set; }
        public string CurrentStatus { get; set; }
    }
}