using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace AutoMed.Models
{
    public class Quote
    {
        public int Id { get; set; }
        public List<Document> Documents { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateReview { get; set; }
        public AutoMedUser ReviewdBy { get; set; }
        public AutoMedUser CreatedBy { get; set; }
        public double Discount { get; set; }
        public bool Approved { get; set; }
        public string WorkDescription { get; set; }
        public Location Location { get; set; }
        public Customer Customer { get; set; }
        public Vehicle Vehicle { get; set; }
    }
}