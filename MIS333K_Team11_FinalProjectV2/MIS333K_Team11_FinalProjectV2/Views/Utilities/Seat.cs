﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MIS333K_Team11_FinalProjectV2.Models;

namespace MIS333K_Team11_FinalProjectV2.Utilities
{
    public class Seat
    {
        public Int32 SeatID { get; set; }
        public String SeatName { get; set; }

        public Ticket Ticket; //why doesn't it need virtual?
        public Showing Showing;
    }
}