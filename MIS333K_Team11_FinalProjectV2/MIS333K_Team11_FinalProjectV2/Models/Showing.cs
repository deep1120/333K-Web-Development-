using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MIS333K_Team11_FinalProjectV2.Models
{
    //think about adding an enum for the ShowingStatus { Pending, Published}
    public enum Theatre { Theatre1, Theatre2 }
    public enum PublishedStatus { IsPublished, NotPublished}

    public class Showing
    {
        //public class CustomDateRangeAttribute : RangeAttribute
        //{
        //    public CustomDateRangeAttribute() : base(typeof(DateTime), DateTime.Now.AddDays(7).Date.ToString(), DateTime.Now.AddDays(14).Date.ToString())
        //    { }
        //}

        public Int32 ShowingID { get; set; }

        [Display(Name = "Showing Number")]
        public Int32 ShowingNumber { get; set; }

        [Display(Name = "Showing Name")]
        public String ShowingName { get; set; } //TODO: not sure if this is needed

        [Required(ErrorMessage = "Showing must be scheduled")]
        //[CustomDateRange(ErrorMessage = "Showing must be scheduled starting a week from today and through the next 7 days afterwards")]
        [Display(Name = "Show Date")]
        [DataType(DataType.DateTime)]
        public DateTime ShowDate { get; set; }

        [Display(Name = "Showing and Time")]
        public String ShowingNameAndDate
        {
            get { return (SponsoringMovie.MovieTitle + " " + ShowDate); }
        }

        [Display(Name = "Ticket Price")]
        public Decimal TicketPrice { get; set; }

        [Required(ErrorMessage = "Theatre # is Required")]
        [EnumDataType(typeof(Theatre))]
        [Display(Name = "Theatre")]
        public Theatre Theatre { get; set; }

        [EnumDataType(typeof(PublishedStatus))]
        [Display(Name = "Published Status")]
        public PublishedStatus IsPublished { get; set; }

        //May insert "Future showing"
        public virtual Movie SponsoringMovie { get; set; }
        public virtual List<Ticket> Tickets { get; set; }

        public Showing()
        {
            //if (SponsoringMovies == null)
            //{
            //    SponsoringMovies = new List<Movie>();
            //}

            if (Tickets == null)
            {
                Tickets = new List<Ticket>();
            }
        }
    }
}
