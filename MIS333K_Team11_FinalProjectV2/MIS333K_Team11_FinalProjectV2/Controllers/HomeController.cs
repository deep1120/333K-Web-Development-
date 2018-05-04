using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MIS333K_Team11_FinalProjectV2.Models;
using System.Data.Entity;
using MIS333K_Team11_FinalProjectV2.ViewModels;
using Microsoft.AspNet.Identity;
using static MIS333K_Team11_FinalProjectV2.Models.AppUser;

namespace MIS333K_Team11_FinalProjectV2.Controllers
{
    public class HomeController : Controller
    {
        AppDbContext db = new AppDbContext();

        // GET: Home
        public ActionResult Index()
        {
            var model = new CustomerWelcomeVM();
            if (User.IsInRole("Customer"))
            {
                //return View(db.Showings.Where(s => s.IsPublished == PublishedStatus.IsPublished && s.ShowDate.Date == DateTime.Now.Date));
                var userId = User.Identity.GetUserId();
                var user = db.Users.SingleOrDefault(x => x.Id == userId);
                model.CustomerName = user.FirstName;
                model.FeaturedMovie = new Movie();

                var featuredMovie = db.Movies.FirstOrDefault(x => x.FeaturedMovie);

                if (featuredMovie != null)
                {
                    model.FeaturedMovie = featuredMovie;
                }
            }
            return View(model);
        }
    }
}
