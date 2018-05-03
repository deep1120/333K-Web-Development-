using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;

namespace MIS333K_Team11_FinalProjectV2.Models
{
    public class AppUser : IdentityUser
    {
        [Required]
        [Display(Name = "First Name")]
        public String FirstName { get; set; }

        [Display(Name = "Middle Initial")]
        public String MiddleInitial { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public String LastName { get; set; }

        [Required]
        [Display(Name = "Street")]
        public String Street { get; set; }

        [Required]
        [Display(Name = "City")]
        public String City { get; set; }

        [Required]
        [Display(Name = "State")]
        public StateAbbr State { get; set; }

        [Required]
        [Display(Name = "Zip Code")] 
        public String ZipCode { get; set; }

        [Required]
        [Display(Name = "Birthday")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime Birthday { get; set; }

        [Display(Name = "Popcorn Points")]
        public Int32 PopcornPoints { get; set; }

        public virtual List<Review> Reviews { get; set; }
        public virtual List<Order> Orders { get; set; }
        public virtual List<Card> Cards { get; set; }

        public AppUser() 
        {
            if (Reviews == null)
            {
                Reviews = new List<Review>();
            }      
            if (Orders == null)
            {
                Orders = new List<Order>();
            }
            if(Cards == null)
            {
                Cards = new List<Card>();
            }
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<AppUser> manager)
        {
            // NOTE: The authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

        // NOTE: Here's your db context for the project.  All of your db sets should go in here
        public class AppDbContext : IdentityDbContext<AppUser>
        {
            public override int SaveChanges()
            {
                try
                {
                    return base.SaveChanges();
                }
                catch (DbEntityValidationException ex)
                {
                    // Retrieve the error messages as a list of strings.
                    var errorMessages = ex.EntityValidationErrors
                            .SelectMany(x => x.ValidationErrors)
                            .Select(x => x.ErrorMessage);

                    // Join the list to a single string.
                    var fullErrorMessage = string.Join("; ", errorMessages);

                    // Combine the original exception message with the new one.
                    var exceptionMessage = string.Concat(ex.Message, " The validation errors are: ", fullErrorMessage);

                    // Throw a new DbEntityValidationException with the improved exception message.
                    throw new DbEntityValidationException(exceptionMessage, ex.EntityValidationErrors);
                }
            }

            // Add dbsets here. Remember, Identity adds a db set for users, 
            //so you shouldn't add that one - you will get an error
            public DbSet<Genre> Genres { get; set; }
            public DbSet<Movie> Movies { get; set; }
            public DbSet<Order> Orders { get; set; }
            public DbSet<Review> Reviews { get; set; }
            public DbSet<Showing> Showings { get; set; }
            public DbSet<Ticket> Tickets { get; set; }
            public DbSet<Card> Cards { get; set; }
            //NOTE: This is a dbSet that you need to make roles work
            public DbSet<AppRole> AppRoles { get; set; }

            public AppDbContext()
                : base("MyDbConnection", throwIfV1Schema: false)
            {
            }

            public static AppDbContext Create()
            {
                return new AppDbContext();
            }

            //public System.Data.Entity.DbSet<MIS333K_Team11_FinalProjectV2.Models.AppUser> AppUsers { get; set; }
        }
    }
}

