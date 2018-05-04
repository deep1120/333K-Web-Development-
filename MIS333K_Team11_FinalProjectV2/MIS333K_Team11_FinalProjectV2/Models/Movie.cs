using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MIS333K_Team11_FinalProjectV2.Models
{
    public enum MPAArating
    {
        Unrated, G, PG, PG13, R, All
    }

    public class Movie
    {
        public Int32 MovieID { get; set; }

        //[Required(ErrorMessage = "Movie Number is required")]
        public Int32 MovieNumber { get; set; }

        //[Required(ErrorMessage = "Movie Title is required")]
        [Display(Name = "Movie Title")]
        public String MovieTitle { get; set; }

        //[Required(ErrorMessage = "Every movie must have one genre")]
        //[Range(1, 1000, ErrorMessage = "Must have at least one genre")]
        [Display(Name = "Genre")]
        public String Genre { get; set; }

        //[Required(ErrorMessage = "Movie Overview is required")]
        [Display(Name = "Movie Overview")]
        public String MovieOverview { get; set; }

        //[Required(ErrorMessage = "Movie Running Time must be a valid whole number")] //valid whole number
        [Display(Name = "Movie Running Time")]
        public Int32 RunningTime { get; set; }

        [Display(Name = "Tagline")]
        public String Tagline { get; set; }

        //[Required(ErrorMessage = "Movie MPAA Rating is required")]
        [Display(Name = "MPAA Rating")]
        public MPAArating MPAAratings { get; set; }

        //[Required(ErrorMessage = "Movie Actor is required")]
        //[Range(1,1000, ErrorMessage = "At least one Actor is required")]
        [Display(Name = "Actor(s)")]
        public String Actor { get; set; }

        //[Required(ErrorMessage = "Movie Revenue is required")]
        [Display(Name = "Movie Revenue")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public Decimal MovieRevenue { get; set; }

        //[Required(ErrorMessage = "Movie Release Date must be on or after Jan 1, 1927")] //how do you do after or on a specific data
        [Display(Name = "Release Date")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime ReleaseDate { get; set; }

        //[Display(Name = "Average User Rating")]
        //public Decimal AverageUserRating
        //{
        //    get { return Convert.ToDecimal(Reviews.Average(r => r.StarRating)); }
        //}

        [Display(Name = "Featured Movie")]
        public bool FeaturedMovie { get; set; }

        public virtual List<Review> Reviews { get; set; }
        public virtual List<Showing> Showings { get; set; }
        public virtual List<Genre> Genres { get; set; }

        public Movie()
        {
            if (Reviews == null)
            {
                Reviews = new List<Review>();
            }
            if (Showings == null)
            {
                Showings = new List<Showing>();
            }
            if (Genres == null)
            {
                Genres = new List<Genre>();
            }
        }
    }
}
