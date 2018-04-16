using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MIS333K_Team11_FinalProjectV2.Models
{
    public class Genre
    {
        public Int32 GenreID { get; set; }

        public String GenreName { get; set; }

        //Navigational Property
        public virtual List<Movie> Movies { get; set; }

        public Genre()
        {
            if (Movies == null)
            {
                Movies = new List<Movie>();
            }
        }
    }
}
