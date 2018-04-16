using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MIS333K_Team11_FinalProjectV2.Models;
using MIS333K_Team11_FinalProjectV2.DAL;

namespace MIS333K_Team11_FinalProjectV2.Controllers
{
    public class HomeController : Controller
    {
        private AppDbContext db = new AppDbContext();

        // GET: Home
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

        public ActionResult DetailedSearch()
        {

            ViewBag.AllGenres = GetAllGenres();

            return View();

        }

        public ActionResult DisplaySearchResults(String SearchMovieTitle, String SearchTagline, int[] SearchGenre, String SelectedYear, MPAArating SelectedMPAARating, String SearchActor)
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
                query = query.Where(m => m.ReleaseDate.Year == intYear);
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