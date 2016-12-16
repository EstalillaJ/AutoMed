using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Principal;

namespace AutoMed.Models
{   
    public class AutoMedUser : IPrincipal
    {   
        public enum Roles
        {
            Administrator = 3,
            Manager = 2,
            Employee = 1
        }
        public IIdentity Identity
        {
            get
            {
                return new GenericIdentity(this.Name);
            }
        }
        public string Name { get; set; }
        public int Id { get; set; }
        public Location Location { get; set; }
        public Roles Role { get; set; }
        public bool IsInRole(string stringRole)
        {
            Roles role;
            if (!Enum.TryParse(stringRole, out role))
                return false;
            else
                return this.Role >= role;
        }
    }
}