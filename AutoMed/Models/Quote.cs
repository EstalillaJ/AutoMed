using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace AutoMed.Models
{
    public class Quote
    {
        public int Id { get; set; }
        public int CurrentNumberInHousehold { get; set; }
        public virtual List<Document> Documents { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateReviewed { get; set; }
        public int ReviewedById { get; set; }
        public AutoMedUser ReviewedBy { get; set; }
        [ForeignKey("CreatedBy")]
        public string CreatedById { get; set; }
        public AutoMedUser CreatedBy { get; set; }
        public double DiscountPercentage { get; set; }
        public double TotalCost { get; set; }
        [NotMapped]
        public double DiscountDollars { get { return TotalCost * DiscountPercentage; } }
        public bool Approved { get; set; }
        [DataType(DataType.MultilineText)]
        public string WorkDescription { get; set; }
        public int LocationId { get; set; }
        public virtual Location Location { get; set; }
        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; }
        public int VehicleId { get; set; }
        public virtual Vehicle Vehicle { get; set; }
        public double AnnualIncome { get; set; }
    }
}