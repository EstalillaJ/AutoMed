using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutoMed.Models
{
    public class Customer
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        protected int id { get; set; }

        public String Address { get; set; }

        public String Email { get; set; }

        public List<Quote> Quotes { get; set; }

        public List<Vehicle> Vehicles { get; set; }

        public int Age { get; set; }

        public Gender Gender { get; set; }

    }
}