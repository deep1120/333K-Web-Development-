using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MIS333K_Team11_FinalProjectV2.Models;

namespace MIS333K_Team11_FinalProjectV2.Utilities
{
    public class SeatHelper
    {
        public static SelectList FindAvailableSeats(List<Ticket> tickets)
        {
            List<String> TakenSeatNames = new List<String>();
            foreach (Ticket t in tickets)
            {
                TakenSeatNames.Add(t.TicketSeat);
            }

            List<String> AllSeatNames = new List<String>();
            AllSeatNames.Add("A1");
            AllSeatNames.Add("A2");
            AllSeatNames.Add("A3");
            AllSeatNames.Add("A4");
            AllSeatNames.Add("A5");
            AllSeatNames.Add("A6");
            AllSeatNames.Add("A7");
            AllSeatNames.Add("A8");
            AllSeatNames.Add("B1");
            AllSeatNames.Add("B2");
            AllSeatNames.Add("B3");
            AllSeatNames.Add("B4");
            AllSeatNames.Add("B5");
            AllSeatNames.Add("B6");
            AllSeatNames.Add("B7");
            AllSeatNames.Add("B8");
            AllSeatNames.Add("C1");
            AllSeatNames.Add("C2");
            AllSeatNames.Add("C3");
            AllSeatNames.Add("C4");
            AllSeatNames.Add("C5");
            AllSeatNames.Add("C6");
            AllSeatNames.Add("C7");
            AllSeatNames.Add("C8");
            AllSeatNames.Add("D1");
            AllSeatNames.Add("D2");
            AllSeatNames.Add("D3");
            AllSeatNames.Add("D4");
            AllSeatNames.Add("D5");
            AllSeatNames.Add("D6");
            AllSeatNames.Add("D7");
            AllSeatNames.Add("D8");

            //Add the rest of the seat names

            List<String> AvailableSeats = AllSeatNames.Except(TakenSeatNames).ToList();

            SelectList slAvailableSeats = new SelectList(AvailableSeats);

            return slAvailableSeats;
        }
        //public static SelectList FindAvailableSeats(List<Ticket> tickets) //tickets is the list of 
        //{
        //    List<Seat> TakenSeats = new List<Seat>();

        //    foreach (Ticket t in tickets)
        //    {
        //        Seat s = new Seat();
        //        s.SeatName = t.TicketSeat;
        //        s.SeatID = GetSeatID(s.SeatName);
        //        TakenSeats.Add(s);
        //    }

        //    List<Seat> AvailableSeats = GetAllSeats().Except(TakenSeats).ToList();

        //    SelectList slAvailableSeats = new SelectList(AvailableSeats, "SeatID", "SeatName");
        //    return slAvailableSeats;

        //}


        public static List<Seat> GetAllSeats()
        {
            List<Seat> AllSeats = new List<Seat>();

            Seat s1 = new Seat() { SeatID = 0, SeatName = "A1" };
            AllSeats.Add(s1);

            Seat s2 = new Seat() { SeatID = 1, SeatName = "A2" };
            AllSeats.Add(s2);

            Seat s3 = new Seat() { SeatID = 2, SeatName = "A3" };
            AllSeats.Add(s3);

            Seat s4 = new Seat() { SeatID = 3, SeatName = "A4" };
            AllSeats.Add(s4);

            Seat s5 = new Seat() { SeatID = 4, SeatName = "A5" };
            AllSeats.Add(s5);

            Seat s6 = new Seat() { SeatID = 5, SeatName = "A6" };
            AllSeats.Add(s6);

            Seat s7 = new Seat() { SeatID = 6, SeatName = "A7" };
            AllSeats.Add(s7);

            Seat s8 = new Seat() { SeatID = 7, SeatName = "A8" };
            AllSeats.Add(s8);

            Seat s9 = new Seat() { SeatID = 8, SeatName = "B1" };
            AllSeats.Add(s9);

            Seat s10 = new Seat() { SeatID = 9, SeatName = "B2" };
            AllSeats.Add(s10);

            Seat s11 = new Seat() { SeatID = 10, SeatName = "B3" };
            AllSeats.Add(s11);

            Seat s12 = new Seat() { SeatID = 11, SeatName = "B4" };
            AllSeats.Add(s12);

            Seat s13 = new Seat() { SeatID = 12, SeatName = "B5" };
            AllSeats.Add(s13);

            Seat s14 = new Seat() { SeatID = 13, SeatName = "B6" };
            AllSeats.Add(s14);

            Seat s15 = new Seat() { SeatID = 14, SeatName = "B7" };
            AllSeats.Add(s15);

            Seat s16 = new Seat() { SeatID = 15, SeatName = "B8" };
            AllSeats.Add(s16);

            Seat s17 = new Seat() { SeatID = 16, SeatName = "C1" };
            AllSeats.Add(s17);

            Seat s18 = new Seat() { SeatID = 17, SeatName = "C2" };
            AllSeats.Add(s18);

            Seat s19 = new Seat() { SeatID = 18, SeatName = "C3" };
            AllSeats.Add(s19);

            Seat s20 = new Seat() { SeatID = 19, SeatName = "C4" };
            AllSeats.Add(s20);

            Seat s21 = new Seat() { SeatID = 20, SeatName = "C5" };
            AllSeats.Add(s21);

            Seat s22 = new Seat() { SeatID = 21, SeatName = "C6" };
            AllSeats.Add(s22);

            Seat s23 = new Seat() { SeatID = 22, SeatName = "C7" };
            AllSeats.Add(s23);

            Seat s24 = new Seat() { SeatID = 23, SeatName = "C8" };
            AllSeats.Add(s24);

            Seat s25 = new Seat() { SeatID = 24, SeatName = "D1" };
            AllSeats.Add(s25);

            Seat s26 = new Seat() { SeatID = 25, SeatName = "D2" };
            AllSeats.Add(s26);

            Seat s27 = new Seat() { SeatID = 26, SeatName = "D3" };
            AllSeats.Add(s27);

            Seat s28 = new Seat() { SeatID = 27, SeatName = "D4" };
            AllSeats.Add(s28);

            Seat s29 = new Seat() { SeatID = 28, SeatName = "D5" };
            AllSeats.Add(s29);

            Seat s30 = new Seat() { SeatID = 29, SeatName = "D6" };
            AllSeats.Add(s30);

            Seat s31 = new Seat() { SeatID = 30, SeatName = "D7" };
            AllSeats.Add(s31);

            Seat s32 = new Seat() { SeatID = 31, SeatName = "D8" };
            AllSeats.Add(s32);

            return AllSeats;
        }

        public static Int32 GetSeatID(String seatName)
        {
            if (seatName == "A1") return 0; 
            if (seatName == "A2") return 1;
            if (seatName == "A3") return 2;
            if (seatName == "A4") return 3;
            if (seatName == "A5") return 4;
            if (seatName == "A6") return 5;
            if (seatName == "A7") return 6;
            if (seatName == "A8") return 7;
            if (seatName == "B1") return 8;
            if (seatName == "B2") return 9;
            if (seatName == "B3") return 10;
            if (seatName == "B4") return 11;
            if (seatName == "B5") return 12;
            if (seatName == "B6") return 13;
            if (seatName == "B7") return 14;
            if (seatName == "B8") return 15;
            if (seatName == "C1") return 16;
            if (seatName == "C2") return 17;
            if (seatName == "C3") return 18;
            if (seatName == "C4") return 19;
            if (seatName == "C5") return 20;
            if (seatName == "C6") return 21;
            if (seatName == "C7") return 22;
            if (seatName == "C8") return 23;
            if (seatName == "D1") return 24;
            if (seatName == "D2") return 25;
            if (seatName == "D3") return 26;
            if (seatName == "D4") return 27;
            if (seatName == "D5") return 28;
            if (seatName == "D6") return 29;
            if (seatName == "D7") return 30;
            if (seatName == "D8") return 31;

            //we need an else but idk what to put in this?? -- ASK Katie
            else
                return 0;

        }

        public static String GetSeatName(int SeatID)
        {
            if (SeatID == 0) return "A1";
            if (SeatID == 1) return "A2";
            if (SeatID == 2) return "A3";
            if (SeatID == 3) return "A4";
            if (SeatID == 4) return "A5";
            if (SeatID == 5) return "A6";
            if (SeatID == 6) return "A7";
            if (SeatID == 7) return "A8";
            if (SeatID == 8) return "B1";
            if (SeatID == 9) return "B2";
            if (SeatID == 10) return "B3";
            if (SeatID == 11) return "B4";
            if (SeatID == 12) return "B5";
            if (SeatID == 13) return "B6";
            if (SeatID == 14) return "B7";
            if (SeatID == 15) return "B8";
            if (SeatID == 16) return "C1";
            if (SeatID == 17) return "C2";
            if (SeatID == 18) return "C3";
            if (SeatID == 19) return "C4";
            if (SeatID == 20) return "C5";
            if (SeatID == 21) return "C6";
            if (SeatID == 22) return "C7";
            if (SeatID == 23) return "C8";
            if (SeatID == 24) return "D1";
            if (SeatID == 25) return "D2";
            if (SeatID == 26) return "D3";
            if (SeatID == 27) return "D4";
            if (SeatID == 28) return "D5";
            if (SeatID == 29) return "D6";
            if (SeatID == 30) return "D7";
            if (SeatID == 31) return "D8";

            else
                return "";
        }
    }
}