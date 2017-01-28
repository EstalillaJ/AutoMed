using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Security;
using System.Web.Mvc;
using AutoMed.DAL;
using AutoMed.Models;
using Microsoft.AspNet.Identity.Owin;
using System.Threading.Tasks;
using Microsoft.Owin.Security;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;

namespace AutoMed.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private ApplicationDbContext db = new ApplicationDbContext();

        public AccountController()
        {

        }
        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, change to shouldLockout: true
            var result = await SignInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, shouldLockout: false);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    return RedirectToAction("Login");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid login attempt.");
                    return View(model);
            }
        }
        //The list page. Lists the employees and their 
        public ActionResult Index()
        {
            List<AutoMedUser> lists = db.Users.Include("Location").ToList();

            return View(lists);
        }
        /// <summary>
        /// edits the users takes on their user id as a string. uses that id to create a list of all possible locations
        /// list of all prossible roles 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            List<SelectListItem> locationList = new List<SelectListItem>();
            db.Locations.ToList().ForEach(
                v => locationList.Add(new SelectListItem { Text = v.Name, Value = v.Id.ToString()})
                );
            ViewBag.locationList = (IEnumerable<SelectListItem>)locationList;
            List<SelectListItem> rolesList = new List<SelectListItem>();
            db.Roles.ToList().ForEach(
                v => rolesList.Add(new SelectListItem { Text = v.Name, Value = v.Id.ToString() })
                );
            ViewBag.rolesList = (IEnumerable<SelectListItem>)rolesList;
            AutoMedUser user = UserManager.FindById(id);
            var viewModel = new EditViewModel { UserName = user.UserName, LocationId = user.LocationId, Id = user.Id, Role = user.Roles.First().RoleId };   
            return View(viewModel);

        }
        /// <summary>
        /// confirms the changes it removes the current role then adds the new one added
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var editId = UserManager.FindById(model.Id);
                editId.UserName = model.UserName;
                editId.LocationId = model.LocationId;
                RoleManager<IdentityRole> roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
                UserManager.RemoveFromRole(model.Id, roleManager.FindById(editId.Roles.First().RoleId).Name);
                UserManager.AddToRole(model.Id, roleManager.FindById(model.Role).Name);
                UserManager.Update(editId);
                return RedirectToAction("Index");
            }
            return View(model);
        }
        /// <summary>
        /// takes the users id and displays who they are with their location and roles
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Delete(string id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            return View(UserManager.FindById(id));
        }
        /// <summary>
        /// confirms the deletion by updating the database it uses the isDeleted function to lockout the user not actually delete from database.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            AutoMedUser deletion = UserManager.FindById(id);
            deletion.isDeleted = true;
            UserManager.Update(deletion);
            //UserManager.Delete(UserManager.FindById(id));
            return RedirectToAction("Index");
        }

        //
        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                Location location = db.Locations.Where(x => x.Name.Equals(model.Location)).FirstOrDefault();
                var user = new AutoMedUser { UserName = model.UserName, LocationId = location.Id, isDeleted = false };

                IdentityResult result = UserManager.Create(user, model.Password);
                if (model.Role == "Administrator")
                {
                    UserManager.AddToRole(user.Id, "Administrator");
                }
                else if (model.Role == "Manager")
                {
                    UserManager.AddToRole(user.Id, "Manager");
                }
                else if (model.Role == "Employee")
                {
                    UserManager.AddToRole(user.Id, "Employee");
                }
                if (result.Succeeded)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

                    // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                    // Send an email with this link
                    // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                    // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                    // await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");                    
                    return RedirectToAction("Index");
                }
                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        private void AddErrors(IdentityResult result)
        {
            throw new NotImplementedException();
        }

        //
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Index", "Home");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_userManager != null)
                {
                    _userManager.Dispose();
                    _userManager = null;
                }

                if (_signInManager != null)
                {
                    _signInManager.Dispose();
                    _signInManager = null;
                }
                db.Dispose();
            }

            base.Dispose(disposing);
        }

        #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }
        #endregion
    }
}