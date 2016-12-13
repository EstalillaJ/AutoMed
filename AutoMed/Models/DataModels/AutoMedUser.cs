using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutoMed.Models.DataModels
{
    public class AutoMedUser
    {
        public string Name { get; set; }
        public int ID { get; set;}
        public Location Location { get; set; }
        public string Role { get; set; }
        public string Password { get; set; }
    }
}