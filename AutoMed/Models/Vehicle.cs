using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutoMed.Models
{
    public class Vehicle
    {
        public string Vin { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public Color Color { get; set; }
        public int Year { get; set; }
        public string LicensePlate { get; set; }
        [ForeignKey("Owner")]
        public int OwnerId { get; set; }
        public Customer Owner { get; set; }
        public int Id { get; set; }
    }
}