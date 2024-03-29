﻿using System;
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
using AutoMed.Models.DataModels;
using AutoMed.Models.ViewModels;

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
                    AutoMedUser user = db.Users.First(u => u.UserName == model.UserName);
                    IdentityRole role = db.Roles.Find(user.Roles.First().RoleId);
                    if (role.Name == "Administrator")
                        return RedirectToAction("Create", "Report", routeValues: null);
                    else if (role.Name == "Manager")
                        return RedirectToAction("Index", "Quotes", routeValues: null);
                    else
                        return RedirectToAction("Index", "Customers");
                case SignInStatus.LockedOut:
                    return RedirectToAction(nameof(Login));
                default:
                    ModelState.AddModelError("", "Invalid login attempt.");
                    return View(model);
            }
        }
        //The list page. Lists the employees and their 
        [Authorize(Roles = "Administrator")]
        public ActionResult Index()
        {
            List<AutoMedUser> lists = db.Users.Include(x => x.Location).ToList();
            return View(lists);
        }

        /// <summary>
        /// edits the users takes on their user id as a string. uses that id to create a list of all possible locations
        /// list of all prossible roles 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            List<SelectListItem> locationList = new List<SelectListItem>();
            db.Locations.ToList().ForEach(
                location => locationList.Add(new SelectListItem { Text = location.Name, Value = location.Id.ToString()})
            );
            ViewBag.locationList = locationList;

            List<SelectListItem> rolesList = new List<SelectListItem>();
            db.Roles.ToList().ForEach(
                role => rolesList.Add(new SelectListItem { Text = role.Name, Value = role.Id.ToString() })
            );
            ViewBag.rolesList = rolesList;

            AutoMedUser user = UserManager.FindById(id);
            EditViewModel viewModel = new EditViewModel { UserName = user.UserName, LocationId = user.LocationId, Id = user.Id, Role = user.Roles.First().RoleId };   
            return View(viewModel);

        }
        /// <summary>
        /// confirms the changes it removes the current role then adds the new one added
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit(EditViewModel model)
        {
            if (ModelState.IsValid)
            {
                RoleManager<IdentityRole> roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
                AutoMedUser userToEdit = UserManager.FindById(model.Id);
                userToEdit.UserName = model.UserName;
                userToEdit.LocationId = model.LocationId;

                UserManager.RemoveFromRole(model.Id, roleManager.FindById(userToEdit.Roles.First().RoleId).Name);
                UserManager.AddToRole(model.Id, roleManager.FindById(model.Role).Name);

                UserManager.Update(userToEdit);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }
        /// <summary>
        /// takes the users id and displays who they are with their location and roles
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize(Roles = "Administrator")]
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
        [Authorize(Roles = "Administrator")]
        public ActionResult DeleteConfirmed(string id)
        {
            AutoMedUser deletion = UserManager.FindById(id);
            deletion.IsDeleted = true;
            UserManager.Update(deletion);
            return RedirectToAction(nameof(Index));
        }

        //
        // GET: /Account/Register
        [Authorize(Roles = "Administrator")]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public JsonResult DoesUserNameExist(string userName)
        {
            AutoMedUser user = db.Users.FirstOrDefault(x => x.UserName == userName);
            return Json(user == null);
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                Location location = db.Locations.Single(x => x.Name.Equals(model.Location));
                var user = new AutoMedUser { UserName = model.UserName, LocationId = location.Id, IsDeleted = false };

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
                    return RedirectToAction(nameof(Index));
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
        [Authorize(Roles = "Manager,Employee,Administrator")]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Login", "Account");
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
        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }
        #endregion
    }
}