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
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public List<Quote> Quotes { get; set; }
        public List<Vehicle> Vehicles { get; set; }
        public int Age { get; set; }
        public Gender Gender { get; set; }
        public int Id { get; set; }
    }
}