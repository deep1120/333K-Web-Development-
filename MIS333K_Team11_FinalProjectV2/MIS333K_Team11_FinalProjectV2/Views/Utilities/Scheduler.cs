//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Data.Entity;
//using System.Linq;
//using System.Net;
//using System.Web;
//using System.Web.Mvc;//using MIS333K_Team11_FinalProjectV2.Models;
//using static MIS333K_Team11_FinalProjectV2.Models.AppUser;
//namespace MIS333K_Team11_FinalProjectV2.Utilities
//{
//    public static class Scheduler
//    {
//        private static AppDbContext db = new AppDbContext();
//        private static List<Showing> toBePublishedShowings;
//        /// <summary>
//        /// Gets the latest showings to be published.
//        /// </summary>
//        public static List<Showing> ToBePublishedShowings
//        {
//            get
//            {
//                var rv = toBePublishedShowings ?? (toBePublishedShowings = db.Showings.Where(x => !x.IsPublished).ToList());
//                return rv;
//            }
//        }
//        public static bool IsScheduled(Showing showing)
//        {
//            return ToBePublishedShowings.Exists(

//                x => x.ShowingID == showing.ShowingID
//                && (
//                    (x.EndTime >= showing.ShowDate && x.EndTime <= showing.EndTime)
//                    || (x.ShowDate >= showing.ShowDate && x.ShowDate <= showing.EndTime)
//                ));
//        }
//        public static bool Add(Showing showing, out string error)
//        {
//            error = null;
//            if (IsScheduled(showing))
//            {
//                error = "Already scheduled";
//                return false;
//            }
//            //if (!showing.EndTime.HasValue)
//            //{
//            //    showing.EndTime = showing.ShowDate.Value.AddMinutes(showing.RunTime);
//            //}
//            var endTime = showing.EndTime.Value;
//            var midmight = new DateTime(endTime.Year, endTime.Month, endTime.Day, 0, 0, 0).AddDays(1);
//            if (endTime >= midmight)
//            {
//                error = "Over midnight";
//                return false; // Over midnight
//            }
//            var lastShowing = ToBePublishedShowings
//                .Where(x => x.Theatre == showing.Theatre)
//                .OrderByDescending(x => x.EndTime).First();
//            if (lastShowing != null)
//            {
//                var gap = showing.ShowDate.Value.Subtract(lastShowing.EndTime.Value).TotalMinutes;
//                if (gap < 25 || gap > 45)
//                {
//                    error = "Gap shorter than 25 min or longer than 45 min";
//                    return false; // gap shorter than 25 min or longer than 45 min
//                }
//            }
//            // TODO other rules for adding a showing
//            // ...
//            // all rules applied OK
//            ToBePublishedShowings.Add(showing);
//            db.Showings.Add(showing);
//            var count = db.SaveChanges();

//            return count > 0;
//        }
//        public static bool Publish(out string error)
//        {
//            error = null;
//            // TODO - check before publishing
//            // return false if anything has errors
//            // check each day of 7 days
//            var showingsByThreater = ToBePublishedShowings.GroupBy(x => x.Theatre);
//            foreach (var threater in showingsByThreater)
//            {
//                var dailys = threater.GroupBy(x => x.EndTime.Value.Date);
//                // check last movie ending time < 9:30PM
//                foreach (var day in dailys)
//                {
//                    var last = day.OrderByDescending(x => x.EndTime.Value).First();
//                    if (IsBeforeNineThirtyPM(last))
//                    {
//                        error = $"Endtime < 9:30PM, add a late movie in {last.Theatre}";
//                        return false;
//                    }
//                }
//            }
//            foreach (var s in ToBePublishedShowings)
//            {
//                s.IsPublished = true;
//            }
//            db.SaveChanges();
//            return true;
//        }
//        public static bool IsBeforeNineThirtyPM(Showing showing)
//        {
//            var endTime = showing.EndTime.Value;
//            var nineThiryPM = new DateTime(endTime.Year, endTime.Month, endTime.Day, 21, 30, 0);
//            return (endTime < nineThiryPM); // ending early then 9:30PM
//        }
//    }
//}