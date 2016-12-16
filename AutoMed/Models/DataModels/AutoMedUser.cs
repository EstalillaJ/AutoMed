using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
namespace AutoMed.Models.DataModels
{
    public class AutoMedUser
    {
        public string Name { get; set; }
        public int Id { get; set;}
        public int LocationId { get; set; }
        public Location Location { get; set; }
        public string Role { get; set; }
        public string Password { get; set; }
    }
}