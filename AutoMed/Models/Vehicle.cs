using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutoMed.Models
{
    public class Vehicle
    {
        public string Vin { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public string Color { get; set; }
        public int Year { get; set; }
        public string LicensePlateNumber { get; set; }
        public int Id { get; set; }
    }
}