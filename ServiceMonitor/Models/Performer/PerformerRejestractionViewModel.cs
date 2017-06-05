using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Dynamic;
using System.Linq;
using System.Web;

namespace ServiceMonitor.Models.Performer
{
    public class PerformerRejestractionViewModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Login { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string PhoneNumber { get; set; }

        public IEnumerable<IdNameViewModel> Voivoderships { get; set; }
        [Required]
        public int VoivodershipId { get; set; }// wartość zwracana jako wybrany element z drop downa

        public IEnumerable<IdNameViewModel> County { get; set; }
        [Required]
        public int CountyId { get; set; }
      
        public IEnumerable<IdNameViewModel> Commune { get; set; }
        [Required]
        public int CommuneId { get; set; }

        public IEnumerable<IdNameViewModel> Locality { get; set; }
        [Required]
        public int LocalityId { get; set; }
        [Required]
        public string Street { get; set; }
        public string HouseNumber { get; set; }

    }
}