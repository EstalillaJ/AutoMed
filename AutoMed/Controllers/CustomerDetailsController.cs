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
        public ActionResult Index(int? id, int? customerId)
        {
            var viewModel = new Customer();
            viewModel.Vehicles = db.Vehicles.ToList();
            viewModel.Quotes = db.Quotes.ToList();
              
            return View(viewModel);
        }
    }
}