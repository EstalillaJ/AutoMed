using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutoMed.Models.DataModels
{
    public class Quote
    {
        public int Id { get; set; }
        [Display(Name = "# of People in Household")]
        public int CurrentNumberInHousehold { get; set; }
        [Display(Name = "Total Income")]
        public double Income { get; set; }
        [Display(Name = "Total Expenses")]
        public double Expenses { get; set; }

        [NotMapped]
        public double AdjustedIncome
        {
            get { return Income - Expenses; }
        }
        public virtual List<Document> Documents { get; set; }
        [Display(Name = "Date Created")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateCreated { get; set; }
        [Display(Name = "Date Reviewed")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? DateReviewed { get; set; }
        public string ReviewedById { get; set; }
        public AutoMedUser ReviewedBy { get; set; }
        public string CreatedById { get; set; }
        public AutoMedUser CreatedBy { get; set; }
        [Display(Name = "Calculated Discount (%)")]
        public double DiscountPercentage { get; set; }
        [Display(Name = "Total Cost")]
        public double TotalCost { get; set; }
        [NotMapped]
        public double DiscountDollars { get { return TotalCost * DiscountPercentage; } }
        [Display(Name = "Approval Status")]
        public QuoteStatus Approval { get; set; }
        [Display(Name = "Work Description")]
        public string WorkDescription { get; set; }
        public int LocationId { get; set; }
        public virtual Location Location { get; set; }
        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; }
        public int VehicleId { get; set; }
        public virtual Vehicle Vehicle { get; set; }
    }
}