using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMed.DAL;
using AutoMed.Models.DataModels;


namespace AutoMed.Controllers
{
    public class ScaleController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            return View(db.Scales.ToList());
        }

        public ActionResult Create()
        {
            Scale scale = new Scale() { IncomeBrackets = new List<IncomeBracket>() };
            for (int i = 1; i <= 8; i++)
            {
                scale.IncomeBrackets.Add(new IncomeBracket() { NumInHousehold = i });
            }

            return View(scale);
        }

        [HttpPost]
        public ActionResult Create(Scale scale)
        {
            db.Scales.Add(scale);
            db.SaveChanges();
            return RedirectToAction("Edit", new { id = scale.Id });
        }

        public ActionResult Edit(int id)
        {
            return View(db.Scales.Include("IncomeBrackets").Where(x => x.Id == id).First());
        }

        [HttpPost]
        public ActionResult Edit(Scale scale)
        {


            return RedirectToAction("Edit", new { year = scale.Year });
        }
    }
}