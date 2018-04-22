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

        //Get: Home 
        public ActionResult Index()
        {
            return View();
        }

    }
}