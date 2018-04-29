using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
//using MIS333K_Team11_FinalProjectV2.DAL;
using MIS333K_Team11_FinalProjectV2.Models;
using static MIS333K_Team11_FinalProjectV2.Models.AppUser;

namespace MIS333K_Team11_FinalProjectV2.Controllers
{
    public class ShowingsController : Controller
    {
        private AppDbContext db = new AppDbContext();

        // GET: Showings
        public ActionResult Index()
        {
            return View(db.Showings.ToList());
        }

        // GET: Showings/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Showing showing = db.Showings.Find(id);
            if (showing == null)
            {
                return HttpNotFound();
            }
            return View(showing);
        }

        // GET: Showings/Create
        public ActionResult Create()
        {
            ViewBag.AllMovies = GetAllMovies();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ShowingID, Theatre, ShowDate")] Showing showing, int? SelectedMovies)
        {
            //add movie
            if (SelectedMovies != 0)
            {
                //find the movie
                Movie mov = db.Movies.Find(SelectedMovies);

                //add in as a single value after changing the relationship in the showing.cs
                showing.SponsoringMovie = mov;
            }

            DateTime morning = new DateTime(showing.ShowDate.Year, showing.ShowDate.Month, showing.ShowDate.Day, 9, 0, 0);
            DateTime night = new DateTime(showing.ShowDate.Year, showing.ShowDate.Month, showing.ShowDate./*AddDays(1).*/Day, 0, 0, 0); //not sure if i need to add a day???
            int morning_result = DateTime.Compare(showing.ShowDate, morning);
            int night_result = DateTime.Compare(showing.ShowDate, night);

            //boolean checkers
            bool checker = true;
            bool boolean = true;

            if ((morning_result >= 0) && (night_result <= 0))
            {

                //find the showings that are on the same day and in the same theater and then compare them with each other
                //by making sure the end time of showing to be created is less than a current showing's start time
                //OR start time of showing to be created is going to be greater than a current showing's 

                List<Showing> allShowings = db.Showings.ToList();
                List<Showing> showingsDays = allShowings.Where(s => s.ShowDate.Day == showing.ShowDate.Day && s.Theatre == showing.Theatre).ToList();
                //showingsDays = showingsDays.Where(s => s.Theatre == showing.Theatre).ToList();

                if (showingsDays.Count() == 0)
                {
                    db.Showings.Add(showing);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                DateTime showing_start = showing.ShowDate;
                DateTime showing_end = showing.ShowDate.AddMinutes(showing.SponsoringMovie.RunningTime);

                while (checker == true)
                {
                    foreach (Showing sh in showingsDays)
                    {

                        DateTime sh_start = sh.ShowDate;
                        DateTime sh_end = sh.ShowDate.AddMinutes(showing.SponsoringMovie.RunningTime);

                        if (showing_start >= sh_end || showing_end <= sh_start)
                        {
                            boolean = true;
                            checker = true;
                        }

                        else
                        {
                            //leaves loop? does that work? and then will populate viewbag and not go through with creation
                            boolean = false;
                            checker = false;
                            ViewBag.ErrorMessageOverlap = "Showing you are trying to schedule overlaps with another showing's time";
                            ViewBag.AllMovies = GetAllMovies(showing);
                            return View(showing); //I think that return automatically breaks out for you?
                            //break;
                        }

                    }

                    break;
                }
                //if boolean = true: add showing to db
                //will have gone through entire while loop with success and will then be added to showing database!
                if (boolean == true)
                {

                    db.Showings.Add(showing);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }

            //will populate viewbag and not go through with order bc not between 9am-12am
            //else
            //{
            ViewBag.ErrorMessageTime = "Showing must be scheduled in between 9:00 AM and 12:00 AM";
            ViewBag.AllMovies = GetAllMovies(showing);
            return View(showing);
            //}
        }

        //// POST: Showings/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "ShowingID, Theatre, ShowDate")] Showing showing, int? SelectedMovies)
        //{

        //    //add movie
        //    if (SelectedMovies != 0)
        //    {
        //        //find the movie
        //        Movie mov = db.Movies.Find(SelectedMovies);
        //        //showing.SponsoringMovies.Add(mov);

        //        //add in as a single value after changing the relationship in the showing.cs
        //        showing.SponsoringMovie = mov;
        //    }

        //    DateTime morning = new DateTime(showing.ShowDate.Year, showing.ShowDate.Month, showing.ShowDate.Day, 9, 0, 0);
        //    DateTime night = new DateTime(showing.ShowDate.Year, showing.ShowDate.Month, showing.ShowDate.AddDays(1).Day, 0, 0, 0);
        //    int morning_result = DateTime.Compare(showing.ShowDate, morning);
        //    int night_result = DateTime.Compare(showing.ShowDate, night);

        //    if ((morning_result >= 0) && (night_result <=0))
        //    {
        //        //find the showings that are on the same day and in the same theater and then compare them with each other
        //        //by making sure the end time of showing to be created is less than a current showing's start time
        //        //OR start time of showing to be created is going to be greater than a current showing's 

        //        List<Showing> allShowings = db.Showings.ToList();
        //        List<Showing> showingsDays = allShowings.Where(s => s.ShowDate.Day == showing.ShowDate.Day && s.Theatre == showing.Theatre).ToList();

        //        DateTime showing_start = showing.ShowDate;
        //        DateTime showing_end = showing.ShowDate.AddMinutes(showing.SponsoringMovie.RunningTime);

        //        if(showingsDays.Count() == 0)
        //        {
        //            db.Showings.Add(showing);
        //            db.SaveChanges();
        //            return RedirectToAction("Index");
        //        }

        //        foreach (Showing sh in showingsDays)
        //        {
        //            DateTime sh_start = sh.ShowDate;
        //            DateTime sh_end = sh.ShowDate.AddMinutes(showing.SponsoringMovie.RunningTime);

        //            if (showing_start > sh_end || showing_end < sh_start)
        //            {
        //                //if valid, add to db
        //                if (ModelState.IsValid)
        //                {
        //                    db.Showings.Add(showing);
        //                    db.SaveChanges();
        //                    return RedirectToAction("Index");
        //                }
        //            }

        //        }

        //        ViewBag.ErrorMessageOverlap = "Showing you are trying to schedule overlaps with another showing's time";
        //        ViewBag.AllMovies = GetAllMovies(showing);
        //        return View(showing);
        //    }

        //    else
        //    {
        //        ViewBag.ErrorMessageTime = "Showing must be scheduled in between 9:00 AM and 12:00 AM";
        //        ViewBag.AllMovies = GetAllMovies(showing);
        //        return View(showing);

        //    }
        //}

        public ActionResult Publish()
        {
            return View();
        }

        //[HttpPost]
        //public ActionResult Publish(List<Showing> showings)
        //{
        //    //somehow groupby day and then theater
        //    //then loop through the amount of items in each respective list
        //    //make sure that there is no big or small gap
        //    //and then make sure last showing doesn't before 9:30 PM
        //    List<Showing> CheckShowings = db.Showings.ToList().GroupBy(); //group by day
        //    var gap = showing.ShowDate.Value.Subtract(lastShowing.ShowDate.AddMinutes(Showing.SponsoringMovie.RunningTime).Value).TotalMinutes;
        //    //                if (gap < 25 || gap > 45)
        //    //                {
        //    //                    error = "Gap shorter than 25 min or longer than 45 min";
        //    //                    return false; // gap shorter than 25 min or longer than 45 min
        //    //                

        //}


        //GET
        public ActionResult DateSearch()
        {
            return View();
        }

        [HttpPost]
        public ActionResult DateSearch([Bind(Include = "ShowingID, Theatre, ShowDate")]DateTime? datSelectedDate)
        {

            var query = from s in db.Showings
                        select s;

            if (datSelectedDate != null)
            {
                //needed truncate time method because we were comparing a showdate with a specific time compared to one without just a date
                query = query.Where(m => DbFunctions.TruncateTime(m.ShowDate) == datSelectedDate);
            }

            List<Showing> SelectedShowings = query.ToList();
            //order list
            SelectedShowings.OrderByDescending(m => m.TicketPrice);

            //ViewBag.AllShowings = db.Showings.Count();
            //ViewBag.SelectedMovies = SelectedShowings.Count();
            //send list to view
            return View("Index", SelectedShowings);
        }

        // GET: Showings/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Showing showing = db.Showings.Find(id);
            if (showing == null)
            {
                return HttpNotFound();
            }
            ViewBag.AllMovies = GetAllMovies(showing);
            return View(showing);
        }

        // POST: Showings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ShowingID, Theatre, ShowDate")] Showing showing, int? SelectedMovies)
        {
            if (ModelState.IsValid)
            {
                //find the showing to edit
                Showing showingToChange = db.Showings.Find(showing.ShowingID);

                //remove the existing movie
                showingToChange.SponsoringMovie = showing.SponsoringMovie;

                if (SelectedMovies != 0)
                {
                    //find the movie
                    Movie mov = db.Movies.Find(SelectedMovies);
                    showing.SponsoringMovie = mov;
                }

                //change scalar properties
                showingToChange.Theatre = showing.Theatre;
                showingToChange.ShowDate = showing.ShowDate;
                //showingToChange.ShowingName = showing.SponsoringMovie.MovieTitle;
                //showingToChange.RunTime = showing.RunTime;

                db.Entry(showing).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AllMovies = GetAllMovies(showing);
            return View(showing);
        }

        // GET: Showings/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Showing showing = db.Showings.Find(id);
            if (showing == null)
            {
                return HttpNotFound();
            }
            return View(showing);
        }

        // POST: Showings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Showing showing = db.Showings.Find(id);
            db.Showings.Remove(showing);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public SelectList GetAllMovies()
        {
            List<Movie> allMovs = db.Movies.OrderBy(m => m.MovieTitle).ToList();
            SelectList selMovs = new SelectList(allMovs, "MovieID", "MovieTitle");
            return selMovs;
        }

        public SelectList GetAllMovies(Showing showing)
        {
            List<Movie> allMovs = db.Movies.OrderBy(m => m.MovieTitle).ToList();

            //convert list of selected movies to ints
            List<Int32> SelectedMovies = new List<Int32>();

            Movie mov = db.Movies.Find(showing.SponsoringMovie.MovieID);

            SelectedMovies.Add(mov.MovieID);
            //loop through the showing's movie and add the movie id
            //foreach (Movie mov in showing.SponsoringMovie)
            //{
            //    SelectedMovies.Add(mov.MovieID);
            //}

            //create the multiselect list 
            SelectList selMovs = new SelectList(allMovs, "MovieID", "MovieTitle", SelectedMovies);

            //return the multiselect list
            return selMovs;
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
