using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Principal;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutoMed.Models
{
    public class AutoMedIdentity : IIdentity
    {   
        [NotMapped]
        public string AuthenticationType { get; set; }

        [NotMapped]
        public bool IsAuthenticated { get; set; }

        public string Name { get; set; }

        public int ID { get; set; }

        public string Password { get; set; }

        public Location Location { get; set; }
    }
}