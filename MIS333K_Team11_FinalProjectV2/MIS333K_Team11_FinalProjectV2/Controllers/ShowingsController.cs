﻿using System;
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

        // POST: Showings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ShowingID, Theatre, ShowDate")] Showing showing, int? SelectedMovies)
        {
            //ask for the next showing number
            //showing.ShowingNumber = Utilities.GenerateShowingNumber.GetNextShowingNumber();

           
            //add movie
            if (SelectedMovies != 0)
            {
                //find the movie
                Movie mov = db.Movies.Find(SelectedMovies);
                showing.SponsoringMovies.Add(mov);
            }

            if (showing.ShowDate >= Convert.ToDateTime("9:00 AM") && showing.ShowDate <= Convert.ToDateTime("12:00 AM"))
            {

                if (ModelState.IsValid)
                {
                    db.Showings.Add(showing);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

            }

            else
            {
                ViewBag.AllMovies = GetAllMovies(showing);
                return View(showing);

            }

            //List<Showing> showings = db.Showings.ToList();
            ////should we populate 32 ticket seats in here for every showing object we create?

            //DateTime now = DateTime.Now;
            ////Showing date = new Showing();
            ////DateTime showdate = date.ShowDate;
            //DateTime showdate = showing.ShowDate;


            //if (now < showdate)
            //{
            //    ViewBag.ErrorMessage = "Unable to add Time";
            //    return RedirectToAction("Create", "Showings");
            //}

            ////TODO: Figure out why time isn't working
            //DateTime start = Convert.ToDateTime("09:00:00 AM");
            //DateTime end = Convert.ToDateTime("12:00:00 AM");

            //Showing ShowTime = new Showing();
            //DateTime val = ShowTime.ShowDate;


            //if ((val < start) || (val > end))
            //{
            //    ViewBag.ErrorMessage = "Unable to add Time";
            //    return RedirectToAction("Create", "Showings");
            //}

            //if (ModelState.IsValid)
            //{

            //    db.Showings.Add(showing);
            //db.SaveChanges();
            //return RedirectToAction("Index");
            //}



            ////populate the viewbag with the movie list
            ViewBag.AllMovies = GetAllMovies(showing);
            return View(showing);
        }

        public ActionResult DateSearch()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DateSearch(DateTime? datSelectedDate)
        {

            var query = from s in db.Showings
                        select s;

            if (datSelectedDate !=null)
            {
                query = query.Where(m => m.ShowDate == datSelectedDate);
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

                //remove the existing movies
                showingToChange.SponsoringMovies.Clear();

                if (SelectedMovies != 0)
                {
                    //find the movie
                    Movie mov = db.Movies.Find(SelectedMovies);
                    showing.SponsoringMovies.Add(mov);
                }

                //change scalar properties
                showingToChange.Theatre = showing.Theatre;
                showingToChange.ShowDate = showing.ShowDate;
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

            //loop through the showing's movie and add the movie id
            foreach (Movie mov in showing.SponsoringMovies)
            {
                SelectedMovies.Add(mov.MovieID);
            }

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
