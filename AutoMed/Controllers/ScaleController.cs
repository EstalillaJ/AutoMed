using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMed.DAL;
using System.Data.Entity;
using AutoMed.Models.DataModels;


namespace AutoMed.Controllers
{   /// <summary>
    ///     This class handles all POST and GET requests related to the create, read, and edit of Poverty Scales.
    ///     Access to all methods in this class are limited to users with administrator privileges.
    /// </summary>
    [Authorize(Roles = "Administrator")]
    public class ScaleController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        /// <summary>
        ///  This method returns an HTML page 
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View(db.Scales.ToList());
        }

        /// <summary>
        /// This is a HTTP GET Method that returns a webpage where users can
        /// enter a Poverty Scale for a new year. Only Accessible by Administrators.
        /// </summary>
        /// <returns>HTML page for creating Poverty Scale</returns>
        public ActionResult Create()
        {
            Scale scale = new Scale { IncomeBrackets = new List<IncomeBracket>() };
            for (int i = 1; i <= 8; i++)
            {
                scale.IncomeBrackets.Add(new IncomeBracket { NumInHousehold = i });
            }

            return View(scale);
        }

        /// <summary>
        /// This is an HTTP post method that saves a scale to the database. Only Accessible by Administrators.
        /// </summary>
        /// <param name="scale">A scale object, sent url-encoded in the body of the POST request.</param>
        /// <returns>An HTML page to view the newly created scale</returns>
        [HttpPost]
        public ActionResult Create(Scale scale)
        {
            db.Scales.Add(scale);
            db.SaveChanges();
            return RedirectToAction(nameof(Edit), new { id = scale.Id });
        }

        /// <summary>
        /// This method is a GET request for editing an existing scale. Only Accessible by Administrators.
        /// </summary>
        /// <param name="id">The id of the scale to edit.</param>
        /// <returns>An HTML page to edit the existing scale.</returns>
        public ActionResult Edit(int id)
        {
            Scale scale = db.Scales.Include(x => x.IncomeBrackets).FirstOrDefault(x => x.Id == id);
            if (scale != null)
            {
                return View(scale);
            }
            return HttpNotFound();
        }


        /// <summary>
        /// This method is a POST request which takes a scale that can be used
        /// to update a scale that already exists. Only Accessible by Administrators.
        /// </summary>
        /// <param name="scale">A scale object, sent url-encoded in the body of the POST request.</param>
        /// <returns>An HTML page to further edit the scale passed to this method.</returns>
        [HttpPost]
        public ActionResult Edit(Scale scale)
        {
            return RedirectToAction(nameof(Edit), new { year = scale.Year });
        }
    }
}