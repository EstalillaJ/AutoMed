using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutoMed.Models
{   
    // TODO obfuscate this more (maybe?)
    public class ReportGenerator
    {   

        public static string GetColumn<T>(T entity, string columnName)
        {
            return Columns[typeof(T)][columnName](entity); // TODO Maybe make this exception more friendly
        }

        public static IEnumerable<string> GetColumnNames<T>()
        {
            return Columns[typeof(T)].Keys;
        }

        /**
        * To have a column be selectable from the report creation page add it here.
        */
        private static Dictionary<string, Func<dynamic, string>> QuoteColumns =
            new Dictionary<string, Func<dynamic, string>>()
            {   // Name Displayed To User, What It Returns
                { "Quote Id" , q => q.Id.ToString() },
                { "Number In Household", q => q.CurrentNumberInHousehold.ToString() },
                { "Date Created", q => q.DateCreated.ToShortDateString() },
                { "Date Reviewed", q => q.DateReviewed == null ? "None" : q.DateReviewed.ToShortDateString()  },
                { "Reviewer Username", q => q.ReviewedBy == null ? "None" : q.ReviewedBy.UserName },
                { "Reviewer Location", q => q.ReviewedBy == null ? "None" : q.ReviewedBy.Location.Name  },
                { "Creater Username", q => q.CreatedBy.UserName },
                { "Creater Location", q => q.CreatedBy.Location.Name },
                { "Discount (%)", q => q.DiscountPercentage.ToString() },
                { "Total Cost", q => q.TotalCost.ToString() },
                { "Discount ($)", q => q.DiscountDollars.ToString() },
                { "Approval Status", q => q.Approval.ToString() },
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

        private static Dictionary<Type, Dictionary<string, Func<dynamic, string>>> Columns =
            new Dictionary<Type, Dictionary<string, Func<dynamic, string>>>()
            {
                { typeof(Quote), QuoteColumns }
            };
    }
}