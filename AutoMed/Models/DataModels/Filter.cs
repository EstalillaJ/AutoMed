using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System;

namespace AutoMed.Models
{
    public class Filter
    {
        [Display(Name = "Filter Name")]
        public string Default_Name { get; set; }
        [Display(Name = "Max Amount")]
        public string MAX_Money { get; set; }
        [Display(Name = "Min Amount")]
        public string MIN_Money { get; set; }
        [Display(Name = "Max Percentage")]
        public string MAX_Percentage { get; set; }
        [Display(Name = "Min Percentage")]
        public string Min_Percentage { get; set; }
        [Display(Name = "Start Date")]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }
        [Display(Name = "End Date")]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }
        [Display(Name = "# in Household")]
        public string household { get; set; }
        [Display(Name = "Address")]
        public string Address { get; set; }
        [Display(Name = "Zip Code")]
        public string Zipe_Code { get; set; }
        public State State { get; set; }
        [Display(Name = "City")]
        public string City { get; set; }
        public int Id { get; set; }
    }
}