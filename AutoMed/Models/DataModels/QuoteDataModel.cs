using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using AutoMed.Models.DataModels;

namespace AutoMed.Models.DataModels
{
    public class QuoteDataModel
    {   

        public QuoteDataModel(Quote quote)
        {
            this.Id = quote.Id;
            this.Documents = quote.Documents;
            this.DateApproved = quote.DateApproved;
            this.DateCreated = quote.DateCreated;
            this.Customer = quote.Customer;
            this.Location = quote.Location;
            this.Vehicle = quote.Vehicle;
            this.WorkDescription = quote.WorkDescription;
            this.Discount = quote.Discount;
            this.LocationId = Location.Id;
            this.CustomerId = Customer.Id;
            this.VehicleId = Vehicle.Id;
        }

        public QuoteDataModel() { }
        public int Id { get; set; }
        public List<Document> Documents { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateApproved { get; set; }
        [ForeignKey("ApprovedBy")]
        public int? ApprovedById { get; set; }
        public AutoMedUserDataModel ApprovedBy { get; set; }
        [ForeignKey("CreatedBy")]
        public int CreatedById { get; set; }
        public AutoMedUserDataModel CreatedBy { get; set; }
        public double Discount { get; set; }
        public string WorkDescription { get; set; }
        public int LocationId { get; set; }
        public Location Location { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public int VehicleId { get; set; }
        public Vehicle Vehicle { get; set; }

        public Quote ToQuote()
        {
            return new Quote()
            {
                ApprovedBy = this.ApprovedBy.ToPrincipal(),
                CreatedBy = this.CreatedBy.ToPrincipal(),
                Location = this.Location,
                Customer = this.Customer,
                Vehicle = this.Vehicle,
                WorkDescription = this.WorkDescription,
                Discount = this.Discount,
                Documents = this.Documents,
                DateApproved = this.DateApproved,
                DateCreated = this.DateCreated
            };
        }
    }
}