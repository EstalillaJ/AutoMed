using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutoMed.Models
{
    public class Quote
    {   
        public int Id { get; set; }
        public List<Document> Documents { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateApproved { get; set; }
        public AutoMedPrincipal ApprovedBy { get; set; }
        public AutoMedPrincipal CreatedBy { get; set; }
        public double Discount { get; set; }
        public bool isApproved { get; set; }
        public string WorkDescription { get; set; }
        public Location Location { get; set; }
        public Customer Customer { get; set; }
        public Vehicle Vehicle { get; set; }
    }
}