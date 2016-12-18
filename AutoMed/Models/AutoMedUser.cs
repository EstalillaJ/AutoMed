using System;
using System.Security.Principal;
using System.ComponentModel.DataAnnotations.Schema;

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
        [NotMapped]
        public IIdentity Identity
        {
            get
            {
                return new GenericIdentity(this.Name);
            }
        }
        public string Name { get; set; }
        public int Id { get; set; }
        public int LocationId { get; set; }
        public Location location { get; set; }
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