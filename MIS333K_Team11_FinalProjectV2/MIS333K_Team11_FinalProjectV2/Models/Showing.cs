using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MIS333K_Team11_FinalProjectV2.Models
{
    public class Showing
    {
        public Int32 ShowingID { get; set; }

        //[Display(Name = "Showing Time")] //Use one of these
        [Display(Name = "Start Time")]
        public DateTime StartTime { get; set; }

        [Display(Name = "End Time")] //a calculated value = start time + running time of movie
        public DateTime EndTime {get; set;}

        [Display(Name = "Running Time")]
        public Int32 RunTime { get; set; }

        //May insert "Future showing"
        public virtual Movie Movie { get; set; }
        public virtual List<Ticket> Tickets { get; set; }


        public Showing()
        {
            if (Tickets == null)
            {
                Tickets = new List<Ticket>();
            }
        }
    }
}
