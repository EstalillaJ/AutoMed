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
using AutoMed.Models;
using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace AutoMed.Controllers
{
    public class QuotesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Quotes
        /// <summary>
        /// gets a list of quotes from the database
        /// </summary>
        /// <returns>list of quotes</returns>
        public ActionResult Index()
        {
            return View(db.Quotes.ToList());
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
                v => vehicleSelect.Add(new SelectListItem { Text = string.Format("{0} {1} {2}", v.Year, v.Make, v.Model), Value = v.Id.ToString() })
            );

            ViewBag.VehicleSelect = (IEnumerable<SelectListItem>)vehicleSelect;
            return View(new Quote() { CustomerId = id });
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
        public ActionResult Create([Bind(Include = "CustomerId,VehicleId,CurrentNumberInHousehold,TotalCost,WorkDescription")] Quote quote, List<HttpPostedFileBase> files)
        {
            if (ModelState.IsValid)
            {
                AutoMedUser loggedIn = db.Users.Where(x => x.UserName.Equals(User.Identity.Name)).Include("Location").First();

                quote.Documents = new List<Document>();
                quote.Approval = User.IsInRole("Administrator") || User.IsInRole("Manager") ? QuoteStatus.Accepted : QuoteStatus.Pending;
                files.ForEach(x => { if (x != null) quote.Documents.Add(new Document() { UploadedImage = x }); });
                quote.DateCreated = DateTime.Now;
                quote.DateReviewed = null;
                quote.LocationId = loggedIn.Location.Id;
                quote.CreatedById = loggedIn.Id;
                quote.SetDiscountPercentage();
                db.Quotes.Add(quote);
                db.SaveChanges();

                PostDocument(quote.Documents);
                return RedirectToAction("Details", new { id = quote.Id });
            }

            return View(quote);
        }

        // GET: Quotes/Edit/5
        /// <summary>
        /// gets an existing quote from database that can be edited. 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>quote view</returns>
        public ActionResult Edit(int? id)
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

        // POST: Quotes/Edit/5
        /// <summary>
        /// posts edited quote to database
        /// </summary>
        /// <param name="quote"></param>
        /// <param name="files"></param>
        /// <returns>redirects to index view or quote view</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CurrentNumberInHousehold,DiscountPercentage,TotalCost,Approval,WorkDescription")] Quote quote, List<HttpPostedFileBase> files)
        {
            if (ModelState.IsValid)

            {
                db.Quotes.Attach(quote);
                quote.Documents = new List<Document>();
                files.ForEach(x => { if (x != null) quote.Documents.Add(new Document() { UploadedImage = x }); });
                db.Entry(quote).Property(x => x.CurrentNumberInHousehold).IsModified = true;
                db.Entry(quote).Property(x => x.DiscountPercentage).IsModified = true;
                db.Entry(quote).Property(x => x.TotalCost).IsModified = true;
                db.Entry(quote).Property(x => x.Approval).IsModified = true;
                db.Entry(quote).Property(x => x.WorkDescription).IsModified = true;
                quote.SetDiscountPercentage();
                db.SaveChanges();
                PostDocument(quote.Documents);

                return RedirectToAction("Index");

            }

            return View(quote);

        }



        // GET: Quotes/Delete/5
        /// <summary>
        /// gets information of quote that is being deleted
        /// </summary>
        /// <param name="id"></param>
        /// <returns>view of quote</returns>
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
        public ActionResult DeleteConfirmed(int id)
        {
            
            Quote quote = db.Quotes.Find(id);
            db.Quotes.Remove(quote);
            db.SaveChanges();
            return RedirectToAction("Index");
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
        /// retrieves image from blob storage
        /// </summary>
        /// <param name="documentId"></param>
        /// <returns>returns file image</returns>
        [Authorize(Roles = "Manager,Administrator")]
        [Route("Document/Image/{documentId}")]
        // <img src="Document/Image/{documentId}" />
        public ActionResult GetImage(int documentId)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("BlobStorageConnection"));
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            // container name must be lowercase
            CloudBlobContainer container = blobClient.GetContainerReference("images");
            CloudBlockBlob blockBlob = container.GetBlockBlobReference(documentId.ToString());

            if (!blockBlob.Exists()) // Maybe you could do this with a call to the context instead.
                return new HttpNotFoundResult();

            MemoryStream ms = new MemoryStream();
            blockBlob.DownloadToStream(ms);
            return base.File(ms.ToArray(), blockBlob.Properties.ContentType ?? "image/png");
        }

        /// <summary>
        /// posts document images to blob storage
        /// </summary>
        /// <param name="documents"></param>
        private void PostDocument(List<Document> documents)
        {

            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("BlobStorageConnection"));
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            CloudBlobContainer container = blobClient.GetContainerReference("images");
            container.CreateIfNotExists();

            foreach (Document document in documents)
            {
                CloudBlockBlob blockBlob = container.GetBlockBlobReference(document.Id.ToString());

                blockBlob.Properties.ContentType = document.UploadedImage.ContentType;
                blockBlob.UploadFromStream(document.UploadedImage.InputStream);
            }

        }
    }
}
