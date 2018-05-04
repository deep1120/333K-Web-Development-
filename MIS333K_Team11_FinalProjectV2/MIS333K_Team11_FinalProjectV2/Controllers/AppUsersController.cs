using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MIS333K_Team11_FinalProjectV2.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Globalization;
using static MIS333K_Team11_FinalProjectV2.Models.AppUser;
//using MIS333K_Team11_FinalProjectV2.DAL;

namespace MIS333K_Team11_FinalProjectV2.Controllers
{
    public class AppUsersController : Controller
    {
        private AppDbContext db = new AppDbContext();

        // GET: AppUsers
        [Authorize]
        public ActionResult Index()
        {
            return View(db.Users.ToList());
        }

        //for managers and employees
        [Authorize(Roles = "Manager,Employee")]
        public ActionResult ManagerIndex()
        {
            return View(db.Users.ToList());
        }

        // GET: AppUsers/Details/5
        [Authorize(Roles = "Manager, Employee, Customer")]
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AppUser appUser = db.Users.Find(id);
            if (appUser == null)
            {
                return HttpNotFound();
            }
            return View(appUser);
        }

        // GET: AppUsers/Create
        [AllowAnonymous]
        public ActionResult Create()
        {
            return View();
        }

        // POST: AppUsers/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,FirstName,MiddleInitial,LastName,Street,ZipCode," +
            "City,State,Email,EmailConfirmed,PasswordHash,SecurityStamp," +
            "PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEndDataUtc,LockoutEnabled," +
            "AccessFailedCount,UserName")] AppUser appUser)
        {
            if (ModelState.IsValid)
            {
                db.Users.Add(appUser);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(appUser);
        }

        // GET: AppUsers/Edit/5
        [Authorize(Roles = "Manager, Customer, Employee")]
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            AppUser appUser = db.Users.Find(id);
            if (appUser == null)
            {
                return HttpNotFound();
            }
            return View(appUser);
        }

        //GET: Employee/Manager Edit
        [Authorize(Roles = "Manager")]
        public ActionResult ManagerEdit(string id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AppUser appUser = db.Users.Find(id);
            if (appUser == null)
            {
                return HttpNotFound();
            }
            return View(appUser);
        }

        // POST: AppUsers/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Edit([Bind(Include = "Id,FirstName,MiddleInitial,LastName,Street,ZipCode," +
            "City,State,Email,PhoneNumber,Birthday,PopcornPoints")] AppUser appUser)
        {
            if (ModelState.IsValid)
            {
                db.Entry(appUser).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(appUser);
        }

        //POST: Manager edit
        [Authorize(Roles = "Manager")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ManagerEdit([Bind(Include = "Id,FirstName,MiddleInitial,LastName,Street,ZipCode," +
            "City,State,Email,PhoneNumber,Birthday,PopcornPoints")] AppUser appUser)
        {
            if(ModelState.IsValid)
            {
                AppUser memberToChange = db.Users.Find(appUser.Id);
                if (User.IsInRole("Manager"))
                {
                    memberToChange.FirstName = appUser.FirstName;
                    memberToChange.MiddleInitial = appUser.MiddleInitial;
                    memberToChange.LastName = appUser.MiddleInitial;
                    memberToChange.Birthday = appUser.Birthday;
                    memberToChange.PopcornPoints = appUser.PopcornPoints;
                    memberToChange.Street = appUser.Street;
                    memberToChange.City = appUser.City;
                    memberToChange.State = appUser.State;
                    memberToChange.ZipCode = appUser.ZipCode;
                    memberToChange.PhoneNumber = appUser.PhoneNumber;

                    db.Entry(memberToChange).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    memberToChange.Street = appUser.Street;
                    memberToChange.City = appUser.City;
                    memberToChange.State = appUser.State;
                    memberToChange.ZipCode = appUser.ZipCode;
                    memberToChange.Birthday = appUser.Birthday;
                    memberToChange.PhoneNumber = appUser.PhoneNumber;

                    db.Entry(memberToChange).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }      
            }
            return View(appUser);
        }

        // GET: Users/Delete/5
        [Authorize(Roles = "Manager")]
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AppUser appUser = db.Users.Find(id);
            if (appUser == null)
            {
                return HttpNotFound();
            }
            return View(appUser);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Manager")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            AppUser appUser = db.Users.Find(id);
            db.Users.Remove(appUser);
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
