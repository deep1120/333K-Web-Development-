using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MIS333K_Team11_FinalProjectV2.Models;

namespace MIS333K_Team11_FinalProjectV2.Utilities
{
    public static class GenerateNextConfirmationNumber
    {
        public static Int32 GetNextConfirmation()
        {
            //we need a db context to connect to the database
            Models.AppUser.AppDbContext db = new Models.AppUser.AppDbContext();

            Int32 intMaxConfirmationNumber; //the current maximum course number
            Int32 intNextConfirmationNumber; //the course number for the next class

            if (db.Orders.Count() == 0) //there are no courses in the database yet

            {
                intMaxConfirmationNumber = 10000; //course numbers start at 1
            }
            else
            {
                intMaxConfirmationNumber = db.Orders.Max(p => p.ConfirmationNumber); //this is the highest number in the database right now
            }

            //add one to the current max to find the next one
            intNextConfirmationNumber = intMaxConfirmationNumber + 1;

            //return the value
            return intNextConfirmationNumber;
        }
    }
}