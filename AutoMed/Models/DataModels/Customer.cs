using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AutoMed.Models.DataModels
{
    public class Customer
    {   
        [Display(Name="First Name")]
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Display(Name = "Address Line 1")]
        public string AddressLine1 { get; set; }
        [Display(Name = "Address Line 2")]
        public string AddressLine2 { get; set; }
        public State State { get; set; }
        [Display(Name = "ZIP Code")]
        public int ZipCode { get; set; }
        public string City { get; set; }
        public string Email { get; set; }
        [Display(Name = "Primary Phone Number")]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }
        public virtual List<Quote> Quotes { get; set; }
        public virtual List<Vehicle> Vehicles { get; set; }
        [Display(Name = "Birth Date")]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }
        public Sex Sex { get; set; }
        public int Id { get; set; }
    }
}