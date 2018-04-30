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
        // GET: Reviews
        public ActionResult Index()
        {
            var reviews = db.Reviews.Include(r => r.StarRating);
            return View(reviews.ToList());
        }

        // GET: Reviews/Details/5
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

        // GET: Reviews/Create
        public ActionResult Create(int? id, string name)
        {
            //if (name == "artistReview")
            //{
            //    ViewBag.Controller = "Artists";
            //}
            //if (name == "albumReview")
            //{
            //    ViewBag.Controller = "Albums";
            //}
            if (name == "movieReview")
            {
                ViewBag.Controller = "Movies";
            }
            ViewBag.Item = id;
            AppUser user = UserManager.FindById(User.Identity.GetUserId());
            var querychk = from r in user.Reviews
                           select r;
            //if (name == "albumReview")
            //{
            //    var queryAl = from r in querychk
            //                  select r.AlbumReview.AlbumID;
            //    List<int> revAlbums = queryAl.ToList();

            //    foreach (int i in revAlbums)
            //    {
            //        if (i == id)
            //        {
            //            TempData["ReviewError"] = "You cannot review the same album twice.";
            //            return RedirectToAction("Details", "Albums", new { id = id });
            //        }
            //    }
            //}
            //if (name == "artistReview")
            //{
            //    var queryAr = from r in querychk
            //                  select r.ArtistReview.ArtistID;
            //    List<int> revArtists = queryAr.ToList();
            //    foreach (int i in revArtists)
            //    {
            //        if (i == id)
            //        {
            //            TempData["ReviewError"] = "You cannot review the same artist twice.";
            //            return RedirectToAction("Details", "Artists", new { id = id });
            //        }
            //    }
            //}
            if (name == "MovieReview")
            {
                var querySo = from r in querychk
                              select r.MovieReview.MovieID;
                List<int> revMovies = querySo.ToList();
                foreach (int i in revMovies)
                {
                    if (i == id)
                    {
                        TempData["ReviewError"] = "You cannot review the same movie twice.";
                        return RedirectToAction("Details", "Movies", new { id = id });
                    }
                }
            }
            //if (name == "albumReview")
            //{
            //    Album AlbumToRate = db.Albums.Find(id);
            //    if (AlbumToRate == null)
            //    {
            //        return HttpNotFound();
            //    }
            //    else
            //    {
            //        var query = from o in user.Orders
            //                    from ord in db.OrderDetails
            //                    where ord.OrderID == o.OrderID
            //                    select ord;
            //        var query2 = from od in query
            //                     where AlbumToRate.AlbumID == od.AlbumID
            //                     select od.Album;
            //        System.Diagnostics.Debug.WriteLine(query.ToList());
            //        var result = query2.ToList();
            //        System.Diagnostics.Debug.WriteLine(result.Count());
            //        if (result.Count == 0)
            //        {
            //            TempData["ReviewError"] = "You must purchase before reviewing.";

            //            return RedirectToAction("Details", "Albums", new { id = id });
            //        }
            //    }
            //}
            //if (name == "artistReview")
            //{
            //    Artist ArtistToRate = db.Artists.Find(id);
            //    if (ArtistToRate == null)
            //    {
            //        return HttpNotFound();
            //    }
            //    else
            //    {
            //        var query = from o in user.Orders
            //                    from ord in db.OrderDetails
            //                    where ord.OrderID == o.OrderID
            //                    select ord;
            //        //check for artist against albums owned
            //        var query2 = from od in query
            //                     from al in db.Albums
            //                     where al.AlbumID == od.AlbumID
            //                     select al;
            //        var query4 = from al in query2
            //                     from art in al.Artists
            //                     select art;
            //        var artal = from art in query4
            //                    where art.ArtistID == ArtistToRate.ArtistID
            //                    select art;
            //        //check for artist against songs owned
            //        var query3 = from od in query
            //                     from so in db.Songs
            //                     where od.SongID == so.SongID
            //                     select so;
            //        var query5 = from s in query3
            //                     from art in s.Artists
            //                     select art;
            //        var query6 = from art in query5 //songs
            //                     where ArtistToRate.ArtistID == art.ArtistID
            //                     select art;

            //        System.Diagnostics.Debug.WriteLine(query.ToList());
            //        var result1 = artal.ToList();
            //        var result2 = query6.ToList();
            //        if (result1.Count == 0 && result2.Count == 0)
            //        {
            //            TempData["ReviewError"] = "Purchase a song or album from this artist before reviewing.";

            //            return RedirectToAction("Details", "Artists", new { id = id });
            //        }
            //    }
            //}

            if (name == "movieReview")
            {
                Movie MovieToRate = db.Movies.Find(id);
                if (MovieToRate == null)
                {
                    return HttpNotFound();
                }
                else
                {
                    var query = from o in user.Orders
                                from ord in db.Tickets
                                where ord.OrderID == o.OrderID
                                select ord;
                    var query2 = from od in query
                                 where MovieToRate.MovieID == od.MovieID
                                 select od.MovieID;
                    var result1 = query2.ToList();

                    var query3 = from o in user.Orders
                                 from ord in db.Tickets
                                 where ord.OrderID == o.OrderID
                                 select ord;
                    //var help = from al in MovieToRate.Genre
                    //           select al;
                    //var query4 = from od in query3
                    //             from al in help
                    //             where al.GenreID == od.GenreID
                    //             select od.Genre;
                    //var result2 = query4.ToList();

                    if (result1.Count == 0 /*&& result2.Count == 0*/)
                    {
                        TempData["ReviewError"] = "You must purchase before reviewing.";
                        return RedirectToAction("Details", "Movies", new { id = id });

                    }
                }
            }
            TempData["ID"] = id;
            TempData["reviewType"] = name;
            return View();
        }

        // POST: Reviews/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create([Bind(Include = "StarRating,Comment")] Review review, int? id, string name)
        {
            if (id == null || name == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //set 'no choice' to null
            if (review.StarRating == 0)
            {
                review.StarRating = 0;
            }
            //            //if (name == "artistReview")
            //            //{
            //            //    ViewBag.Controller = "Artists";
            //            //}
            //            //if (name == "albumReview")
            //            //{
            //            //    ViewBag.Controller = "Albums";
            //            //}
            if (name == "movieReview")
            {
                ViewBag.Controller = "Movies";
            }
            ViewBag.Item = id;
            //set rating
            review.rating = db.Ratings.Find((int)review.StarRating);
            ViewBag.Controller = name;
            ViewBag.Item = id;
            //if (name == "albumReview")
            //{
            //    Album AlbumToRate = db.Albums.Find(id);
            //    //make sure item exists
            //    if (AlbumToRate == null)
            //    {
            //        return HttpNotFound();
            //    }
            //    review.AlbumReview = AlbumToRate;
            //    if (ModelState.IsValid)
            //    {
            //        review.Owner = db.Users.Find(User.Identity.GetUserId());
            //        db.Reviews.Add(review);
            //        db.SaveChanges();
            //        AlbumToRate.Ratings.Add(review.rating);
            //        db.SaveChanges();
            //        return RedirectToAction("Details", "Albums", new { id = id });
            //    }
            //}

            if (name == "movieReview")
            {
                Movie MovieToRate = db.Movies.Find(id);
                //make sure item exists
                if (MovieToRate == null)
                {
                    return HttpNotFound();
                }
                review.MovieReview = MovieToRate;
                if (ModelState.IsValid)
                {
                    review.AppUser = db.Users.Find(User.Identity.GetUserId());
                    db.Reviews.Add(review);
                    db.SaveChanges();
                    MovieToRate.Ratings.Add(review.rating);
                    db.SaveChanges();
                    return RedirectToAction("Details", "Movies", new { id = id });
                }
            }

            //if (name == "artistReview")
            //{
            //    Artist ArtistToRate = db.Artists.Find(id);
            //    //make sure item exists
            //    if (ArtistToRate == null)
            //    {
            //        return HttpNotFound();
            //    }
            //    review.ArtistReview = ArtistToRate;
            //    if (ModelState.IsValid)
            //    {
            //        review.Owner = db.Users.Find(User.Identity.GetUserId());
            //        db.Reviews.Add(review);
            //        db.SaveChanges();
            //        ArtistToRate.Ratings.Add(review.rating);
            //        db.SaveChanges();
            //        return RedirectToAction("Details", "Artists", new { id = id });
            //    }
            //}
            return View(review);
        }


        // GET: Reviews/Edit/5
        [Authorize]
        public ActionResult Edit(int? id, int? ReviewID, string name)
        {
            if (id == null || ReviewID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Review review = db.Reviews.Find(ReviewID);
            if (review == null)
            {
                return HttpNotFound();
            }
            //if (name == "artistReview")
            //{
            //    ViewBag.Controller = "Artists";
            //}
            //if (name == "albumReview")
            //{
            //    ViewBag.Controller = "Albums";
            //}
            if (name == "movieReview")
            {
                ViewBag.Controller = "Movies";
            }
            ViewBag.Item = id;
            AppUser user = UserManager.FindById(User.Identity.GetUserId());
            if (!UserManager.IsInRole(user.Id, "Manager") && !UserManager.IsInRole(user.Id, "Employee"))
            {

                //redirect the user if they do not have permission to edit the review
                if (review.AppUser.Id != user.Id)
                {
                    TempData["ReviewError"] = "You do not have permission to edit that review";

                    //if (name == "albumReview")
                    //{
                    //    return RedirectToAction("Details", "Albums", new { id = id });
                    //}
                    //else if (name == "artistReview")
                    //{
                    //    return RedirectToAction("Details", "Artists", new { id = id });

                    if (name == "movieReview")
                    {
                        return RedirectToAction("Details", "Movies", new { id = id });
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
            }
            TempData["ReviewID"] = ReviewID;
            TempData["name"] = name;
            //user does have permission
            return View(review);
        }

        // POST: Reviews/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Edit([Bind(Include = "StarRating,Comment")] Review review, int? id, string name)
        {
            //if (name == "artistReview")
            //{
            //    ViewBag.Controller = "Artists";
            //}
            //if (name == "albumReview")
            //{
            //    ViewBag.Controller = "Albums";
            //}
            if (name == "movieReview")
            {
                ViewBag.Controller = "Movies";
            }

            ViewBag.Item = id;
            if (ModelState.IsValid)
            {
                review.rating = db.Ratings.Find((int)review.StarRating);
                Review revToChange = db.Reviews.Find(TempData["ReviewID"]);

                AppUser user = UserManager.FindById(User.Identity.GetUserId());
                if (UserManager.IsInRole(user.Id, "Manager") || UserManager.IsInRole(user.Id, "Employee"))
                {
                    if (revToChange.StarRating != review.StarRating)
                    {
                        TempData["ReviewError"] = "You are not permitted to edit a user's rating.";
                        //if (name == "artistReview")
                        //{
                        //    return RedirectToAction("Details", "Artists", new { id = id });
                        //}
                        //if (name == "albumReview")
                        //{
                        //    return RedirectToAction("Details", "Albums", new { id = id });
                        //}
                        if (name == "movieReview")
                        {
                            return RedirectToAction("Details", "Movies", new { id = id });
                        }
                    }
                }


                ////if it's an artist review
                //if (name == "artistReview")
                //{
                //    Artist ArtistToRate = db.Artists.Find(revToChange.ArtistReview.ArtistID);
                //    //make sure item exists
                //    if (ArtistToRate == null)
                //    {
                //        return HttpNotFound();
                //    }
                //    review.ArtistReview = ArtistToRate;
                //    review.Owner = db.Users.Find(User.Identity.GetUserId());
                //    ArtistToRate.Ratings.Remove(revToChange.rating);
                //    ArtistToRate.Ratings.Add(review.rating);
                //    db.Reviews.Add(review);
                //    db.Reviews.Remove(revToChange);
                //    db.SaveChanges();
                //    return RedirectToAction("Details", "Artists", new { id = review.ArtistReview.ArtistID });
                //}
                ////if it's an album review
                //if (name == "albumReview")
                //{
                //    Album AlbumToRate = db.Albums.Find(revToChange.AlbumReview.AlbumID);
                //    //make sure item exists
                //    if (AlbumToRate == null)
                //    {
                //        return HttpNotFound();
                //    }
                //    review.AlbumReview = AlbumToRate;
                //    review.Owner = db.Users.Find(User.Identity.GetUserId());
                //    AlbumToRate.Ratings.Remove(revToChange.rating);
                //    AlbumToRate.Ratings.Add(review.rating);
                //    db.Reviews.Add(review);
                //    db.Reviews.Remove(revToChange);
                //    db.SaveChanges();
                //    return RedirectToAction("Details", "Albums", new { id = AlbumToRate.AlbumID });
                //}
                //if it's a movie review
                if (name == "movieReview")
                {
                    Movie MovieToRate = db.Movies.Find(revToChange.MovieReview.MovieID);
                    //make sure item exists
                    if (MovieToRate == null)
                    {
                        return HttpNotFound();
                    }
                    review.MovieReview = MovieToRate;
                    review.AppUser = db.Users.Find(User.Identity.GetUserId());
                    MovieToRate.Ratings.Remove(revToChange.rating);
                    MovieToRate.Ratings.Add(review.rating);
                    db.Reviews.Add(review);
                    db.Reviews.Remove(revToChange);
                    db.SaveChanges();
                    return RedirectToAction("Details", "Movies", new { id = review.MovieReview.MovieID });
                }
            }
            return View(review);
        }

        // GET: Reviews/Delete/5
        [Authorize]
        public ActionResult Delete(int? id, int? ReviewID, string name)
        {
            if (id == null || ReviewID == null || name == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Review review = db.Reviews.Find(ReviewID);
            if (review == null)
            {
                return HttpNotFound();
            }
            //if (name == "artistReview")
            //{
            //    ViewBag.Controller = "Artists";
            //}
            //if (name == "albumReview")
            //{
            //    ViewBag.Controller = "Albums";
            //}
            if (name == "movieReview")
            {
                ViewBag.Controller = "Movies";
            }
            ViewBag.Item = id;
            AppUser user = UserManager.FindById(User.Identity.GetUserId());
            if (!UserManager.IsInRole(user.Id, "Manager") && !UserManager.IsInRole(user.Id, "Employee"))
            {
                //redirect the user if they do not have permission to edit the review
                if (review.AppUser.Id != user.Id)
                {
                    TempData["ReviewError"] = "You do not have permission to edit that review";
                    //if (name == "albumReview")
                    //{
                    //    return RedirectToAction("Details", "Albums", new { id = id });
                    //}
                    //else if (name == "artistReview")
                    //{
                    //    return RedirectToAction("Details", "Artists", new { id = id });
                    //}
                    if (name == "movieReview")
                    {
                        return RedirectToAction("Details", "Movies", new { id = id });
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }

            }
            TempData["ReviewID"] = ReviewID;
            TempData["name"] = name;
            return View(review);
        }

        // POST: Reviews/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult DeleteConfirmed(int id, int? ReviewID, string name)
        {
            if (ReviewID == null || name == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //if (name == "artistReview")
            //{
            //    ViewBag.Controller = "Artists";
            //}
            //if (name == "albumReview")
            //{
            //    ViewBag.Controller = "Albums";
            //}
            if (name == "movieReview")
            {
                ViewBag.Controller = "Movies";
            }
            ViewBag.Item = id;
            System.Diagnostics.Debug.WriteLine(id);
            Review review = db.Reviews.Find(ReviewID);
            if (review == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Rating ratingToRemove = db.Ratings.Find(review.rating.RatingID);
            //if (name == "artistReview")
            //{
            //    review.ArtistReview.Ratings.Remove(ratingToRemove);
            //    db.Reviews.Remove(review);
            //    db.SaveChanges();
            //    return RedirectToAction("Details", "Artists", new { id = id });
            //}
            //if (name == "albumReview")
            //{
            //    review.AlbumReview.Ratings.Remove(ratingToRemove);
            //    db.Reviews.Remove(review);
            //    db.SaveChanges();
            //    return RedirectToAction("Details", "Albums", new { id = id });
            //}
            if (name == "movieReview")
            {
                review.MovieReview.Ratings.Remove(ratingToRemove);
                db.Reviews.Remove(review);
                db.SaveChanges();
                return RedirectToAction("Details", "Movies", new { id = id });
            }
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