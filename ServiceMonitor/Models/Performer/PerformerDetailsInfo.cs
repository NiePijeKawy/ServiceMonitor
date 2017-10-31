using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServiceMonitor.Models.Performer
{
    public class PerformerDetailsInfo
    {
        public string CustomerFullName { get; set; }
        public string CustomerPhoneNumber { get; set; }
        public string CustomerVoivodeship { get; set; }
        public string CustomerCounty { get; set; }
        public string CustomerCommune { get; set; }
        public string CustomerLocality { get; set; }
        public string CustomerFullStreet { get; set; }

        public string NameOfService { get; set; }
        public double CostService { get; set; }

        public string PlotVoivodeship { get; set; }
        public string PlotCounty { get; set; }
        public string PlotCommune { get; set; }
        public string PlotLocality { get; set; }
        public string PlotNumber { get; set; }

        public string PODGiK_Name { get; set; }
        public string PODGiK_PhoneNumber { get; set; }
        public string PODGiK_WebSite { get; set; }

        public string PODGiK_Voivodeship { get; set; }
        public string PODGiK_County { get; set; }
        public string PODGiK_Commune { get; set; }
        public string PODGiK_Locality { get; set; }
        public string PODGiK_FullStreet { get; set; }


    }
}