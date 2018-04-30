<<<<<<< HEAD
﻿using MIS333K_Team11_FinalProjectV2.Models;
using MIS333K_Team11_FinalProjectV2.ViewModel;
using MIS333K_Team11_FinalProjectV2.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static MIS333K_Team11_FinalProjectV2.Models.AppUser;

namespace MIS333K_Team11_FinalProjectV2.Controllers
{
    public class ReportController : Controller
    {
        AppDbContext db = new AppDbContext();
        // GET: Report

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult MovieReport()
        {
            var model = db.Tickets.Where(y => y.MovieID != null && y.MovieID != 0).GroupBy(o => new { MovieID = o.Movie.MovieID, MovieTitle = o.Movie.MovieTitle }).Select(g => new MovieReportVM { MovieID = g.Key.MovieID, MovieTitle = g.Key.MovieTitle, Revenue = g.Sum(x => x.Subtotal), NumberOfPurchase = g.Sum(x => x.Quantity) });
            return View(model);
        }

        //public ActionResult AlbumReport()
        //{
        //    var model = db.OrderDetails.Where(y => y.AlbumID != null && y.AlbumID != 0).GroupBy(o => new { AlbumID = o.Album.AlbumID, AlbumTitle = o.Album.AlbumTitle }).Select(g => new AlbumReportViewModel { AlbumID = g.Key.AlbumID, AlbumTitle = g.Key.AlbumTitle, Revenue = g.Sum(x => x.Subtotal), NumberOfPurchase = g.Sum(x => x.Quantity) });
        //    return View(model);
        //}

        //public ActionResult BandReport()
        //{

        //    return View();
        //}

    }

}
=======
﻿//using MIS333K_Team11_FinalProjectV2.Models;
//using MIS333K_Team11_FinalProjectV2.ViewModel;
//using MIS333K_Team11_FinalProjectV2.ViewModels;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using System.Web.Mvc;
//using static MIS333K_Team11_FinalProjectV2.Models.AppUser;

//namespace MIS333K_Team11_FinalProjectV2.Controllers
//{
//    public class ReportController : Controller
//    {
//        AppDbContext db = new AppDbContext();
//        // GET: Report

//        public ActionResult Index()
//        {
//            return View();
//        }

//        public ActionResult MovieReport()
//        {
//            var model = db.Tickets.Where(y => y.MovieID != null && y.MovieID != 0).GroupBy(o => new { MovieID = o.MovieID.MovieID, MovieTitle = o.Movie.MovieTitle }).Select(g => new MovieReportVM { MovieID = g.Key.MovieID, MovieTitle = g.Key.MovieTitle, Revenue = g.Sum(x => x.Subtotal), NumberOfPurchase = g.Sum(x => x.Quantity) });
//            return View(model);
//        }

//        //public ActionResult AlbumReport()
//        //{
//        //    var model = db.OrderDetails.Where(y => y.AlbumID != null && y.AlbumID != 0).GroupBy(o => new { AlbumID = o.Album.AlbumID, AlbumTitle = o.Album.AlbumTitle }).Select(g => new AlbumReportViewModel { AlbumID = g.Key.AlbumID, AlbumTitle = g.Key.AlbumTitle, Revenue = g.Sum(x => x.Subtotal), NumberOfPurchase = g.Sum(x => x.Quantity) });
//        //    return View(model);
//        //}

//        //public ActionResult BandReport()
//        //{

//        //    return View();
//        //}

//    }

//}
>>>>>>> 4363380832063b7c583d49c4e62967ebeeea8c42
