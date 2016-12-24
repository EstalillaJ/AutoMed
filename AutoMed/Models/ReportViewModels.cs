using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
namespace AutoMed.Models
{
    public class CreateReportViewModel
    {   
        [Display(Name="Include All Locations")]
        public bool AllLocations { get; set; }
        [Display(Name="Include All Discount Amounts")]
        public bool AnyDiscountAmount { get; set; }
        [Display(Name="Include All Dates")]
        public bool AnyDate { get; set; }
        public SelectList Locations { get; set; }
        public int SelectedLocationId { get; set; }
        [Display(Name = "Max Discount ($)")]
        public double MaxDiscountDollars { get; set; }
        [Display(Name = "Min Discount ($)")]
        public double MinDiscountDollars { get; set; }
        [Display(Name = "Year")]
        public int StartYear { get; set; }
        [Display(Name = "Month")]
        public int StartMonth { get; set; }
        [Display(Name = "Day")]
        public int StartDay { get; set; }
        [Display(Name = "Year")]
        public int EndYear { get; set; }
        [Display(Name = "Month")]
        public int EndMonth { get; set; }
        [Display(Name = "Day")]
        public int EndDay { get; set; }
    }
}