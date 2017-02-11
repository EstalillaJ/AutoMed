using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutoMed.Models.DataModels
{
    public class BracketMapping
    {   
        public int Id { get; set; }
        public int LocationId { get; set; }
        public Location Location { get; set; }
        public double CustomPercentage { get; set; }
        public double BracketPercentage { get; set; } 
    }
}