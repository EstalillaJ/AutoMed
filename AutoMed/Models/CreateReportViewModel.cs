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
            ColumnsToInclude = new List<Checkbox<string>>();
            foreach (string item in Report.Columns.Keys)
            {
                ColumnsToInclude.Add(new Checkbox<string>() { Item = item });
            }
        }
        public List<Checkbox<string>> ColumnsToInclude { get; set; }

        [Display(Name = "Include All Data")]
        public bool AllColumns { get; set; }

        [Display(Name="Include All Locations")]
        public bool AllLocations { get; set; }
        public List<Checkbox<Location>> Locations { get; set; }
        [Display(Prompt="Max ($)")]
        public double? MaxDiscountDollars { get; set; }
        [Display(Prompt="Min ($)")]
        public double? MinDiscountDollars { get; set; }
        [Display(Prompt = "Max (%)")]
        public double? MaxDiscountPercentage { get; set; }
        [Display(Prompt = "Min (%)")]
        public double? MinDiscountPercentage { get; set; }
        public Month? StartMonth { get; set; }
        public int? StartDay { get; set; }
        public int? StartYear { get; set; }
        public Month? EndMonth { get; set; }
        public int? EndDay { get; set; }
        public int? EndYear { get; set; }
        [Display(Prompt = "# in Household")]
        public int? NumberInHousehold { get; set; }
        [Display(Prompt = "Address")]
        public string Address { get; set; }
        [Display(Prompt = "Zip Code")]
        public int? ZipCode { get; set; }
        public State? State { get; set; }
        [Display(Prompt ="City")]
        public string City { get; set; }
    }

    public class ReportDetailsViewModel
    {
        public List<Quote> Quotes { get; set; }
        public List<string> Columns { get; set; }
    }
}