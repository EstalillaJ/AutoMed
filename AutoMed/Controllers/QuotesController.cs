using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AutoMed.DAL;
using System.Net.Mime;
using AutoMed.Models;
using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System.Web.Configuration;
using AutoMed.Models.DataModels;

namespace AutoMed.Controllers
{
    public class QuotesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Quotes
        /// <summary>
        /// gets a list of quotes unapproved quotes at the current users location returns a view of them
        /// </summary>
        /// <returns>view of quotes</returns>
        [Authorize(Roles = "Administrator,Manager")]
        public ActionResult Index()
        {
            List<Quote> quotes;

            if (User.IsInRole("Administrator"))
                quotes = db.Quotes.Where(x => x.Approval == QuoteStatus.Pending)
                                  .OrderByDescending(x => x.DateCreated)
                                  .ToList();
            else
            {
                int locationId = db.Users.Single(x => x.UserName == User.Identity.Name).Location.Id;
                quotes = db.Quotes.Where(x => x.Approval == QuoteStatus.Pending && locationId == x.Location.Id)
                                  .OrderByDescending(x => x.DateCreated)
                                  .ToList();
            }

            return View(quotes);
        }

        // GET: Quotes/Details/5
        /// <summary>
        /// gets quote details for a specific customer from the database
        /// </summary>
        /// <param name="id"></param>
        /// <returns>view of quote</returns>
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Quote quote = db.Quotes.Find(id);
            if (quote == null)
            {
                return HttpNotFound();
            }
            return View(quote);
        }

        // GET: Quotes/Create/1
        /// <summary>
        /// gets quote creation form for a specific customer, and populates it with vehicles that 
        /// are already associated to that customer ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Quote creation form for existing customer</returns>
        [Authorize(Roles = "Employee,Manager,Administrator")]
        public ActionResult Create(int id)
        {
            List<SelectListItem> vehicleSelect = new List<SelectListItem>();
            // Get a list of all the vehicles they own. Create a List<SelectListItem> using those vehicles (this will be used in the view)
            db.Customers.Find(id).Vehicles.ForEach(
                v => vehicleSelect.Add(new SelectListItem { Text = $"{v.Year} {v.Make} {v.Model}", Value = v.Id.ToString() })
            );

            ViewBag.VehicleSelect = vehicleSelect;
            return View(new Quote() { CustomerId = id, Customer = db.Customers.Find(id) });
        }

        // POST: Quotes/Create
        /// <summary>
        /// posts to database a new quote for a customer along with document images that have been uploaded.
        /// </summary>
        /// <param name="quote"></param>
        /// <param name="files"></param>
        /// <returns>quote view</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Employee,Manager,Administrator")]
        public ActionResult Create([Bind(Include = "CustomerId,VehicleId,CurrentNumberInHousehold,MandatoryCost,EligibleCost,Income,Expenses,WorkDescription")] Quote quote, List<HttpPostedFileBase> files)
        {
            if (ModelState.IsValid)
            {
                AutoMedUser loggedIn = db.Users.Where(x => x.UserName.Equals(User.Identity.Name)).Include("Location").First();

                quote.Documents = new List<Document>();
                for (int i = 0; i < files.Count; i++)
                { 
                        if (i != files.Count -1) quote.Documents.Add(new Document() {UploadedImage = files[i]});
                }
                quote.DateCreated = DateTime.Now;
                quote.LocationId = loggedIn.Location.Id;
                quote.CreatedById = loggedIn.Id;
                quote.Location = db.Locations.Find(quote.LocationId);
                SetDiscount(quote);

                string redir;
                if (User.IsInRole("Administrator") || User.IsInRole("Manager"))
                {
                    quote.Approval = QuoteStatus.Accepted;
                    quote.DateReviewed = quote.DateCreated;
                    redir = nameof(Edit);
                }
                else
                {
                    quote.Approval = QuoteStatus.Pending;
                    quote.DateReviewed = null;
                    redir = nameof(Details);
                }

                db.Quotes.Add(quote);
                db.SaveChanges();
                UploadDocumentBlobs(quote.Documents);

                return RedirectToAction(redir, new { id = quote.Id });
            }

            return View(quote);
        }

        // GET: Quotes/Edit/5
        /// <summary>
        /// gets an existing quote from database that can be edited. 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>quote view</returns>
        [Authorize(Roles = "Manager,Administrator")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Quote quote = db.Quotes.Include(x => x.CreatedBy).First(x => x.Id == id);
            List<SelectListItem> vehicleSelect = new List<SelectListItem>();

            db.Customers.Find(quote.CustomerId).Vehicles.ForEach(
                   v => vehicleSelect.Add(new SelectListItem { Text = $"{v.Year} {v.Make} {v.Model}", Value = v.Id.ToString() })
               );

            ViewBag.VehicleSelect = vehicleSelect;

            return View(quote);
        }

        // POST: Quotes/Edit/5
        /// <summary>
        /// posts edited quote to database
        /// </summary>
        /// <param name="quote"></param>
        /// <param name="files"></param>
        /// <returns>redirects to index view or quote view</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Manager,Administrator")]
        public ActionResult Edit([Bind(Include = "Id,CurrentNumberInHousehold,Income,Expenses,DiscountPercentage,EligibleCost,LocationId,MandatoryCost,Approval,WorkDescription")] Quote quote, List<HttpPostedFileBase> files)
        {
            if (ModelState.IsValid)
            {
                quote.Location = db.Locations.Find(quote.LocationId);
                db.Quotes.Attach(quote);
                quote.Documents = new List<Document>();

                for (int i = 0; i < files.Count; i++)
                {
                    if (i != files.Count - 1)
                        quote.Documents.Add(new Document() { UploadedImage = files[i] });
                }

                db.Entry(quote).Property(x => x.CurrentNumberInHousehold).IsModified = true;
                db.Entry(quote).Property(x => x.DiscountPercentage).IsModified = true;
                db.Entry(quote).Property(x => x.EligibleCost).IsModified = true;
                db.Entry(quote).Property(x => x.Approval).IsModified = true;
                db.Entry(quote).Property(x => x.WorkDescription).IsModified = true;

                SetDiscount(quote);
                db.SaveChanges();
                UploadDocumentBlobs(quote.Documents);

                return RedirectToAction(nameof(Edit), new {id = quote.Id});
            }

            return View(quote);
        }

        /// <summary>
        ///  Recieves a list of quotes from a view and udpates their status in the database
        /// </summary>
        /// <param name="quotes">List of quotes to have their statuses changed</param>
        /// <returns>The Quotes Index View</returns>
        [HttpPost]
        [Authorize(Roles = "Manager,Administrator")]
        public ActionResult UpdateQuoteStatuses(List<Quote> quotes)
        {
            foreach (Quote quote in quotes)
            {
                db.Quotes.Attach(quote);
                db.Entry(quote).Property(x => x.Approval).IsModified = true;
                quote.ReviewedBy = db.Users.First(x => x.UserName.Equals(User.Identity.Name));
            }
            db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        // GET: Quotes/Delete/5
        /// <summary>
        /// gets information of quote that is being deleted
        /// </summary>
        /// <param name="id"></param>
        /// <returns>view of quote</returns>
        [Authorize(Roles = "Administrator")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Quote quote = db.Quotes.Find(id);
            if (quote == null)
            {
                return HttpNotFound();
            }
            return View(quote);
        }

        // POST: Quotes/Delete/5
        /// <summary>
        /// removes quote from database
        /// </summary>
        /// <param name="id"></param>
        /// <returns>redirects to quote index view</returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult DeleteConfirmed(int id)
        {
            
            Quote quote = db.Quotes.Find(id);
            if (quote != null)
            {
                db.Quotes.Remove(quote);
                db.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        /// <summary>
        /// retrieves images associated with a customer from blob storage
        /// </summary>
        /// <param name="documentId"></param>
        /// <returns>returns file image</returns>
        [Authorize(Roles = "Manager,Administrator,Employee")]
        [Route("Document/Image/{documentId}")]
        // <img src="Document/Image/{documentId}" />
        public ActionResult GetImage(int documentId)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(WebConfigurationManager.AppSettings["BlobStorageConnection"]);
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            // container name must be lowercase
            CloudBlobContainer container = blobClient.GetContainerReference(WebConfigurationManager.AppSettings["BlobContainer"]);
            CloudBlockBlob blockBlob = container.GetBlockBlobReference(documentId.ToString());

            if (!blockBlob.Exists())
                return new HttpNotFoundResult();

            MemoryStream ms = new MemoryStream();
            blockBlob.DownloadToStream(ms);
            return File(ms.ToArray(), blockBlob.Properties.ContentType ?? "image/png");
        }

        /// <summary>
        /// posts document images to blob storage for a customer
        /// </summary>
        /// <param name="documents"></param>
        private void UploadDocumentBlobs(List<Document> documents)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(WebConfigurationManager.AppSettings["BlobStorageConnection"]);
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            CloudBlobContainer container = blobClient.GetContainerReference(WebConfigurationManager.AppSettings["BlobContainer"]);
            container.CreateIfNotExists();

            foreach (Document document in documents)
            {
                CloudBlockBlob blockBlob = container.GetBlockBlobReference(document.Id.ToString());

                blockBlob.Properties.ContentType = document.UploadedImage.ContentType;
                blockBlob.UploadFromStream(document.UploadedImage.InputStream);
            }
        }

        private void SetDiscount(Quote quote)
        {   
            // Fallback to last year's scale if this year's hasn't been entered
            Scale scale = db.Scales.SingleOrDefault(x => x.Year == DateTime.Now.Year) ?? db.Scales.Single(x => x.Year == DateTime.Now.Year - 1);

            double baseIncome = quote.CurrentNumberInHousehold < 9 ?
                                scale.IncomeBrackets.Single(b => b.NumInHousehold == quote.CurrentNumberInHousehold).Income:
                                scale.IncomeBrackets.Single(b => b.NumInHousehold == 8).Income + scale.AdditionalPersonBase * (quote.CurrentNumberInHousehold - 8);

            quote.Location.BracketMappings.Sort((x, y) => x.PovertyLevel.CompareTo(y.PovertyLevel));

            
            if (quote.AdjustedIncome >= quote.Location.PovertyLevelCutoff / 100.0 * baseIncome)
            {
                quote.DiscountPercentage = 0;
                return;
            }
   
            if (quote.Location.BracketMappings.First().PovertyLevel / 100.0 * baseIncome > quote.AdjustedIncome)
            {
                quote.DiscountPercentage = quote.Location.BracketMappings[0].Discount;
                return;
            }

            for (int i = 0; i < quote.Location.BracketMappings.Count; i++)
            {   
                if (i == quote.Location.BracketMappings.Count - 1 || // Short circuit at the end
                    quote.Location.BracketMappings[i].PovertyLevel / 100.0 * baseIncome <= quote.AdjustedIncome
                    && quote.AdjustedIncome < quote.Location.BracketMappings[i + 1].PovertyLevel / 100.0 * quote.AdjustedIncome)
                {
                    quote.DiscountPercentage = quote.Location.BracketMappings[i].Discount;
                    return;
                }
            }
        }
    }
}
