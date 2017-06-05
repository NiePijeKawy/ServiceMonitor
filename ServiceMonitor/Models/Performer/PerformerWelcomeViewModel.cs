using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ServiceMonitor.Models.Performer
{
    public class PerformerWelcomeViewModel
    {
        [Required]
        public string Login { get; set; }

        [Required]
        [PasswordPropertyText]
        public string Password { get; set; }
    }
}