using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MIS333K_Team11_FinalProjectV2.Models
{
    public class Ticket
    {
        public Int32 TicketID { get; set; }

        [Display(Name = "Ticket Price")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public Decimal TicketPrice { get; set; }

        //special event can be a boolean (special or not)
        //how are we going to code/seed data for the two theaters?
        [Display(Name = "Ticket Seat")] //Do we need to code for special seating?
        public int TicketSeat { get; set; }

        [Display(Name = "Total Fees")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public Decimal TotalFees { get; set; }

        public virtual Showing Showing { get; set; }
        public virtual Order Order { get; set; }
    }
}
