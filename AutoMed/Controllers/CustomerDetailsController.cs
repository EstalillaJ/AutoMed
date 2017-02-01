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
using System.Data.Entity.Infrastructure;



namespace AutoMed.Controllers
{
    public class customerDetailsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: customerDetails
        public ActionResult Index(int? id)//db.Customers.Include("Quotes").Include("Vehicles").Where(q => q.id == id).FirstOrDefault(); 
        {
            var viewModel = new Customer();

            
            Customer customer = db.Customers.Include("Vehicles").Include("Quotes").Where(q => q.Id == id).FirstOrDefault(); 


            /*viewModel.customer = db.Customers.Include(i=>i.Vehicles).Include(i=>i.Quotes);
            if (id != null)
            {
                ViewBag.Id = id.Value;
                viewModel.Vehicle = viewModel.customer.Where(
                    i => i.Id == id.Value).Single().Vehicles;
            }

            if (id != null)
            {
                ViewBag.Id = id.Value;
                viewModel.Quote = viewModel.customer.Where(
                    x => x.Id == id.Value).Single().Quotes;
            }*/

            viewModel.Id = (from a in db.Customers.Where(a => a.Id == id) select a.Id).FirstOrDefault();
            
           // viewModel.Vehicles = db.Vehicles.Where(v =>v.cToList();
//viewModel.Quotes = db.Quotes.ToList();

            return View(customer);
        }
        // GET: Vehicles/Details/5
        public ActionResult Details_V(int? id, Customer customer)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vehicle vehicle = db.Vehicles.Find(id);
            if (vehicle == null)
            {
                return HttpNotFound();
            }
            return View(vehicle);
        }

        // GET: Vehicles/Create
        public ActionResult Create_V(int id)
        {
            return View(new Vehicle() { OwnerId = id });
        }

        // POST: Vehicles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create_V([Bind(Include = "Id,Vin,Make,Model,Color,Year,LicensePlate,OwnerId")] Vehicle vehicle, Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Vehicles.Add(vehicle);
                db.SaveChanges();
                return RedirectToAction("Index",new { id = customer.Id });
            }

            return View(vehicle);
        }

        // GET: Vehicles/Edit/5
        public ActionResult Edit_V(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vehicle vehicle = db.Vehicles.Find(id);
            if (vehicle == null)
            {
                return HttpNotFound();
            }
            return View(vehicle);
        }

        // POST: Vehicles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit_V([Bind(Include = "Id,Vin,Make,Model,Color,Year,LicensePlate,OwnerId")] Vehicle vehicle, Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(vehicle).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", new { id = customer.Id });
            }
            return View(vehicle);
        }

        // GET: Vehicles/Delete/5
        public ActionResult Delete_V(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vehicle vehicle = db.Vehicles.Find(id);
            if (vehicle == null)
            {
                return HttpNotFound();
            }
            return View(vehicle);
        }

        // POST: Vehicles/Delete/5
        [HttpPost, ActionName("Delete_V")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmedV(int id, Customer customer)
        {
            Vehicle vehicle = db.Vehicles.Find(id);
            db.Vehicles.Remove(vehicle);
            db.SaveChanges();
            return RedirectToAction("Index", new { id = customer.Id });
        }

        

        //Quotes

        // GET: Quotes/Details/5
        public ActionResult Details_Q(int? id)
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
        public ActionResult Create_Q()
        {
            return View();
        }

        // POST: Quotes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create_Q([Bind(Include = "Id,CurrentNumberInHousehold,DateCreated,DateReview,DiscountPercentage,TotalCost,Approved,WorkDescription")] Quote quote)
        {
            if (ModelState.IsValid)
            {
                db.Quotes.Add(quote);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(quote);
        }

        // GET: Quotes/Edit/5
        public ActionResult Edit_Q(int? id)
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
        public ActionResult Edit_Q([Bind(Include = "Id,CurrentNumberInHousehold,DateCreated,DateReview,DiscountPercentage,TotalCost,Approved,WorkDescription")] Quote quote)
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
        public ActionResult Delete_Q(int? id)
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
        [HttpPost, ActionName("Delete_Q")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmedQ(int id)
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