using System;
using MIS333K_Team11_FinalProjectV2.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Linq;
using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Web.Security;
using System.Net;
using static MIS333K_Team11_FinalProjectV2.Models.AppUser;
//using MIS333K_Team11_FinalProjectV2.DAL;

namespace MIS333K_Team11_FinalProjectV2.Controllers
{
    public class RolesController : Controller
    {
        AppDbContext db = new AppDbContext();
        private ApplicationSignInManager _signInManager;
        private AppUserManager _userManager;

        public RolesController()
        {
        }

        public RolesController(AppUserManager userManager, ApplicationSignInManager signInManager)
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

        public AppUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<AppUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        private AppRoleManager RoleManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<AppRoleManager>();
            }
        }

        // GET: /Roles/
        [Authorize(Roles = "Manager")]
        public ActionResult Index()
        {
            var roles = db.Roles.ToList();
            return View(roles);
        }

        //
        // GET: /Roles/Create
        [Authorize(Roles = "Manager")]
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Roles/Create
        [HttpPost]
        [Authorize(Roles = "Manager")]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                db.Roles.Add(new Microsoft.AspNet.Identity.EntityFramework.IdentityRole()
                {
                    Name = collection["RoleName"]
                });
                IdentityResult result = RoleManager.Create(new AppRole(collection["RoleName"]));
                db.SaveChanges();
                ViewBag.ResultMessage = "Role created successfully !";
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Roles/Edit/5
        [Authorize(Roles = "Manager")]
        public ActionResult Edit(string roleName)
        {
            var thisRole = db.Roles.Where(r => r.Name.Equals(roleName, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();

            return View(thisRole);
        }

        //
        // POST: /Roles/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Manager")]
        public ActionResult Edit(Microsoft.AspNet.Identity.EntityFramework.IdentityRole role)
        {
            try
            {
                db.Entry(role).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Roles/Delete/5
        [Authorize(Roles = "Manager")]
        public ActionResult Delete(string RoleName)
        {
            var thisRole = db.Roles.Where(r => r.Name.Equals(RoleName, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
            db.Roles.Remove(thisRole);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Manager, Employee")]
        public ActionResult ManageUserRoles()
        {
            //for managers
            var list = db.Roles.OrderBy(r => r.Name).ToList().Select(rr => new SelectListItem { Value = rr.Name.ToString(), Text = rr.Name }).ToList();
            ViewBag.Roles = list;

            //for employees
            var customerList = db.Roles.Where(x => x.Name == "Customer" || x.Name == "DisabledCustomer").ToList().Select(rr => new SelectListItem { Value = rr.Name.ToString(), Text = rr.Name }).ToList();
            ViewBag.CustomerRoles = customerList;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Manager")]
        public ActionResult RoleAddToUser(string UserName, string RoleName)
        {
            if (String.IsNullOrEmpty(RoleName) || (String.IsNullOrEmpty(UserName)))
            {
                return RedirectToAction("ManageUserRoles");
            }
            else
            {
                AppUser user = db.Users.Where(u => u.UserName.Equals(UserName, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
                if (user == null)
                {
                    return RedirectToAction("ManageUserRoles");
                }
                else
                {
                    UserManager.AddToRole(user.Id, RoleName);
                    ViewBag.AddResultMessage = "Role added to user successfully!";
                }
            }

            // prepopulate roles for the view dropdown
            var list = db.Roles.OrderBy(r => r.Name).ToList().Select(rr => new SelectListItem { Value = rr.Name.ToString(), Text = rr.Name }).ToList();
            ViewBag.Roles = list;
            //for employees
            var customerList = db.Roles.Where(x => x.Name == "Customer" || x.Name == "DisabledCustomer").ToList().Select(rr => new SelectListItem { Value = rr.Name.ToString(), Text = rr.Name }).ToList();
            ViewBag.CustomerRoles = customerList;

            ModelState.Clear();
            return View("ManageUserRoles");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Manager, Employee")]
        public ActionResult GetRoles(string UserName)
        {
            if (!string.IsNullOrWhiteSpace(UserName))
            {
                AppUser user = db.Users.Where(u => u.UserName.Equals(UserName, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
                if (user == null)
                {
                    return RedirectToAction("ManageUserRoles");
                }
                else
                {
                    ViewBag.RolesForThisUser = UserManager.GetRoles(user.Id);
                }

                // prepopulate roles for the view dropdown
                var list = db.Roles.OrderBy(r => r.Name).ToList().Select(rr => new SelectListItem { Value = rr.Name.ToString(), Text = rr.Name }).ToList();
                ViewBag.Roles = list;
                //for employees
                var customerList = db.Roles.Where(x => x.Name == "Customer" || x.Name == "DisabledCustomer").ToList().Select(rr => new SelectListItem { Value = rr.Name.ToString(), Text = rr.Name }).ToList();
                ViewBag.CustomerRoles = customerList;
            }
            else
            {
                return RedirectToAction("ManageUserRoles");
            }

            ModelState.Clear();
            return View("ManageUserRoles");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Manager")]
        public ActionResult DeleteRoleForUser(string UserName, string RoleName)
        {
            if (String.IsNullOrEmpty(RoleName) || (String.IsNullOrEmpty(UserName)))
            {
                return RedirectToAction("ManageUserRoles");
            }
            else
            {
                AppUser user = db.Users.Where(u => u.UserName.Equals(UserName, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();

                if (user == null)
                {
                    return RedirectToAction("ManageUserRoles");
                }
                else if (UserManager.IsInRole(user.Id, RoleName))
                {
                    UserManager.RemoveFromRole(user.Id, RoleName);
                    ViewBag.DeleteResultMessage = "Role removed from this user successfully!";
                }
                else
                {
                    ViewBag.DeleteResultMessage = "This user doesn't belong to selected role.";
                }
            }

            // prepopulate roles for the view dropdown
            var list = db.Roles.OrderBy(r => r.Name).ToList().Select(rr => new SelectListItem { Value = rr.Name.ToString(), Text = rr.Name }).ToList();
            ViewBag.Roles = list;
            //for employees
            var customerList = db.Roles.Where(x => x.Name == "Customer" || x.Name == "DisabledCustomer").ToList().Select(rr => new SelectListItem { Value = rr.Name.ToString(), Text = rr.Name }).ToList();
            ViewBag.CustomerRoles = customerList;

            ModelState.Clear();
            return View("ManageUserRoles");
        }
    }
}