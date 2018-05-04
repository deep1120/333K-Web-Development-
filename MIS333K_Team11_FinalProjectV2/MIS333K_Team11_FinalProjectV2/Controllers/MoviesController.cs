using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
//using MIS333K_Team11_FinalProjectV2.DAL;
using MIS333K_Team11_FinalProjectV2.Models;
using static MIS333K_Team11_FinalProjectV2.Models.AppUser;

namespace MIS333K_Team11_FinalProjectV2.Controllers
{
    public enum YearRank { GreaterThan, LesserThan}
    public class MoviesController : Controller
    {
        private AppDbContext db = new AppDbContext();

        // GET: Home
        [Authorize]
        public ActionResult Index(String SearchString)
        {
            var query = from m in db.Movies
                        select m;
            if (SearchString != null)
            {
                query = query.Where(m => m.MovieTitle.Contains(SearchString));
            }

            List<Movie> SelectedMovies = new List<Movie>();
            SelectedMovies = query.ToList();

            ViewBag.AllMovies = db.Movies.Count();
            ViewBag.SelectedMovies = SelectedMovies.Count();

            return View(SelectedMovies.OrderByDescending(m => m.MovieTitle));
        }

        // GET: Movies/Create
        [Authorize(Roles = "Manager")]
        public ActionResult Create()
        {
            ViewBag.AllGenres = GetAllGenres();
            return View();
        }

        // POST: Movies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Manager")]
        public ActionResult Create([Bind(Include = "MovieID,MovieNumber, MovieTitle, MovieOverview, RunningTime, Tagline, MPAARating, Actor, MovieRevenue, ReleaseDate, AverageUserRating, FeaturedMovie")] Movie movie, int[] SelectedGenres)
        {

            foreach (int i in SelectedGenres)
            {
                Genre g = db.Genres.Find(i);
                movie.Genres.Add(g);
            }


            if (ModelState.IsValid)
            {
                db.Movies.Add(movie);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AllGenres = GetAllGenres(movie);
            return View(movie);
        }

        public MultiSelectList GetAllGenres(Movie movie)
        {
            List<Genre> allgenres = db.Genres.OrderBy(d => d.GenreName).ToList();
            List<Int32> SelectedGenres = new List<Int32>();

            foreach (Genre genre in movie.Genres)
            {
                SelectedGenres.Add(genre.GenreID);
            }

            MultiSelectList selgenres = new MultiSelectList(allgenres, "GenreID", "GenreName", SelectedGenres);

            return selgenres;
        }

        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movie movie = db.Movies.Find(id);
            if (movie == null)
            {
                return HttpNotFound();
            }
            return View(movie);
        }

        [Authorize]
        public ActionResult DetailedSearch()
        {
            ViewBag.AllGenres = GetAllGenres();
            return View();
        }

        [Authorize]
        public ActionResult DisplaySearchResults(String SearchMovieTitle, String SearchTagline, int[] SearchGenre, String SelectedYear, MPAArating SelectedMPAARating, String SearchActor, YearRank SelectedSortOrder)
        {

            //if they selected a search string, limit results to only repos that meet the criteria
            //create query
            var query = from m in db.Movies
                        select m;

            //check to see if they selected something
            if (SearchMovieTitle != null)
            {
                query = query.Where(m => m.MovieTitle.Contains(SearchMovieTitle));
            }

            if (SearchTagline != null)
            {
                query = query.Where(m => m.Tagline.Contains(SearchTagline));
            }

            if (SearchActor != null)
            {
                query = query.Where(m => m.Actor.Contains(SearchActor));
            }

            if (SearchGenre != null)
            {
                foreach (int GenreID in SearchGenre)
                {
                    //Genre GenreToFind = db.Genres.Find(GenreID);
                    query = query.Where(m => m.Genres.Select(g => g.GenreID).Contains(GenreID));
                }
            }

            switch (SelectedMPAARating)
            {
                case MPAArating.G:
                    query = query.Where(m => m.MPAAratings == MPAArating.G);
                    break;

                case MPAArating.PG:
                    query = query.Where(m => m.MPAAratings == MPAArating.PG);
                    break;

                case MPAArating.PG13:
                    query = query.Where(m => m.MPAAratings == MPAArating.PG13);
                    break;

                case MPAArating.R:
                    query = query.Where(m => m.MPAAratings == MPAArating.R);
                    break;

                case MPAArating.Unrated:
                    query = query.Where(m => m.MPAAratings == MPAArating.Unrated);
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
                switch(SelectedSortOrder)
                {
                    case YearRank.GreaterThan:
                        query = query.Where(r => r.ReleaseDate.Year >= intYear);
                        break;
                    case YearRank.LesserThan:
                        query = query.Where(r => r.ReleaseDate.Year <= intYear);
                        break;
                }

                //query = query.Where(m => m.ReleaseDate.Year == intYear);
            }

            List<Movie> SelectedMovies = query.ToList();
            //order list
            SelectedMovies.OrderByDescending(m => m.MovieTitle);

            ViewBag.AllMovies = db.Movies.Count();
            ViewBag.SelectedMovies = SelectedMovies.Count();
            //send list to view
            return View("Index", SelectedMovies);
        }
        public MultiSelectList GetAllGenres()
        {
            List<Genre> AllGenres = db.Genres.OrderBy(m => m.GenreName).ToList();
            MultiSelectList selGenres = new MultiSelectList(AllGenres, "GenreID", "GenreName");
            return selGenres;
        }
    }
}
