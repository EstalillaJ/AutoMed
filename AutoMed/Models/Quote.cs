using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using AutoMed.Models.DataModels;

namespace AutoMed.Models
{
    public class Quote
    {
        public int Id { get; set; }
        public List<Document> Documents { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateApproved { get; set; }
        public AutoMedUser ApprovedBy { get; set; }
        public AutoMedUser CreatedBy { get; set; }
        public double Discount { get; set; }
        public bool IsApproved { get { return ApprovedBy != null; } }
        public string WorkDescription { get; set; }
        public Location Location { get; set; }
        public Customer Customer { get; set; }
        public Vehicle Vehicle { get; set; }
    }
}