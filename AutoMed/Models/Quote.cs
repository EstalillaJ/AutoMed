using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
namespace AutoMed.Models
{
    public class Quote
    {
        public int Id { get; set; }
        public List<Document> Documents { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateApproved { get; set; }
        [ForeignKey("ApprovedBy")]
        public int? ApprovedById { get; set; }
        public AutoMedPrincipal ApprovedBy { get; set; }
        [ForeignKey("CreatedBy")]
        public int CreatedById { get; set; }
        public AutoMedPrincipal CreatedBy { get; set; }
        public double Discount { get; set; }
        public bool IsApproved { get; set; }
        public string WorkDescription { get; set; }
        public int LocationId { get; set; }
        public Location Location { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public int VehicleId { get; set; }
        public Vehicle Vehicle { get; set; }

    }
}