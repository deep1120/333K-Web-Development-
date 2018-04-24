using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;

namespace MIS333K_Team11_FinalProjectV2.Models
{
    public class AppUser : IdentityUser
    {
        //testing comment to see update on github
        //NEED TO ADD CREDIT CARD IN THE MODEL CLASS

        //will inherit from identity user
        //public Int32 UserID { get; set; } //Is the primary key email or UserID, if Email add in [key]

        ////[Required(ErrorMessage = "Email is required")]
        ////[Display(Name = "Email")]
        //public String Email { get; set; }

        ////[Display(Name = "Password")]
        //public String Password { get; set; }

        [Display(Name = "First Name")]
        public String FirstName { get; set; }

        [Display(Name = "Last Name")]
        public String LastName { get; set; }

        [Display(Name = "Middle Initial")]
        public String MiddleInitital { get; set; }

        [Display(Name = "Birthday")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime Birthday { get; set; }

        [Display(Name = "Street")]
        public String Street { get; set; }

        [Display(Name = "City")]
        public String City { get; set; }

        //would state be an enum then?
        [Display(Name = "State")] //add validation must be a state in the US if chosen US
        public String State { get; set; }

        [Display(Name = "Zip Code")] //add validation XX values
        public String ZipCode { get; set; }

        [Display(Name = "Popcorn Points")]
        public Int32 PopcornPoints { get; set; }

        public virtual List<Review> Reviews { get; set; }
        public virtual List<Order> Purchased { get; set; }
        public virtual List<Order> Gifted { get; set; }

        public AppUser() //Reference to HW 6
        {
            //will check to see if list is blank or not
            if (Reviews == null)
            {
                Reviews = new List<Review>();
            }
            if (Purchased == null)
            {
                Purchased = new List<Order>();
            }
            if (Gifted == null)
            {
                Gifted = new List<Order>();
            }

        }

        //This method allows you to create a new user
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<AppUser> manager)
        {
            // NOTE: The authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }
}
