﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using AutoMed.Models.DataModels;

namespace AutoMed.Models.ViewModels
{   
    public class CreateReportViewModel
    {
        public CreateReportViewModel()
        {
            Locations = new List<Checkbox<Location>>();
            ColumnsToInclude = new List<Checkbox<string>>();
            foreach (string item in ReportGenerator.GetColumnNames<Quote>())
            {
                ColumnsToInclude.Add(new Checkbox<string> { Item = item });
            }
        }
        public int Id { get; set; }
        public List<Checkbox<string>> ColumnsToInclude { get; set; }

        [Display(Name = "All")]
        public bool AllColumns { get; set; }

        [Display(Name="All")]
        public bool AllLocations { get; set; }
        public List<Checkbox<Location>> Locations { get; set; }
        [Display(Name="Max ($)")]
        public double? MaxDiscountDollars { get; set; }
        [Display(Name="Min ($)")]
        public double? MinDiscountDollars { get; set; }
        [Display(Name = "Max (%)")]
        public double? MaxDiscountPercentage { get; set; }
        [Display(Name = "Min (%)")]
        public double? MinDiscountPercentage { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Start Date")]
        public DateTime? StartDate { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "End Date")]
        public DateTime? EndDate { get; set; }
        [Display(Name = "# in Household")]
        public int? NumberInHousehold { get; set; }
        [Display(Name = "Address")]
        public string Address { get; set; }
        [Display(Name = "Zip Code")]
        public int? ZipCode { get; set; }
        public State? State { get; set; }
        [Display(Name ="City")]
        public string City { get; set; }
    }

    public class ReportDetailsViewModel
    {
        public List<Quote> Quotes { get; set; }
        public List<string> Columns { get; set; }
    }
}