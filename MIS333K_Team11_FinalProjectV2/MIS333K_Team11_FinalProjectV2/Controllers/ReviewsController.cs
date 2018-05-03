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
            [Authorize(Roles = "Manager, Admin, Employee")]
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
            [Authorize(Roles = "Customer")]
            public ActionResult Create()
            {
                return View();
            }

            // POST: Ratings/Create
            // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
            // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
            [Authorize(Roles = "Customer")]
            [HttpPost]
            [ValidateAntiForgeryToken]
            public ActionResult Create([Bind(Include = "ReviewID,Comment,StarRating")] Review review, int? id)
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
            [Authorize(Roles = "Manager, Admin, Employee")]
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
            [Authorize(Roles = "Manager, Admin, Employee")]
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
            [Authorize(Roles = "Manager, Admin")]
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
            [Authorize(Roles = "Manager, Admin")]
            [HttpPost, ActionName("Delete")]
            [ValidateAntiForgeryToken]
            public ActionResult DeleteConfirmed(int id)
            {
                Review review = db.Reviews.Find(id);
                db.Reviews.Remove(review);
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

    //        // GET: Reviews
    //        public ActionResult Index()
    //        {
    //            List<Review> Reviews = db.Reviews.ToList();
    //            return View(Reviews);
    //        }

    //        // GET: Reviews/Details/5
    //        public ActionResult Details(int? id)
    //        {
    //            if (id == null)
    //            {
    //                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
    //            }
    //            Review review = db.Reviews.Find(id);
    //            if (review == null)
    //            {
    //                return HttpNotFound();
    //            }
    //            return View(review);
    //        }

    //        // GET: Reviews/Create
    //        public ActionResult Create(int? id, string name)
    //        {
    //            if (name == "movieReview")
    //            {
    //                ViewBag.Controller = "Movies";
    //            }
    //            ViewBag.Item = id;
    //            AppUser user = UserManager.FindById(User.Identity.GetUserId());
    //            var querychk = from r in user.Reviews
    //                           select r;

    //            if (name == "MovieReview")
    //            {
    //                var querySo = from r in querychk
    //                              select r.MovieReview.MovieID;
    //                List<int> revMovies = querySo.ToList();
    //                foreach (int i in revMovies)
    //                {
    //                    if (i == id)
    //                    {
    //                        TempData["ReviewError"] = "You cannot review the same movie twice.";
    //                        return RedirectToAction("Details", "Movies", new { id = id });
    //                    }
    //                }
    //            }

    //            if (name == "movieReview")
    //            {
    //                Movie MovieToRate = db.Movies.Find(id);
    //                if (MovieToRate == null)
    //                {
    //                    return HttpNotFound();
    //                }
    //                else
    //                {
    //                    var query = from o in user.Orders
    //                                from ord in db.Tickets
    //                                where ord.OrderID == o.OrderID
    //                                select ord;
    //                    var query2 = from od in query
    //                                 where MovieToRate.MovieID == od.MovieID
    //                                 select od.MovieID;
    //                    var result1 = query2.ToList();

    //                    var query3 = from o in user.Orders
    //                                 from ord in db.Tickets
    //                                 where ord.OrderID == o.OrderID
    //                                 select ord;
    //                    //var help = from al in MovieToRate.Genre
    //                    //           select al;
    //                    //var query4 = from od in query3
    //                    //             from al in help
    //                    //             where al.GenreID == od.GenreID
    //                    //             select od.Genre;
    //                    //var result2 = query4.ToList();

    //                    if (result1.Count == 0 /*&& result2.Count == 0*/)
    //                    {
    //                        TempData["ReviewError"] = "You must purchase before reviewing.";
    //                        return RedirectToAction("Details", "Movies", new { id = id });

    //                    }
    //                }
    //            }
    //            TempData["ID"] = id;
    //            TempData["reviewType"] = name;
    //            return View();
    //        }

    //        // POST: Reviews/Create
    //        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    //        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    //        [HttpPost]
    //        [ValidateAntiForgeryToken]
    //        [Authorize]
    //        public ActionResult Create([Bind(Include = "StarRating,Comment")] Review review, int? id, string name)
    //        {
    //            if (id == null || name == null)
    //            {
    //                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
    //            }
    //            //set 'no choice' to null
    //            if (review.StarRating == 0)
    //            {
    //                review.StarRating = 0;
    //            }
    //            if (name == "movieReview")
    //            {
    //                ViewBag.Controller = "Movies";
    //            }
    //            ViewBag.Item = id;
    //            //set rating
    //            review.r = db.Ratings.Find((int)review.StarRating);
    //            ViewBag.Controller = name;
    //            ViewBag.Item = id;

    //            if (name == "movieReview")
    //            {
    //                Movie MovieToRate = db.Movies.Find(id);
    //                //make sure item exists
    //                if (MovieToRate == null)
    //                {
    //                    return HttpNotFound();
    //                }
    //                review.MovieReview = MovieToRate;
    //                if (ModelState.IsValid)
    //                {
    //                    review.AppUser = db.Users.Find(User.Identity.GetUserId());
    //                    db.Reviews.Add(review);
    //                    db.SaveChanges();
    //                    MovieToRate.Ratings.Add(review.rating);
    //                    db.SaveChanges();
    //                    return RedirectToAction("Details", "Movies", new { id = id });
    //                }
    //            }
    //            return View(review);
    //        }


    //        // GET: Reviews/Edit/5
    //        [Authorize]
    //        public ActionResult Edit(int? id, int? ReviewID, string name)
    //        {
    //            if (id == null || ReviewID == null)
    //            {
    //                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
    //            }
    //            Review review = db.Reviews.Find(ReviewID);
    //            if (review == null)
    //            {
    //                return HttpNotFound();
    //            }
    //            if (name == "movieReview")
    //            {
    //                ViewBag.Controller = "Movies";
    //            }
    //            ViewBag.Item = id;
    //            AppUser user = UserManager.FindById(User.Identity.GetUserId());
    //            if (!UserManager.IsInRole(user.Id, "Manager") && !UserManager.IsInRole(user.Id, "Employee"))
    //            {

    //                //redirect the user if they do not have permission to edit the review
    //                if (review.AppUser.Id != user.Id)
    //                {
    //                    TempData["ReviewError"] = "You do not have permission to edit that review";

    //                    if (name == "movieReview")
    //                    {
    //                        return RedirectToAction("Details", "Movies", new { id = id });
    //                    }
    //                    else
    //                    {
    //                        return RedirectToAction("Index", "Home");
    //                    }
    //                }
    //            }
    //            TempData["ReviewID"] = ReviewID;
    //            TempData["name"] = name;
    //            //user does have permission
    //            return View(review);
    //        }

    //        // POST: Reviews/Edit/5
    //        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    //        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    //        [HttpPost]
    //        [ValidateAntiForgeryToken]
    //        [Authorize]
    //        public ActionResult Edit([Bind(Include = "StarRating,Comment")] Review review, int? id, string name)
    //        {
    //            if (name == "movieReview")
    //            {
    //                ViewBag.Controller = "Movies";
    //            }

    //            ViewBag.Item = id;
    //            if (ModelState.IsValid)
    //            {
    //                review.rating = db.Ratings.Find((int)review.StarRating);
    //                Review revToChange = db.Reviews.Find(TempData["ReviewID"]);

    //                AppUser user = UserManager.FindById(User.Identity.GetUserId());
    //                if (UserManager.IsInRole(user.Id, "Manager") || UserManager.IsInRole(user.Id, "Employee"))
    //                {
    //                    if (revToChange.StarRating != review.StarRating)
    //                    {
    //                        TempData["ReviewError"] = "You are not permitted to edit a user's rating.";

    //                        if (name == "movieReview")
    //                        {
    //                            return RedirectToAction("Details", "Movies", new { id = id });
    //                        }
    //                    }
    //                }

    //                if (name == "movieReview")
    //                {
    //                    Movie MovieToRate = db.Movies.Find(revToChange.MovieReview.MovieID);
    //                    //make sure item exists
    //                    if (MovieToRate == null)
    //                    {
    //                        return HttpNotFound();
    //                    }
    //                    review.MovieReview = MovieToRate;
    //                    review.AppUser = db.Users.Find(User.Identity.GetUserId());
    //                    MovieToRate.Ratings.Remove(revToChange.rating);
    //                    MovieToRate.Ratings.Add(review.rating);
    //                    db.Reviews.Add(review);
    //                    db.Reviews.Remove(revToChange);
    //                    db.SaveChanges();
    //                    return RedirectToAction("Details", "Movies", new { id = review.MovieReview.MovieID });
    //                }
    //            }
    //            return View(review);
    //        }

    //        // GET: Reviews/Delete/5
    //        [Authorize]
    //        public ActionResult Delete(int? id, int? ReviewID, string name)
    //        {
    //            if (id == null || ReviewID == null || name == null)
    //            {
    //                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
    //            }
    //            Review review = db.Reviews.Find(ReviewID);
    //            if (review == null)
    //            {
    //                return HttpNotFound();
    //            }

    //            if (name == "movieReview")
    //            {
    //                ViewBag.Controller = "Movies";
    //            }
    //            ViewBag.Item = id;
    //            AppUser user = UserManager.FindById(User.Identity.GetUserId());
    //            if (!UserManager.IsInRole(user.Id, "Manager") && !UserManager.IsInRole(user.Id, "Employee"))
    //            {
    //                //redirect the user if they do not have permission to edit the review
    //                if (review.AppUser.Id != user.Id)
    //                {
    //                    TempData["ReviewError"] = "You do not have permission to edit that review";

    //                    if (name == "movieReview")
    //                    {
    //                        return RedirectToAction("Details", "Movies", new { id = id });
    //                    }
    //                    else
    //                    {
    //                        return RedirectToAction("Index", "Home");
    //                    }
    //                }

    //            }
    //            TempData["ReviewID"] = ReviewID;
    //            TempData["name"] = name;
    //            return View(review);
    //        }

    //        // POST: Reviews/Delete/5
    //        [HttpPost, ActionName("Delete")]
    //        [ValidateAntiForgeryToken]
    //        [Authorize]
    //        public ActionResult DeleteConfirmed(int id, int? ReviewID, string name)
    //        {
    //            if (ReviewID == null || name == null)
    //            {
    //                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
    //            }
    //            if (name == "movieReview")
    //            {
    //                ViewBag.Controller = "Movies";
    //            }
    //            ViewBag.Item = id;
    //            System.Diagnostics.Debug.WriteLine(id);
    //            Review review = db.Reviews.Find(ReviewID);
    //            if (review == null)
    //            {
    //                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
    //            }

    //            Rating ratingToRemove = db.Ratings.Find(review.rating.RatingID);
    //            //if (name == "artistReview")
    //            //{
    //            //    review.ArtistReview.Ratings.Remove(ratingToRemove);
    //            //    db.Reviews.Remove(review);
    //            //    db.SaveChanges();
    //            //    return RedirectToAction("Details", "Artists", new { id = id });
    //            //}
    //            //if (name == "albumReview")
    //            //{
    //            //    review.AlbumReview.Ratings.Remove(ratingToRemove);
    //            //    db.Reviews.Remove(review);
    //            //    db.SaveChanges();
    //            //    return RedirectToAction("Details", "Albums", new { id = id });
    //            //}
    //            if (name == "movieReview")
    //            {
    //                review.MovieReview.Rating.Remove(ratingToRemove);
    //                db.Reviews.Remove(review);
    //                db.SaveChanges();
    //                return RedirectToAction("Details", "Movies", new { id = id });
    //            }
    //            return RedirectToAction("Index");
    //        }

    //protected override void Dispose(bool disposing)
    //    {
    //        if (disposing)
    //        {
    //            db.Dispose();
    //        }
    //        base.Dispose(disposing);
    //    }
//    }
//}