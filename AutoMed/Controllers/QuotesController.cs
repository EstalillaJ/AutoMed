using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AutoMed.DAL;
using AutoMed.Models;

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

        // GET: Quotes/Create
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
        public ActionResult Create([Bind(Include = "CustomerId,VehicleId,CurrentNumberInHousehold,TotalCost,Approved,WorkDescription")] Quote quote, List<HttpPostedFileBase> files)
        {
            if (ModelState.IsValid)
            {
                // Grab the location of the logged in user. TODO do with cookies ? NOT A PRIORITY though
                AutoMedUser loggedIn = db.Users.Where(x => x.UserName.Equals(User.Identity.Name)).Include("Location").First();
                quote.Documents = new List<Document>();
                // We want to create some document objects in the quote. This way, when we add the qoute and call save changes, it will
                // insert the documents too. The image wont be saved because of the not mapped attribute but the document ids will be set.
                // We'll save the images right after db.SaveChanges
                files.ForEach(x => quote.Documents.Add(new Document() { UploadedImage = x }));
                quote.DateCreated = DateTime.Now;
                quote.DateReviewed = null;
                quote.LocationId = loggedIn.Location.Id;
                quote.CreatedById = loggedIn.Id;
                db.Quotes.Add(quote);
                db.SaveChanges();
                //UploadDocuments(quote.Documents);
                // SET THE DISCOUNT PERCENTAGE SOMEWHERE BEFORE THIS RETURN
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
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CurrentNumberInHousehold,DateCreated,DateReviewed,DiscountPercentage,TotalCost,Approved,WorkDescription")] Quote quote)
        {
            if (ModelState.IsValid)
            {
                db.Entry(quote).State = EntityState.Modified;
                db.SaveChanges();
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
    }
}
