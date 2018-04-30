﻿using System;
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
                var userId = User.Identity.GetUserId();
                var user = db.Users.SingleOrDefault(x => x.Id == userId);
                model.CustomerName = user.FirstName;
                model.FeaturedMovie = new Movie();
                //model.FeaturedAlbum = new Album();
                //model.FeaturedArtist = new Artist();

                var featuredMovie = db.Movies.FirstOrDefault(x => x.FeaturedMovie);
                //var featuredAlbum = db.Albums.FirstOrDefault(x => x.FeaturedAlbum);
                //var featuredArtist = db.Artists.FirstOrDefault(x => x.FeaturedArtist);
                if (featuredMovie != null)
                {
                    model.FeaturedMovie = featuredMovie;
                }
                //    if (featuredAlbum != null)
                //    {
                //        model.FeaturedAlbum = featuredAlbum;
                //    }
                //    if (featuredArtist != null)
                //    {
                //        model.FeaturedArtist = featuredArtist;
                //    }
                //
            }
            return View(model);
        }
    }
}


//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Data.Entity;
//using System.Linq;
//using System.Net;
//using System.Web;
//using System.Web.Mvc;
//using MIS333K_Team11_FinalProjectV2.Models;
//using static MIS333K_Team11_FinalProjectV2.Models.AppUser;
////using MIS333K_Team11_FinalProjectV2.DAL;

//namespace MIS333K_Team11_FinalProjectV2.Controllers
//{
//    public class HomeController : Controller
//    {
//        private AppDbContext db = new AppDbContext();

//        //Get: Home 
//        public ActionResult Index()
//        {
//            return View();
//        }

//        public ActionResult WelcomePage()
//        {
//            return View();
//        }
//    }
//}