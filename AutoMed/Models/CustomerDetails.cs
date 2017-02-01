using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using AutoMed.Models;


namespace AutoMed.Models
{
    public class CustomerDetails
    {
        public IEnumerable<Vehicle> Vehicle { get; set; }

        public IEnumerable<Quote> Quote { get; set; }

        public IEnumerable<Customer> customer { get; set; }
    }
}