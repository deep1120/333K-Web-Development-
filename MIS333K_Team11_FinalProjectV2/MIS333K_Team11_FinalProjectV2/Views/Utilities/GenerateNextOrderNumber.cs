using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//using MIS333K_Team11_FinalProjectV2.DAL;

namespace MIS333K_Team11_FinalProjectV2.Utilities
{
    public static class GenerateNextOrderNumber
    {
        public static Int32 GetNextOrderNumber()
        {
            //we need a db context to connect to the database
            Models.AppUser.AppDbContext db = new Models.AppUser.AppDbContext();

            Int32 intMaxOrderNumber; //the current maximum course number
            Int32 intNextOrderNumber; //the course number for the next class

            if (db.Orders.Count() == 0) //there are no registrations in the database yet
            {
                intMaxOrderNumber = 10000; //registration numbers start at 1001
            }
            else
            {
                intMaxOrderNumber = db.Orders.Max(c => c.OrderNumber); //this is the highest number in the database right now
            }

            //add one to the current max to find the next one
            intNextOrderNumber = intMaxOrderNumber + 1;

            //return the value
            return intNextOrderNumber;
        }
    }
}