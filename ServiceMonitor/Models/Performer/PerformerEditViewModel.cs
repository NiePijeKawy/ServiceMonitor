using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ServiceMonitor.Models.Performer
{
    public class PerformerEditViewModel
    {
        public int Id { get; set; }
        public string ServiceName { get; set; }
        public string CurentStatus { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime UntilWhen { get; set; }
        [Required]
        public int IdNewStatus { get; set;}
        public IEnumerable<IdNameViewModel> WorksState { get; set; }
        public string Description { get; set; }
    }
}