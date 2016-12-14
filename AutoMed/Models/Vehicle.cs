using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutoMed.Models
{
    public class Vehicle
    {
        public String Vin { get; set; }

        public String Make { get; set; }

        public String Model { get; set; }

        public int Year { get; set; }

        protected int Id { get; set; }

    }
}