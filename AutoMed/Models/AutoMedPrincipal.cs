using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Principal;

namespace AutoMed.Models
{
    public class AutoMedPrincipal : IPrincipal
    {
        public IIdentity Identity { get; set; }

        public string Role { get; set; }
        public bool IsInRole(string role)
        {
            
        }
    }
}