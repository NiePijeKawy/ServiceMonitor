using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServiceMonitor.Models.Performer
{
    public class PerformerDetailsViewModel
    {
        public PerformerDetailsInfo Info { get; set; }

        public IEnumerable<PerformerDetailsWorkState> WorkState { get; set; }

    }
}