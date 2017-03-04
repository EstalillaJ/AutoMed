using System;
using System.Linq;
using System.Web.Mvc;
using AutoMed.Models;
using AutoMed.DAL;
using System.Text;
using System.Collections.Generic;
using LinqKit;
using System.Data.Entity;
using AutoMed.Models.DataModels;
using AutoMed.Models.ViewModels;

namespace AutoMed.Controllers
{   
    [Authorize(Roles="Administrator")]
    public class ReportController : Controller
    {
        private ApplicationDbContext db = ApplicationDbContext.Create();
        public ActionResult Create()
        {
            CreateReportViewModel viewModel = new CreateReportViewModel();
            db.Locations.ToList().ForEach(x => viewModel.Locations.Add(new Checkbox<Location>(x)));
            return View(viewModel);
        }

        public ActionResult Details(CreateReportViewModel model)
        {
            List<Quote> matchedQuotes = GetMatchingQuotes(model);
            List<string> matchedColumns = model.ColumnsToInclude.Where(x => x.IsChecked).Select(x => x.Item).ToList();
            return View(new ReportDetailsViewModel() { Columns = matchedColumns, Quotes = matchedQuotes });
        }

        [HttpPost]
        public ActionResult Download(ReportDetailsViewModel model)
        {
           IEnumerable<int> quoteIds = model.Quotes.Select(x => x.Id);
           List<Quote> quotes = IncludeAllNavigationProperties(db.Quotes).Where(q => quoteIds.Contains(q.Id)).ToList();
           return File(Encoding.ASCII.GetBytes(GenerateReportString(quotes, model.Columns)), "text/plain", $"Report_{DateTime.Now}.csv");
        }


        private List<Quote> GetMatchingQuotes(CreateReportViewModel model)
        {
            List<int> selectedLocations = model.Locations.Where(checkbox => checkbox.IsChecked).Select(checkbox => checkbox.Item.Id).ToList();
            double maxDiscountPercentage = model.MaxDiscountPercentage ?? double.MaxValue;
            double minDiscountPercentage = model.MinDiscountPercentage ?? double.MinValue;
            double maxDiscountDollars = model.MaxDiscountDollars ?? double.MaxValue;
            double minDiscountDollars = model.MinDiscountDollars ?? double.MinValue;
            string city = model.City ?? string.Empty;
            string address = model.Address ?? string.Empty;
            model.StartDate = model.StartDate ?? DateTime.MinValue;
            model.EndDate = model.EndDate ?? DateTime.MaxValue;

            address = address.ToLower(); /* seems EF does something odd when we next this ToLower()
                                            call below (empty string never matches anything when it should match all) */

            ExpressionStarter<Quote> isMostlyMatched = PredicateBuilder.New<Quote>(q =>
                  (q.Customer.AddressLine1 + q.Customer.AddressLine2).ToLower().Contains(address) &&
                  selectedLocations.Contains(q.Location.Id) &&
                  model.StartDate <= q.DateCreated && q.DateCreated <= model.EndDate &&
                  minDiscountPercentage <= q.DiscountPercentage && q.DiscountPercentage <= maxDiscountPercentage &&
                  minDiscountDollars <= q.EligibleCost && q.EligibleCost <= maxDiscountDollars &&
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
                matchesNumberInHousehold.DefaultExpression = q => q.CurrentNumberInHousehold == model.NumberInHousehold;
            }

            return IncludeAllNavigationProperties(db.Quotes)
                        .AsExpandable()
                        .Where(isMostlyMatched.And(matchesZip.And(matchesState).And(matchesNumberInHousehold))).ToList();
        }
        
        //TODO find a generic way, or use multiple db sets
        private IQueryable<Quote> IncludeAllNavigationProperties (DbSet<Quote> set)
        {
            // TODO only eager load if the matched columns reference the property?
            // (Enforces naming convention in Report class) Add required property attribute to dictionary!

            // TODO is this really more efficient? http://stackoverflow.com/a/13820046 
            // Time it, and consider enabling multiple result sets

            return set.Include(x => x.Location)    // Yes it looks messy. It is more efficient to eager load
                .Include(x => x.Vehicle)     // when we know we will need the properties, then to make multiple queries
                .Include(x => x.Customer)
                .Include(x => x.ReviewedBy)
                .Include(x => x.CreatedBy)
                .Include(x => x.CreatedBy.Location)
                .Include(x => x.ReviewedBy.Location);
        }

        private string GenerateReportString(List<Quote> quotes, List<string> columns)
        {
            StringBuilder report = new StringBuilder(string.Empty);
            char seperator;
            // Column Headers
            for (int i = 0; i < columns.Count; i++)
            {
                seperator = i == columns.Count - 1 ? '\n' : ',';
                report.Append($"{columns[i]}{seperator}");
            }

            // Table Rows
            foreach (Quote q in quotes)
            {
                for (int i = 0; i < columns.Count; i++)
                {
                    seperator = i == columns.Count - 1 ? '\n' : ',';
                    report.Append($"{ReportGenerator.GetColumn(q, columns[i])} {seperator}");
                }
            }

            return report.ToString();
        }
    }
}