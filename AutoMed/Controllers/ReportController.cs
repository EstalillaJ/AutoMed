using System;
using System.Linq;
using System.Web.Mvc;
using AutoMed.Models;
using AutoMed.DAL;
using System.Text;
using System.Collections.Generic;
using LinqKit;

namespace AutoMed.Controllers
{   
    [Authorize(Roles="Administrator")]
    public class ReportController : Controller
    {
        private ApplicationDbContext Context = ApplicationDbContext.Create();
        public ActionResult Create()
        {
            CreateReportViewModel viewModel = new CreateReportViewModel();
            Context.Locations.ToList().ForEach(x => viewModel.Locations.Add(new Checkbox<Location>(x)));
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Create(CreateReportViewModel model)
        {
           List<Quote> matchedQuotes = GetMatchingQuotes(model);
           return File(Encoding.ASCII.GetBytes(GenerateReportString(matchedQuotes)), "text/plain", string.Format("Report_{0}.csv", DateTime.Now));
        }


        private List<Quote> GetMatchingQuotes(CreateReportViewModel model)
        {
            List<Quote> matchedQuotes = new List<Quote>();
            DateTime startDate = new DateTime(model.StartYear ?? 2015, (int?) model.StartMonth ?? 1, model.StartDay ?? 1);
            DateTime endDate = new DateTime(model.EndYear ?? 3000, (int?) model.EndMonth ?? 1, model.EndDay ?? 1);
            List<int> selectedLocations = model.Locations.Where(x => x.IsChecked).Select(x => x.Item.Id).ToList();
            double maxDiscountPercentage = model.MaxDiscountPercentage ?? double.MaxValue;
            double minDiscountPercentage = model.MinDiscountPercentage ?? double.MinValue;
            double maxDiscountDollars = model.MaxDiscountDollars ?? double.MaxValue;
            double minDiscountDollars = model.MinDiscountDollars ?? double.MinValue;
            string city = model.City ?? string.Empty;
            string address = model.City ?? string.Empty;

            ExpressionStarter<Quote> isMostlyMatched = PredicateBuilder.New<Quote>(q =>
                  (q.Customer.AddressLine1 + q.Customer.AddressLine2).Contains(address) &&
                  selectedLocations.Contains(q.Location.Id) &&
                  startDate <= q.DateCreated && q.DateCreated <= endDate &&
                  minDiscountPercentage <= q.DiscountPercentage && q.DiscountPercentage <= maxDiscountPercentage &&
                  minDiscountDollars <= q.TotalCost && q.TotalCost <= maxDiscountDollars &&
                  q.Customer.City.Contains(city));

            // These ones only work with exact equality
            ExpressionStarter<Quote> matchesZip, matchesState, matchesNumberInHousehold;
            matchesZip = matchesNumberInHousehold = matchesState = PredicateBuilder.New<Quote>(true);
            if (model.ZipCode != null)
            {
                matchesZip.DefaultExpression = q => q.Customer.ZipCode == model.ZipCode;
            }
            if (model.State != null)
            {
                matchesState.DefaultExpression = q => q.Customer.State == model.State;
            }
            if (model.NumberInHousehold != null)
            {
                matchesZip.DefaultExpression = q => q.CurrentNumberInHousehold == model.NumberInHousehold;
            }
        
            return Context.Quotes.Include("Location").Include("Vehicle").Include("Customer").
                        AsExpandable().
                        Where(isMostlyMatched.And(matchesZip.And(matchesState).And(matchesNumberInHousehold))).ToList();
        }

        private string GenerateReportString(List<Quote> quotes)
        {   
            // If we have time we should load these into the database and avoid hard-coding. Not a priority though.
            string columnFormatter = "{0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}, {10}, {11}, {12}, {13}, {14}, {15}, {16}\n";
            StringBuilder builder = new StringBuilder(string.Format(columnFormatter,
                "Quote Id",
                "Location",
                "Date Created",
                "Discount (%)",
                "Discount ($)",
                "# in Household",
                "First Name",
                "Last Name",
                "Email",
                "Phone #",
                "Address",
                "City",
                "State",
                "Zip",
                "Make",
                "Model",
                "Year"));

            foreach (Quote q in quotes)
            {
                builder.Append(string.Format(columnFormatter,
                    q.Id,
                    q.Location.Name,
                    q.DateCreated,
                    q.DiscountPercentage,
                    q.DiscountDollars,
                    q.CurrentNumberInHousehold,
                    q.Customer.FirstName,
                    q.Customer.LastName,
                    q.Customer.Email,
                    q.Customer.PhoneNumber,
                    q.Customer.AddressLine1 + q.Customer.AddressLine2,
                    q.Customer.City,
                    q.Customer.State,
                    q.Customer.ZipCode,
                    q.Vehicle.Make,
                    q.Vehicle.Model,
                    q.Vehicle.Year));
            }

            return builder.ToString();
        }
    }
}