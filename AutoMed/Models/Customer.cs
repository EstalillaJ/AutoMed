﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutoMed.Models
{
    public class Customer
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        [NotMapped]
        public List<Quote> Quotes { get; set; }
        public List<Vehicle> Vehicles { get; set; }
        public int Age { get; set; }
        public Gender Gender { get; set; }
        public int Id { get; set; }
    }
}