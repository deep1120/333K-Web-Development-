using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MIS333K_Team11_FinalProjectV2.Models
{
    public class Review
    {
        public Int32 ReviewID { get; set; }

        [StringLength(100, ErrorMessage = "Reviews cannot exceed 100 characters.")]
        public String Comment { get; set; }

        [Required(ErrorMessage = "Please rate before reviewing")]
        [Display(Name = "Star Rating")]
        [Range(1,5,ErrorMessage ="Rate from 1-5")]
        public Int32 StarRating { get; set; }

        public virtual Movie MovieReview { get; set; } //each review is for one movie 
        public virtual AppUser AppUser { get; set; }
    }
}

        //will probably not need to store in enum
        //customer voting will be like a list of storing the counts
        //this is low priority thing -- focus on this at the end bc she only anticipates a few teams getting this
        //will possibly have to keep track of another object bc each customer is an object
        //[Display(Name = "Customer Voting")] 
        //public CustomerVoting Customervoting { get; set; }
