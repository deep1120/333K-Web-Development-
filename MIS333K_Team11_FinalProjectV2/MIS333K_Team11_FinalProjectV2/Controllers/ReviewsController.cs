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
        [Authorize]
        public ActionResult Index()
        {   
            string UserID = User.Identity.GetUserId();
            List<Review> Reviews = db.Reviews.Where(r => r.AppUser.Id == UserID).ToList();
            return View(Reviews);
        }

        // GET: Ratings/Details/5
        [Authorize]
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
        [Authorize(Roles = "Customer")]
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
        //[Authorize(Roles = "Customer")]
        public ActionResult Create([Bind(Include = "ReviewID,StarRating,Comment")] Review review)
        {
            review.AppUser = db.Users.Find(User.Identity.GetUserId());
            db.SaveChanges();
    
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
        [Authorize]
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
        [Authorize]
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

        public SelectList GetAllMoviesToReview()
        {
            List<Movie> Movies = new List<Movie>();

            string currentUserId = User.Identity.GetUserId();
            List<Ticket> ReviewTickets = db.Tickets.Where(t => t.Order.Orderstatus == OrderStatus.Completed).ToList();
            List<Order> Orders = db.Orders.Where(o => o.OrderAppUser.Id == currentUserId).ToList();
            

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



