using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using AutoMed.Models;
using AutoMed.DAL;
using System.Text;

namespace AutoMed.Controllers
{   
    //[Authorize(Roles="Administrator")]
    public class ReportController : Controller
    {
        private ApplicationDbContext Context = ApplicationDbContext.Create();
        public ActionResult Create()
        {
            CreateReportViewModel viewModel = new CreateReportViewModel();
            Context.Locations.ToList().ForEach(
                x => viewModel.Locations.Add(
                    new Checkbox<Location>()
                    {
                        Item = x
                    }));
            return View(viewModel);
        }

        [HttpPost]
        public FileResult Create(CreateReportViewModel model)
        {
            string file = "file, another file\n name, another name";

            return File(Encoding.ASCII.GetBytes(file), "text/plain", string.Format("Report_{0}.csv", DateTime.Now));
        }
    }
}