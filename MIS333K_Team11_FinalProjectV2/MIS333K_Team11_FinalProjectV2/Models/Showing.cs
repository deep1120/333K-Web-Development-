using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using MIS333K_Team11_FinalProjectV2.Utilities;

namespace MIS333K_Team11_FinalProjectV2.Models
{
<<<<<<< HEAD
    public class Showing
    {
        public class CustomDateRangeAttribute : RangeAttribute
        {
            public CustomDateRangeAttribute() : base(typeof(DateTime), DateTime.Now.AddDays(7).ToString(), DateTime.Now.AddDays(14).ToString())
            { }
        }

=======

    public enum Theatre { Theatre1, Theatre2 }

    public class Showing
    {
>>>>>>> 916b8ddac93c6d14f26b63ba7edbdaf87f8e5101
        public Int32 ShowingID { get; set; }

        [Display(Name = "Showing Number")]              //not sure if needed
        public Int32 ShowingNumber { get; set; }

        [Display(Name = "Showing Name")]
        public String ShowingName { get; set; } //TODO: not sure if this is needed

        [Required(ErrorMessage = "Showing must be scheduled")]
        [CustomDateRange(ErrorMessage = "Showing must be scheduled starting a week from today and through the next 7 days afterwards")]
        [Display(Name = "Show Date")]
        [DataType(DataType.DateTime)]
        public DateTime ShowDate { get; set; }

        //[Display(Name = "Showing Time")] //Use one of these
        //[Display(Name = "Start Time")]
        //[DataType(DataType.Time)]
        //public DateTime StartTime { get; set; }

        [Display(Name = "End Time")] //a calculated value = start time + running time of movie
        [DataType(DataType.Time)]
        public DateTime? EndTime
        {
            get { return ShowDate.AddMinutes(SponsoringMovies.Sum(m => m.RunningTime)); }
        }

        [Display(Name = "Running Time")]                              //not sure if needed either since related to movie
        public Int32 RunTime
        {
            //get { return SponsoringMovies.Sum(m=> m.RunningTime);}
            get;set; 
        }

        [Display(Name = "Ticket Price")]               //not sure if needed since will be from ticket.cs
        public Decimal TicketPrice { get; set; }

        [Required(ErrorMessage = "Theatre # is Required")]
        [Display(Name = "IsPublished")]
        public bool IsPublished { get; set; }

        [Required(ErrorMessage = "Theatre is Required")]
        [EnumDataType(typeof(Theatre))]
        [Display (Name = "Theatre" )]
        public Theatre Theatre { get; set; }

        //May insert "Future showing"
        public virtual List <Movie> SponsoringMovies { get; set; }
        public virtual List<Ticket> Tickets { get; set; }
        public virtual List<Seat> Seats { get; set; }

        public Showing()
        {
            if (SponsoringMovies == null)
            {
                SponsoringMovies = new List<Movie>();
            }

            if (Tickets == null)
            {
                Tickets = new List<Ticket>();
            }
        }
    }
}
