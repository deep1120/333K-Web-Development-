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
            ViewBag.SelectedShowings = db.Showings.Count();
            ViewBag.AllShowings = db.Showings.Count();
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

        //[Authorize(Roles = "Manager")]
        // GET: Showings/Create
        public ActionResult Create()
        {
            ViewBag.AllMovies = GetAllMovies();
            return View();
        }

        //[Authorize(Roles = "Manager")]
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
            DateTime night = new DateTime(showing.ShowDate.Year, showing.ShowDate.Month, showing.ShowDate.Day, 23, 59, 0); //not sure if i need to add a day???
            int morning_result = DateTime.Compare(showing.ShowDate, morning);
            int night_result = DateTime.Compare(showing.ShowDate, night);

            //boolean checkers
            bool checker = true;
            bool boolean = true;

            if ((showing.ShowDate >= morning) && (night_result <= 0))
            {

                //find the showings that are on the same day and in the same theater and then compare them with each other
                //by making sure the end time of showing to be created is less than a current showing's start time
                //OR start time of showing to be created is going to be greater than a current showing's 

                List<Showing> allShowings = db.Showings.ToList();
                List<Showing> showingsDays = allShowings.Where(s => s.ShowDate.Day == showing.ShowDate.Day && s.Theatre == showing.Theatre).ToList();

                DateTime weekday = Convert.ToDateTime("12:00");
                DateTime tuesday = Convert.ToDateTime("5:00");

                if (showingsDays.Count() == 0)
                {
                    //need to put logic in here to set showing price
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
            ViewBag.ErrorMessageTime = "Showing must be scheduled in between 9:00 AM and 11:59 PM";
            ViewBag.AllMovies = GetAllMovies(showing);
            return View(showing);
        }

        //[Authorize(Roles = "Manager")]
        //public ActionResult Publish()
        //{
        //    return View();
        //}

        //[Authorize(Roles = "Manager")]
        [HttpPost]
        public ActionResult Publish()
        {
            //somehow groupby day and then theater?? or do we need to loop through all of it??
            //then loop through the amount of items in each respective list
            //make sure that there is no big or small gap
            //and then make sure last showing doesn't before 9:30 PM

            List<Showing> CheckShowings = db.Showings.ToList(); //group by day??

            return View();
            //var gap = showing.ShowDate.Value.Subtract(lastShowing.ShowDate.AddMinutes(Showing.SponsoringMovie.RunningTime).Value).TotalMinutes;
            //                if (gap < 25 || gap > 45)
            //                {
            //                    error = "Gap shorter than 25 min or longer than 45 min";
            //                    return false; // gap shorter than 25 min or longer than 45 min
            //                }
            //                

        }


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
            SelectedShowings.OrderByDescending(m => m.SponsoringMovie.MovieTitle);

            ViewBag.AllShowings = db.Showings.Count();
            ViewBag.SelectedShowings = SelectedShowings.Count();
            //send list to view
            return View("Index", SelectedShowings);
        }

        public ActionResult DetailedSearch()
        {
            ViewBag.AllGenres = GetAllGenres();
            return View();
        }

        public ActionResult DisplaySearchResults(String SearchMovieTitle, String SearchTagline, int[] SearchGenre, String SelectedYear, MPAArating SelectedMPAARating, String SearchActor, YearRank SelectedSortOrder)
        {

            //if they selected a search string, limit results to only repos that meet the criteria
            //create query
            var query = from m in db.Showings
                        select m;

            //check to see if they selected something
            if (SearchMovieTitle != null)
            {
                query = query.Where(m => m.SponsoringMovie.MovieTitle.Contains(SearchMovieTitle));
            }

            if (SearchTagline != null)
            {
                query = query.Where(m => m.SponsoringMovie.Tagline.Contains(SearchTagline));
            }

            if (SearchActor != null)
            {
                query = query.Where(m => m.SponsoringMovie.Actor.Contains(SearchActor));
            }

            if (SearchGenre != null)
            {
                foreach (int GenreID in SearchGenre)
                {
                    //Genre GenreToFind = db.Genres.Find(GenreID);
                    query = query.Where(m => m.SponsoringMovie.Genres.Select(g => g.GenreID).Contains(GenreID));
                }
            }

            switch (SelectedMPAARating)
            {
                case MPAArating.G:
                    query = query.Where(m => m.SponsoringMovie.MPAAratings == MPAArating.G);
                    break;

                case MPAArating.PG:
                    query = query.Where(m => m.SponsoringMovie.MPAAratings == MPAArating.PG);
                    break;

                case MPAArating.PG13:
                    query = query.Where(m => m.SponsoringMovie.MPAAratings == MPAArating.PG13);
                    break;

                case MPAArating.R:
                    query = query.Where(m => m.SponsoringMovie.MPAAratings == MPAArating.R);
                    break;

                case MPAArating.Unrated:
                    query = query.Where(m => m.SponsoringMovie.MPAAratings == MPAArating.Unrated);
                    break;

                case MPAArating.All:

                    break;
            }

            if (SelectedYear != null && SelectedYear != "")
            {
                Int32 intYear;
                try
                {
                    intYear = Convert.ToInt32(SelectedYear);
                }
                catch
                {
                    ViewBag.Message = SelectedYear + "is not a valid year, please try again!";
                    ViewBag.AllGenres = GetAllGenres();
                    return View("DetailedSearch");
                }
                switch (SelectedSortOrder)
                {
                    case YearRank.GreaterThan:
                        query = query.Where(r => r.SponsoringMovie.ReleaseDate.Year >= intYear);
                        break;
                    case YearRank.LesserThan:
                        query = query.Where(r => r.SponsoringMovie.ReleaseDate.Year <= intYear);
                        break;
                }

                //query = query.Where(m => m.ReleaseDate.Year == intYear);
            }

            List<Showing> SelectedShowings = query.ToList();
            //order list
            SelectedShowings.OrderByDescending(m => m.SponsoringMovie.MovieTitle);

            ViewBag.AllShowings = db.Showings.Count();
            ViewBag.SelectedShowings = SelectedShowings.Count();
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

        public MultiSelectList GetAllGenres()
        {
            List<Genre> AllGenres = db.Genres.OrderBy(m => m.GenreName).ToList();
            MultiSelectList selGenres = new MultiSelectList(AllGenres, "GenreID", "GenreName");
            return selGenres;
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
