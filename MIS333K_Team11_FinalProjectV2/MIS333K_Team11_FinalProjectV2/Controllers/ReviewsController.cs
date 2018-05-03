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
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using static MIS333K_Team11_FinalProjectV2.Models.AppUser;

namespace MIS333K_Team11_FinalProjectV2.Controllers
{
    public class ReviewsController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private AppUserManager _userManager;
        AppDbContext db = new AppDbContext();
        public ReviewsController() { }
        public ReviewsController(AppUserManager userManager, ApplicationSignInManager signInManager)
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

        // GET: Ratings
        //[Authorize(Roles = "Manager, Admin, Employee")]
        public ActionResult Index()
        {
            return View(db.Reviews.ToList());
        }

        // GET: Ratings/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Review review = db.Reviews.Find(id);
            if (review == null)
            {
                return HttpNotFound();
            }
            return View(review);
        }

        // GET: Ratings/Create
        //[Authorize(Roles = "Customer")]
        public ActionResult Create()
        {
            ViewBag.MoviesToReview = GetAllMoviesToReview();
            return View();
        }
        

        // POST: Ratings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[Authorize(Roles = "Customer")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ReviewID,Comment,StarRating")] Review review)
        {
            //Album RatedAlbum;
            if (ModelState.IsValid)
            {
                db.Reviews.Add(review);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(review);
        }

        // GET: Ratings/Edit/5
        //[Authorize(Roles = "Manager, Admin, Employee")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Review review = db.Reviews.Find(id);
            if (review == null)
            {
                return HttpNotFound();
            }
            return View(review);
        }

        // POST: Ratings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[Authorize(Roles = "Manager, Admin, Employee")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ReviewID,Comment,StarRating")] Review review)
        {
            if (ModelState.IsValid)
            {
                db.Entry(review).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(review);
        }

        // GET: Ratings/Delete/5
        //[Authorize(Roles = "Manager, Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Review review = db.Reviews.Find(id);
            if (review == null)
            {
                return HttpNotFound();
            }
            return View(review);
        }

        // POST: Ratings/Delete/5
        //[Authorize(Roles = "Manager, Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Review review = db.Reviews.Find(id);
            db.Reviews.Remove(review);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        //public SelectList GetAllShowings()
        //{
        //    //Get the list of showings in order by showing name 
        //    List<Showing> allShowings = db.Showings.OrderBy(s => s.SponsoringMovie.MovieTitle).ToList();

        //    //convert the list to a select list
        //    SelectList selShowings = new SelectList(allShowings, "ShowingID", "ShowingNameAndDate");

        //    //return the select list        
        //    return selShowings;
        //}




        public SelectList GetAllMoviesToReview()
        {
            List<Movie> Movies = new List<Movie>();

            List<Ticket> ReviewTickets = db.Tickets.Where(t => t.Order.Orderstatus == OrderStatus.Completed).ToList();

            foreach (Ticket rt in ReviewTickets)
            {
                Movie moviename = rt.Showing.SponsoringMovie;
                //String movie = rt.Showing.SponsoringMovie.MovieTitle;

                if (Movies.Count() == 0)
                {
                    Movies.Add(moviename);
                }
                if (!Movies.Contains(moviename))
                {
                    Movies.Add(moviename);
                }
            }
            SelectList MoviesToReview = new SelectList(Movies, "MovieID", "MovieTitle");
             return MoviesToReview;
        }

        //private AppUserManager UserManager
        //{
        //    get
        //    {
        //        return HttpContext.GetOwinContext().GetUserManager<AppUserManager>();
        //    }
        //}

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



