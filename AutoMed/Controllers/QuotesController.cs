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
        public ActionResult Index()
        {
            return View(db.Quotes.ToList());
        }

        // GET: Quotes/Details/5
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
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
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
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
 
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


        [Authorize(Roles = "Manager,Administrator")]
        [Route("Document/Image/{documentId}")]
        // I had the return type as FileContentResult but changed to return 404. Let me know if doesnt work.
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

        // Obviously this wont work unless the quote already exists. Feel free to move this code wherever
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
