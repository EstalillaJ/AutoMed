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
    public class QuoteController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Quote
        public ActionResult Index()
        {
            int locationId = db.Users.Include("Location").Where(x => x.UserName == User.Identity.Name).First().Location.Id;
            List<Quote> quotes = db.Quotes.Where(x => x.Approval == QuoteStatus.Pending && locationId == x.Location.Id).ToList();
            return View(quotes);
        }

        // GET: Quote/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Quote/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Quote/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult UpdateCustomerStatus(List<Quote> quotes)
        {
            foreach(Quote quote in quotes)
            {
                db.Quotes.Attach(quote); // State = Unchanged
                db.Entry(quote).Property(x => x.Approval).IsModified = true;
            }
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Quote/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Quote/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Quote/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Quote/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
