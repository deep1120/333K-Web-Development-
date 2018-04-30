using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MIS333K_Team11_FinalProjectV2.Models
{
    public class Rating
    {
        public Int32 RatingID { get; set; }
        [Required(ErrorMessage = "Please enter a rating")]
        public Int32 RatingScore { get; set; }

        public virtual List<Movie> Movies { get; set; }
        public virtual List<Review> Reviews { get; set; }

        public Rating()
        {
            if (Movies == null)
            {
                Movies = new List<Movie>();
            }
            if (Reviews == null)
            {
                Reviews = new List<Review>();
            }      
        }
    }
}