using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ServiceMonitor.Models.Performer
{
    public class PerformerAddNewService
    {
        public int Id { get; set; }

        public int IdPerformer { get; set; }
        public string LoginPerformer { get; set; }

        public int IdCustomer { get; set; }

        public IEnumerable<IdNameViewModel> Customers { get; set; }

        public int IdType { get; set; }

        public IEnumerable<IdNameViewModel> Types { get; set; }

        public float Price { get; set; }

        public int IdPodgik { get; set; }

        public IEnumerable<IdNameViewModel> Podgiks { get; set; }

        [Required]
        public int IdVoivodership { get; set; }// wartość zwracana jako wybrany element z drop downa

        public IEnumerable<IdNameViewModel> Voivoderships { get; set; }

        [Required]
        public int IdCounty { get; set; }

        public IEnumerable<IdNameViewModel> County { get; set; }

        [Required]
        public int IdCommune { get; set; }
        
        public IEnumerable<IdNameViewModel> Commune { get; set; }

        [Required]
        public int IdLocality { get; set; }

        public IEnumerable<IdNameViewModel> Locality { get; set; }
        
        public string Street { get; set; }

        [Required]
        public string PlotNumber { get; set; }

        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

    }
}