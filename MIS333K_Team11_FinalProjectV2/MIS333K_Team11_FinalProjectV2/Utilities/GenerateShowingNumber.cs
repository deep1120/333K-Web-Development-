using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//using MIS333K_Team11_FinalProjectV2.DAL;
using MIS333K_Team11_FinalProjectV2.Models;

namespace MIS333K_Team11_FinalProjectV2.Utilities
{
    public static class GenerateShowingNumber
    {
        public static Int32 GetNextShowingNumber()
        {
            //we need a db context to connect to the database
            AppUser.AppDbContext db = new AppUser.AppDbContext();

            Int32 intMaxShowingNumber; //the current maximum course number
            Int32 intNextShowingNumber; //the course number for the next class

            if (db.Showings.Count() == 0) //there are no courses in the database yet

            {
                intMaxShowingNumber = 0; //course numbers start at 1
            }
            else
            {
                intMaxShowingNumber = db.Showings.Max(p => p.ShowingNumber); //this is the highest number in the database right now
            }

            //add one to the current max to find the next one
            intNextShowingNumber = intMaxShowingNumber + 1;

            //return the value
            return intNextShowingNumber;
        }
    }
}