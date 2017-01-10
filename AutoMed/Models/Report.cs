using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutoMed.Models
{   
    // TODO obfuscate this more (maybe?)
    public class Report
    {
        /**
         * To have a column be selectable from the report creation page add it here.
         */
        public static readonly Dictionary<string, Func<Quote, string>> Columns =
            new Dictionary<string, Func<Quote, string>>()
            {   // Name Displayed To User, What It Returns
                { "Quote Id" , q => q.Id.ToString() },
                { "Number In Household", q => q.CurrentNumberInHousehold.ToString() },
                { "Date Created", q => q.DateCreated.ToString() },
                { "Date Reviewed", q => q.DateReviewed.ToString() },
                { "Reviewer Username", q => q.ReviewedBy == null ? "None" : q.ReviewedBy.UserName },
                { "Reviewer Location", q => q.ReviewedBy == null ? "None" : q.ReviewedBy.Location.Name  },
                { "Creater Username", q => q.CreatedBy.UserName },
                { "Creater Location", q => q.CreatedBy.Location.Name },
                { "Discount (%)", q => q.DiscountPercentage.ToString() },
                { "Total Cost", q => q.TotalCost.ToString() },
                { "Discount ($)", q => q.DiscountDollars.ToString() },
                { "Approved", q => q.Approved.ToString() },
                { "Work Description", q => q.WorkDescription },
                { "Quote Location", q => q.Location.Name },
                { "Customer First Name",  q => q.Customer.FirstName},
                { "Customer Last Name", q => q.Customer.LastName },
                { "Customer Address", q => string.Format("{0}\n{1}", q.Customer.AddressLine1, q.Customer.AddressLine2) },
                { "Customer Birthdate", q => q.Customer.BirthDate.ToShortDateString() },
                { "Customer City", q => q.Customer.City },
                { "Customer ZipCode", q => q.Customer.ZipCode.ToString() },
                { "Customer Sex" , q => q.Customer.Sex.ToString() },
                { "Customer Email", q => q.Customer.Email },
                { "Customer Phone #", q => q.Customer.PhoneNumber },
                { "Vehicle Make", q => q.Vehicle.Make },
                { "Vehicle Model", q => q.Vehicle.Model },
                { "Vehicle Year", q => q.Vehicle.Year.ToString() },
                { "Vehicle VIN", q => q.Vehicle.Vin.ToString() },
                { "Vehicle License Plate", q => q.Vehicle.LicensePlate },
            };
    }
}