using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace MIS333K_Team11_FinalProjectV2.Models
{
    //public enum CustomerVoting  //not needed
    //{
       
    //}
    public class Review
    {
        public Int32 ReviewID { get; set; }

        [Display(Name = "Star Rating")]
        public Int32 StarRating { get; set; }

        //will probably not need to store in enum
        //customer voting will be like a list of storing the counts
        //this is low priority thing -- focus on this at the end bc she only anticipates a few teams getting this
        //will possibly have to keep track of another object bc each customer is an object
        //[Display(Name = "Customer Voting")] 
        //public CustomerVoting Customervoting { get; set; }

        public virtual User User { get; set; }
        public virtual Movie Movie { get; set; }

        //public Review()
        //{
        //    if (Movie == null)
        //    {
        //        Movie = new List<Movie>();
        //    }
        //}

    }
}
