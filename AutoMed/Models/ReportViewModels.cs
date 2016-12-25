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
        public CreateReportViewModel()
        {
            Locations = new List<Checkbox<Location>>();
        }
        [Display(Name="Include All Locations")]
        public bool AllLocations { get; set; }
        [Display(Name="Include All Discount Amounts")]
        public bool AnyDiscountAmount { get; set; }
        [Display(Name="Include All Dates")]
        public bool AnyDate { get; set; }
        public List<Checkbox<Location>> Locations { get; set; }
        public int SelectedLocationId { get; set; }
        [UIHint("WaterMarkInt")]
        [Display(Name = "Max Discount ($)", Prompt="Min ($)")]
        public double MaxDiscountDollars { get; set; }
        [Display(Name = "Min Discount ($)", Prompt="Max ($)")]
        [UIHint("WaterMarkInt")]
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