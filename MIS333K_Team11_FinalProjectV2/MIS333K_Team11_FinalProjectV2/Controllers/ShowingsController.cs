using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using MIS333K_Team11_FinalProjectV2.Models;
using static MIS333K_Team11_FinalProjectV2.Models.AppUser;

namespace MIS333K_Team11_FinalProjectV2.Controllers
{
    public enum Option { GreaterThan, LessThan }
    public class ShowingsController : Controller
    {
        private AppDbContext db = new AppDbContext();
        private ApplicationSignInManager _signInManager;
        private AppUserManager _userManager;

        public ShowingsController()
        {
        }

        public ShowingsController(AppUserManager userManager, ApplicationSignInManager signInManager)
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

        //[Authorize(Roles = "Manager")]
        // GET: Showings
        public ActionResult Index()
        {
            if (User.IsInRole("Manager") || User.IsInRole("Employee"))
            {
                ViewBag.SelectedShowings = db.Showings.Count();
                ViewBag.AllShowings = db.Showings.Count();
                return View(db.Showings.ToList());
            }
            else
            {
                ViewBag.SelectedShowings = db.Showings.Where(s => s.IsPublished == PublishedStatus.IsPublished).Count();
                ViewBag.AllShowings = db.Showings.Where(s => s.IsPublished == PublishedStatus.IsPublished).Count();
                return View(db.Showings.Where(s => s.IsPublished == PublishedStatus.IsPublished).ToList());
            }
            //need if statement to return two different views depending on if user is a manager, employee, or customer
            //ViewBag.SelectedShowings = db.Showings.Count();
            //ViewBag.AllShowings = db.Showings.Count();
            //return View(db.Showings.ToList());
        }

        //[Authorize]
        //[ActionName("IndexPublished")]
        //public ActionResult Index(List<Showing> CheckShowings)
        //{
        //    ViewBag.SelectedShowings = CheckShowings.Count();
        //    ViewBag.AllShowings = CheckShowings.Count();
        //    return View(CheckShowings);
        //}

        //[AllowAnonymous]
        public ActionResult Published(List<Showing> CheckShowings)
        {
            //ViewBag.SelectedShowings = CheckShowings.Count();
            //ViewBag.AllShowings = CheckShowings.Count();
            return View(CheckShowings);
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

        [Authorize(Roles = "Manager")]
        // GET: Showings/Create
        public ActionResult Create()
        {
            ViewBag.AllMovies = GetAllMovies();
            return View();
        }

        [Authorize(Roles = "Manager")]
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

            //date checkers for showing ticket pricing
            DateTime weekday = Convert.ToDateTime("12:00");
            DateTime tuesday = Convert.ToDateTime("17:00");

            if ((showing.ShowDate >= morning) && (night_result <= 0))
            {
                if ((showing.ShowDate.DayOfWeek == DayOfWeek.Monday) && (showing.ShowDate < weekday))
                {
                    showing.TicketPrice = 5.00m;
                }
                if ((showing.ShowDate.DayOfWeek == DayOfWeek.Tuesday) && (showing.ShowDate < weekday))
                {
                    showing.TicketPrice = 5.00m;
                }
                if ((showing.ShowDate.DayOfWeek == DayOfWeek.Wednesday) && (showing.ShowDate < weekday))
                {
                    showing.TicketPrice = 5.00m;
                }
                if ((showing.ShowDate.DayOfWeek == DayOfWeek.Thursday) && (showing.ShowDate < weekday))
                {
                    showing.TicketPrice = 5.00m;
                }
                if ((showing.ShowDate.DayOfWeek == DayOfWeek.Friday) && (showing.ShowDate < weekday))
                {
                    showing.TicketPrice = 5.00m;
                }
                if ((showing.ShowDate.DayOfWeek == DayOfWeek.Tuesday) && (showing.ShowDate <= tuesday))
                {
                    showing.TicketPrice = 8.00m;
                }
                if ((showing.ShowDate.DayOfWeek == DayOfWeek.Monday) && (showing.ShowDate >= weekday))
                {
                    showing.TicketPrice = 10.00m;
                }
                if ((showing.ShowDate.DayOfWeek == DayOfWeek.Tuesday) && (showing.ShowDate >= weekday))
                {
                    showing.TicketPrice = 10.00m;
                }
                if ((showing.ShowDate.DayOfWeek == DayOfWeek.Wednesday) && (showing.ShowDate >= weekday))
                {
                    showing.TicketPrice = 10.00m;
                }
                if ((showing.ShowDate.DayOfWeek == DayOfWeek.Thursday) && (showing.ShowDate >= weekday))
                {
                    showing.TicketPrice = 10.00m;
                }
                if ((showing.ShowDate.DayOfWeek == DayOfWeek.Friday) && (showing.ShowDate >= weekday))
                {
                    showing.TicketPrice = 12.00m;
                }
                if (showing.ShowDate.DayOfWeek == DayOfWeek.Saturday)
                {
                    showing.TicketPrice = 12.00m;
                }
                if (showing.ShowDate.DayOfWeek == DayOfWeek.Sunday)
                {
                    showing.TicketPrice = 12.00m;
                }

                //find the showings that are on the same day and in the same theater and then compare them with each other
                //by making sure the end time of showing to be created is less than a current showing's start time
                //OR start time of showing to be created is going to be greater than a current showing's 

                List<Showing> allShowings = db.Showings.ToList();
                List<Showing> showingsDays = allShowings.Where(s => s.ShowDate.Day == showing.ShowDate.Day && s.Theatre == showing.Theatre).ToList();

                if (showingsDays.Count() == 0)
                {
                    showing.IsPublished = PublishedStatus.NotPublished;
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
                        DateTime sh_end = sh.ShowDate.AddMinutes(sh.SponsoringMovie.RunningTime);

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
                    showing.IsPublished = PublishedStatus.NotPublished;
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
        //[HttpGet]
        //[Authorize(Roles = "Manager")]
        //[HttpGet]
        public ActionResult Publish()
        {
            //somehow groupby day and then theater?? or do we need to loop through all of it??
            //then loop through the amount of items in each respective list
            //make sure that there is no big or small gap
            //and then make sure last showing doesn't before 9:30 PM       

            List<Showing> CheckShowings = db.Showings.Where(s => s.IsPublished == PublishedStatus.NotPublished).ToList();

            //List<Showing> Monday1 = CheckShowings.Where(s => s.ShowDate.DayOfWeek == DayOfWeek.Monday && s.Theatre == Theatre.Theatre1).OrderBy(m => m.ShowDate).ToList();
            //List<Showing> Monday2 = CheckShowings.Where(s => s.ShowDate.DayOfWeek == DayOfWeek.Monday && s.Theatre == Theatre.Theatre2).OrderBy(m => m.ShowDate).ToList();
            //List<Showing> Tuesday1 = CheckShowings.Where(s => s.ShowDate.DayOfWeek == DayOfWeek.Tuesday && s.Theatre == Theatre.Theatre1).OrderBy(m => m.ShowDate).ToList();
            //List<Showing> Tuesday2 = CheckShowings.Where(s => s.ShowDate.DayOfWeek == DayOfWeek.Tuesday && s.Theatre == Theatre.Theatre2).OrderBy(m => m.ShowDate).ToList();
            //List<Showing> Wednesday1 = CheckShowings.Where(s => s.ShowDate.DayOfWeek == DayOfWeek.Wednesday && s.Theatre == Theatre.Theatre1).OrderBy(m => m.ShowDate).ToList();
            //List<Showing> Wednesday2 = CheckShowings.Where(s => s.ShowDate.DayOfWeek == DayOfWeek.Wednesday && s.Theatre == Theatre.Theatre2).OrderBy(m => m.ShowDate).ToList();
            //List<Showing> Thursday1 = CheckShowings.Where(s => s.ShowDate.DayOfWeek == DayOfWeek.Thursday && s.Theatre == Theatre.Theatre1).OrderBy(m => m.ShowDate).ToList();
            //List<Showing> Thursday2 = CheckShowings.Where(s => s.ShowDate.DayOfWeek == DayOfWeek.Thursday && s.Theatre == Theatre.Theatre2).OrderBy(m => m.ShowDate).ToList();
            List<Showing> Friday1 = CheckShowings.Where(s => s.ShowDate.DayOfWeek == DayOfWeek.Friday && s.Theatre == Theatre.Theatre1).OrderBy(m => m.ShowDate).ToList();
            //List<Showing> Friday2 = CheckShowings.Where(s => s.ShowDate.DayOfWeek == DayOfWeek.Friday && s.Theatre == Theatre.Theatre2).OrderBy(m => m.ShowDate).ToList();
            //List<Showing> Saturday1 = CheckShowings.Where(s => s.ShowDate.DayOfWeek == DayOfWeek.Saturday && s.Theatre == Theatre.Theatre1).OrderBy(m => m.ShowDate).ToList();
            //List<Showing> Saturday2 = CheckShowings.Where(s => s.ShowDate.DayOfWeek == DayOfWeek.Saturday && s.Theatre == Theatre.Theatre2).OrderBy(m => m.ShowDate).ToList();
            //List<Showing> Sunday1 = CheckShowings.Where(s => s.ShowDate.DayOfWeek == DayOfWeek.Sunday && s.Theatre == Theatre.Theatre1).OrderBy(m => m.ShowDate).ToList();
            //List<Showing> Sunday2 = CheckShowings.Where(s => s.ShowDate.DayOfWeek == DayOfWeek.Sunday && s.Theatre == Theatre.Theatre2).OrderBy(m => m.ShowDate).ToList();

            //var Monday_1 = Monday1.ToArray(); if (Monday_1.Length == 0) { return RedirectToAction("Index"); }
            //var Monday_2 = Monday2.ToArray(); if (Monday_2.Length == 0) { return RedirectToAction("Index"); }
            //var Tuesday_1 = Tuesday1.ToArray(); if (Tuesday_1.Length == 0) { return RedirectToAction("Index"); }
            //var Tuesday_2 = Tuesday2.ToArray(); if (Tuesday_2.Length == 0) { return RedirectToAction("Index"); }
            //var Wednesday_1 = Wednesday1.ToArray(); if (Wednesday_1.Length == 0) { return RedirectToAction("Index"); }
            //var Wednesday_2 = Wednesday2.ToArray(); if (Wednesday_2.Length == 0) { return RedirectToAction("Index"); }
            //var Thursday_1 = Thursday1.ToArray(); if (Thursday_1.Length == 0) { return RedirectToAction("Index"); }
            //var Thursday_2 = Thursday2.ToArray(); if (Thursday_2.Length == 0) { return RedirectToAction("Index"); }
            var Friday_1 = Friday1.ToArray(); if (Friday_1.Length == 0) { return RedirectToAction("Index"); }
            //var Friday_2 = Friday2.ToArray(); if (Friday_2.Length == 0) { return RedirectToAction("Index"); }
            //var Saturday_1 = Saturday1.ToArray(); if (Saturday_1.Length == 0) { return RedirectToAction("Index"); }
            //var Saturday_2 = Saturday2.ToArray(); if (Saturday_2.Length == 0) { return RedirectToAction("Index"); }
            //var Sunday_1 = Sunday1.ToArray(); if (Sunday_1.Length == 0) { return RedirectToAction("Index"); }
            //var Sunday_2 = Sunday2.ToArray(); if (Sunday_2.Length == 0) { return RedirectToAction("Index"); }

            Int32 errorcount = 0;
            Int32 earlyerror = 0;

            //DateTime Monday_Last1 = Monday_1[Monday_1.Length - 1].ShowDate;
            //DateTime Monday_Last_1 = new DateTime(Monday_Last1.Year, Monday_Last1.Month, Monday_Last1.Day, 21, 30, 0);

            //DateTime Monday_Last2 = Monday_2[Monday_2.Length - 1].ShowDate;
            //DateTime Monday_Last_2 = new DateTime(Monday_Last2.Year, Monday_Last1.Month, Monday_Last2.Day, 21, 30, 0);

            //DateTime Tuesday_Last1 = Tuesday_1[Tuesday_1.Length - 1].ShowDate;
            //DateTime Tuesday_Last_1 = new DateTime(Tuesday_Last1.Year, Tuesday_Last1.Month, Tuesday_Last1.Day, 21, 30, 0);

            //DateTime Tuesday_Last2 = Tuesday_2[Tuesday_2.Length - 1].ShowDate;
            //DateTime Tuesday_Last_2 = new DateTime(Tuesday_Last2.Year, Tuesday_Last2.Month, Tuesday_Last2.Day, 21, 30, 0);

            //DateTime Wednesday_Last1 = Wednesday_1[Wednesday_1.Length - 1].ShowDate;
            //DateTime Wednesday_Last_1 = new DateTime(Wednesday_Last1.Year, Wednesday_Last1.Month, Wednesday_Last1.Day, 21, 30, 0);

            //DateTime Wednesday_Last2 = Wednesday_2[Wednesday_2.Length - 1].ShowDate;
            //DateTime Wednesday_Last_2 = new DateTime(Wednesday_Last2.Year, Wednesday_Last2.Month, Wednesday_Last2.Day, 21, 30, 0);

            //DateTime Thursday_Last1 = Thursday_1[Thursday_1.Length - 1].ShowDate;
            //DateTime Thursday_Last_1 = new DateTime(Thursday_Last1.Year, Thursday_Last1.Month, Thursday_Last1.Day, 21, 30, 0);

            //DateTime Thursday_Last2 = Thursday_2[Thursday_2.Length - 1].ShowDate;
            //DateTime Thursday_Last_2 = new DateTime(Thursday_Last2.Year, Thursday_Last2.Month, Thursday_Last2.Day, 21, 30, 0);

            DateTime Friday_Last1 = Friday_1[Friday_1.Length - 1].ShowDate;
            DateTime Friday_Last_1 = new DateTime(Friday_Last1.Year, Friday_Last1.Month, Friday_Last1.Day, 21, 30, 0);

            //DateTime Friday_Last2 = Friday_2[Friday_2.Length - 1].ShowDate;
            //DateTime Friday_Last_2 = new DateTime(Friday_Last2.Year, Friday_Last2.Month, Friday_Last2.Day, 21, 30, 0);

            //DateTime Saturday_Last1 = Saturday_1[Saturday_1.Length - 1].ShowDate;
            //DateTime Saturday_Last_1 = new DateTime(Saturday_Last1.Year, Saturday_Last1.Month, Saturday_Last1.Day, 21, 30, 0);

            //DateTime Saturday_Last2 = Saturday_2[Saturday_2.Length - 1].ShowDate;
            //DateTime Saturday_Last_2 = new DateTime(Saturday_Last2.Year, Saturday_Last2.Month, Saturday_Last2.Day, 21, 30, 0);

            //DateTime Sunday_Last1 = Sunday_1[Sunday_1.Length - 1].ShowDate;
            //DateTime Sunday_Last_1 = new DateTime(Sunday_Last1.Year, Sunday_Last1.Month, Sunday_Last1.Day, 21, 30, 0);

            //DateTime Sunday_Last2 = Sunday_2[Sunday_2.Length - 1].ShowDate;
            //DateTime Sunday_Last_2 = new DateTime(Sunday_Last2.Year, Sunday_Last2.Month, Sunday_Last2.Day, 21, 30, 0);

            //if (Monday_1[Monday_1.Length - 1].ShowDate.AddMinutes(Monday_1[Monday_1.Length - 1].SponsoringMovie.RunningTime) <= Monday_Last_1)
            //{
            //    earlyerror += 1;
            //}
            //if (Monday_2[Monday_2.Length - 1].ShowDate.AddMinutes(Monday_1[Monday_1.Length - 1].SponsoringMovie.RunningTime) <= Monday_Last_2)
            //{
            //    earlyerror += 1;
            //}
            //if (Tuesday_1[Tuesday_1.Length - 1].ShowDate.AddMinutes(Tuesday_1[Tuesday_1.Length - 1].SponsoringMovie.RunningTime) <= Tuesday_Last_1)
            //{
            //    earlyerror += 1;
            //}
            //if (Tuesday_2[Tuesday_2.Length - 1].ShowDate.AddMinutes(Tuesday_2[Tuesday_2.Length - 1].SponsoringMovie.RunningTime) <= Tuesday_Last_2)
            //{
            //    earlyerror += 1;
            //}
            //if (Wednesday_1[Wednesday_1.Length - 1].ShowDate.AddMinutes(Wednesday_1[Wednesday_1.Length - 1].SponsoringMovie.RunningTime) <= Wednesday_Last_1)
            //{
            //    earlyerror += 1;
            //}
            //if (Wednesday_2[Wednesday_2.Length - 1].ShowDate.AddMinutes(Wednesday_2[Wednesday_2.Length - 1].SponsoringMovie.RunningTime) <= Wednesday_Last_2)
            //{
            //    earlyerror += 1;
            //}
            //if (Thursday_1[Thursday_1.Length - 1].ShowDate.AddMinutes(Thursday_1[Thursday_1.Length - 1].SponsoringMovie.RunningTime) <= Thursday_Last_1)
            //{
            //    earlyerror += 1;
            //}
            //if (Thursday_2[Thursday_2.Length - 1].ShowDate.AddMinutes(Thursday_2[Thursday_2.Length - 1].SponsoringMovie.RunningTime) <= Thursday_Last_2)
            //{
            //    earlyerror += 1;
            //}
            if (Friday_1[Friday_1.Length - 1].ShowDate.AddMinutes(Friday_1[Friday_1.Length - 1].SponsoringMovie.RunningTime) <= Friday_Last_1)
            {
                earlyerror += 1;
            }
            //if (Friday_2[Friday_2.Length - 1].ShowDate.AddMinutes(Friday_2[Friday_2.Length - 1].SponsoringMovie.RunningTime) <= Friday_Last_2)
            //{
            //    earlyerror += 1;
            //}
            //if (Saturday_1[Saturday_1.Length - 1].ShowDate.AddMinutes(Saturday_1[Saturday_1.Length - 1].SponsoringMovie.RunningTime) <= Saturday_Last_1)
            //{
            //    earlyerror += 1;
            //}
            //if (Saturday_2[Saturday_2.Length - 1].ShowDate.AddMinutes(Saturday_2[Saturday_2.Length - 1].SponsoringMovie.RunningTime) <= Saturday_Last_2)
            //{
            //    earlyerror += 1;
            //}
            //if (Sunday_1[Sunday_1.Length - 1].ShowDate.AddMinutes(Sunday_1[Sunday_1.Length - 1].SponsoringMovie.RunningTime) <= Sunday_Last_1)
            //{
            //    earlyerror += 1;
            //}
            //if (Sunday_2[Sunday_2.Length - 1].ShowDate.AddMinutes(Sunday_2[Sunday_2.Length - 1].SponsoringMovie.RunningTime) <= Sunday_Last_2)
            //{
            //    earlyerror += 1;
            //}


            //for (int i = 0; i < Monday_1.Length - 1; i++)
            //{
            //    DateTime firstend = Monday_1[i].ShowDate.AddMinutes(Monday_1[i].SponsoringMovie.RunningTime);
            //    DateTime nextstart = Monday_1[i + 1].ShowDate;

            //    var gap = nextstart.Subtract(firstend).TotalMinutes;
            //    if (gap < 25 || gap > 45)
            //    {
            //        errorcount += 1;
            //    }
            //}
            //for (int i = 0; i < Monday_2.Length - 1; i++)
            //{
            //    DateTime firstend = Monday_2[i].ShowDate.AddMinutes(Monday_2[i].SponsoringMovie.RunningTime);
            //    DateTime nextstart = Monday_2[i + 1].ShowDate;

            //    var gap = nextstart.Subtract(firstend).TotalMinutes;
            //    if (gap < 25 || gap > 45)
            //    {
            //        errorcount += 1;
            //    }
            //}
            //for (int i = 0; i < Tuesday_1.Length - 1; i++)
            //{
            //    DateTime firstend = Tuesday_1[i].ShowDate.AddMinutes(Tuesday_1[i].SponsoringMovie.RunningTime);
            //    DateTime nextstart = Tuesday_1[i + 1].ShowDate;

            //    var gap = nextstart.Subtract(firstend).TotalMinutes;
            //    if (gap < 25 || gap > 45)
            //    {
            //        errorcount += 1;
            //    }
            //}
            //for (int i = 0; i < Tuesday_2.Length - 1; i++)
            //{
            //    DateTime firstend = Tuesday_2[i].ShowDate.AddMinutes(Tuesday_2[i].SponsoringMovie.RunningTime);
            //    DateTime nextstart = Tuesday_2[i + 1].ShowDate;

            //    var gap = nextstart.Subtract(firstend).TotalMinutes;
            //    if (gap < 25 || gap > 45)
            //    {
            //        errorcount += 1;
            //    }
            //}
            //for (int i = 0; i < Wednesday_1.Length - 1; i++)
            //{
            //    DateTime firstend = Wednesday_1[i].ShowDate.AddMinutes(Wednesday_1[i].SponsoringMovie.RunningTime);
            //    DateTime nextstart = Wednesday_1[i + 1].ShowDate;

            //    var gap = nextstart.Subtract(firstend).TotalMinutes;
            //    if (gap < 25 || gap > 45)
            //    {
            //        errorcount += 1;
            //    }
            //}
            //for (int i = 0; i < Wednesday_2.Length - 1; i++)
            //{
            //    DateTime firstend = Wednesday_2[i].ShowDate.AddMinutes(Wednesday_2[i].SponsoringMovie.RunningTime);
            //    DateTime nextstart = Wednesday_2[i + 1].ShowDate;

            //    var gap = nextstart.Subtract(firstend).TotalMinutes;
            //    if (gap < 25 || gap > 45)
            //    {
            //        errorcount += 1;
            //    }
            //}
            //for (int i = 0; i < Thursday_1.Length - 1; i++)
            //{
            //    DateTime firstend = Thursday_1[i].ShowDate.AddMinutes(Thursday_1[i].SponsoringMovie.RunningTime);
            //    DateTime nextstart = Thursday_1[i + 1].ShowDate;

            //    var gap = nextstart.Subtract(firstend).TotalMinutes;
            //    if (gap < 25 || gap > 45)
            //    {
            //        errorcount += 1;
            //    }
            //}
            //for (int i = 0; i < Thursday_2.Length - 1; i++)
            //{
            //    DateTime firstend = Thursday_2[i].ShowDate.AddMinutes(Thursday_2[i].SponsoringMovie.RunningTime);
            //    DateTime nextstart = Thursday_2[i + 1].ShowDate;

            //    var gap = nextstart.Subtract(firstend).TotalMinutes;
            //    if (gap < 25 || gap > 45)
            //    {
            //        errorcount += 1;
            //    }
            //}
            for (int i = 0; i < Friday_1.Length - 1; i++)
            {
                DateTime firstend = Friday_1[i].ShowDate.AddMinutes(Friday_1[i].SponsoringMovie.RunningTime);
                DateTime nextstart = Friday_1[i + 1].ShowDate;

                var gap = nextstart.Subtract(firstend).TotalMinutes;
                if (gap < 25 || gap > 45)
                {
                    errorcount += 1;
                }
            }
            //for (int i = 0; i < Friday_2.Length - 1; i++)
            //{
            //    DateTime firstend = Friday_2[i].ShowDate.AddMinutes(Friday_2[i].SponsoringMovie.RunningTime);
            //    DateTime nextstart = Friday_2[i + 1].ShowDate;

            //    var gap = nextstart.Subtract(firstend).TotalMinutes;
            //    if (gap < 25 || gap > 45)
            //    {
            //        errorcount += 1;
            //    }
            //}
            //for (int i = 0; i < Saturday_1.Length - 1; i++)
            //{
            //    DateTime firstend = Saturday_1[i].ShowDate.AddMinutes(Saturday_1[i].SponsoringMovie.RunningTime);
            //    DateTime nextstart = Saturday_1[i + 1].ShowDate;

            //    var gap = nextstart.Subtract(firstend).TotalMinutes;
            //    if (gap < 25 || gap > 45)
            //    {
            //        errorcount += 1;
            //    }
            //}
            //for (int i = 0; i < Saturday_2.Length - 1; i++)
            //{
            //    DateTime firstend = Saturday_2[i].ShowDate.AddMinutes(Saturday_2[i].SponsoringMovie.RunningTime);
            //    DateTime nextstart = Saturday_2[i + 1].ShowDate;

            //    var gap = nextstart.Subtract(firstend).TotalMinutes;
            //    if (gap < 25 || gap > 45)
            //    {
            //        errorcount += 1;
            //    }
            //}
            //for (int i = 0; i < Sunday_1.Length - 1; i++)
            //{
            //    DateTime firstend = Sunday_1[i].ShowDate.AddMinutes(Sunday_1[i].SponsoringMovie.RunningTime);
            //    DateTime nextstart = Sunday_1[i + 1].ShowDate;

            //    var gap = nextstart.Subtract(firstend).TotalMinutes;
            //    if (gap < 25 || gap > 45)
            //    {
            //        errorcount += 1;
            //    }
            //}
            //for (int i = 0; i < Sunday_2.Length - 1; i++)
            //{
            //    DateTime firstend = Sunday_2[i].ShowDate.AddMinutes(Sunday_2[i].SponsoringMovie.RunningTime);
            //    DateTime nextstart = Sunday_2[i + 1].ShowDate;

            //    var gap = nextstart.Subtract(firstend).TotalMinutes;
            //    if (gap < 25 || gap > 45)
            //    {
            //        errorcount += 1;
            //    }
            //}
            if (errorcount == 0 && earlyerror == 0)
            {
                foreach (Showing publishing in CheckShowings)
                {
                    publishing.IsPublished = PublishedStatus.IsPublished;
                    db.Entry(publishing).State = EntityState.Modified;
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
                //return View(CheckShowings);
            }
            else
            {
                if (errorcount >= 1)
                {
                    ViewBag.ErrorCount = "Movie(s) scheduled must have a gap between 25-45 minutes; the gap between must not be smaller or longer than that amount";
                }
                if (earlyerror >= 1)
                {
                    ViewBag.ErrorCount = "The last scheduled movie of a day in each theater must NOT end before 9:30 PM. Please add in another showing to complete the full schedule";
                }
                return RedirectToAction("Index");
            }
        }


        //[HttpPost] 
        //public ActionResult Publish([Bind(Include = "ShowingID, Theatre, ShowDate, IsPublished")]List<Showing> CheckShowings)
        //{
        //    foreach(Showing publishing in CheckShowings)
        //    {
        //        publishing.IsPublished = PublishedStatus.IsPublished;
        //        db.Entry(publishing).State = EntityState.Modified;
        //        db.SaveChanges();
        //    }
        //    return View("Published", CheckShowings);
        //}
        
        //GET
        //public ActionResult DateSearch()
        //{
        //    return View();
        //}

        //[HttpPost]
        //public ActionResult DateSearch([Bind(Include = "ShowingID, Theatre, ShowDate")]DateTime? datSelectedDate)
        //{

        //    var query = from s in db.Showings
        //                select s;

        //    if (datSelectedDate != null)
        //    {
        //        //needed truncate time method because we were comparing a showdate with a specific time compared to one without just a date
        //        query = query.Where(m => DbFunctions.TruncateTime(m.ShowDate) == datSelectedDate);
        //    }

        //    List<Showing> SelectedShowings = query.ToList();
        //    //order list
        //    SelectedShowings.OrderByDescending(m => m.SponsoringMovie.MovieTitle);

        //    ViewBag.AllShowings = db.Showings.Count();
        //    ViewBag.SelectedShowings = SelectedShowings.Count();
        //    //send list to view
        //    return View("Index", SelectedShowings);
        //}

        public ActionResult DetailedSearch()
        {
            ViewBag.AllGenres = GetAllGenres();
            return View();
        }

        public ActionResult DisplaySearchResults(DateTime? datSelectedDate, String SearchMovieTitle, String SearchTagline, 
            int[] SearchGenre, String SelectedYear, MPAArating SelectedMPAARating, String SearchActor, YearRank SelectedSortOrder,
            String StarCount, Option SelectedOption)
        {

            //if they selected a search string, limit results to only repos that meet the criteria
            //create query
            var query = from m in db.Showings
                        select m;

            if (datSelectedDate != null)
            {
                //needed truncate time method because we were comparing a showdate with a specific time compared to one without just a date
                query = query.Where(m => DbFunctions.TruncateTime(m.ShowDate) == datSelectedDate);
            }

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

            if (StarCount != null && StarCount != "")
            {
                Decimal decStar;
                try
                {
                    decStar = Convert.ToDecimal(StarCount);
                }
                catch
                {
                    //not sure which viewbag to put here
                    ViewBag.AllMovies = GetAllMovies();
                    return View("DetailedSearch");
                }

                //switch (SelectedOption)
                //{
                //    case Option.GreaterThan:
                //        query = query.Where(r => r.SponsoringMovie.AverageUserRating > decStar);
                //        break;

                //    case Option.LessThan:
                //        query = query.Where(r => r.SponsoringMovie.AverageUserRating <= decStar);
                //        break;
                //}
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
                var user = UserManager.FindById(User.Identity.GetUserId());
                SendMovieRescheduleEmail(user);
                return RedirectToAction("Index");
            }

            ViewBag.AllMovies = GetAllMovies(showing);
            return View(showing);
        }

        private void SendMovieRescheduleEmail(AppUser user)
        {
            var body = $@"Dear {user.FirstName},  you have rescheduled a movie";
            EmailMessaging.SendEmail(user.Email, "Team 11 Movie Reschedule Confirmation", body);
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
            var user = UserManager.FindById(User.Identity.GetUserId());
            SendMovieCancelEmail(user);
            return RedirectToAction("Index");
        }

        private void SendMovieCancelEmail(AppUser user)
        {
            var body = $@"Dear {user.FirstName}, you have cancelled a movie";
            EmailMessaging.SendEmail(user.Email, "Team 11 Movie Cancelled Confirmation", body);
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
