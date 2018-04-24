using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

// Change this using statement to match your project
using MIS333K_Team11_FinalProjectV2.Models;


// Change this namespace to match your project
namespace MIS333K_Team11_FinalProjectV2.DAL
{
    // NOTE: Here's your db context for the project.  All of your db sets should go in here
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        // Make sure that your connection string name is correct here.
        public AppDbContext()
            : base("MyDBConnection", throwIfV1Schema: false) { }

        public static AppDbContext Create()
        {
            return new AppDbContext();
        }

        // Add dbsets here. Remember, Identity adds a db set for users, 
        //so you shouldn't add that one - you will get an error
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Order> Orders { get; set; }
        //public DbSet<Order> Purchased { get; set; }
        //public DbSet<Order> Gifted { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Showing> Showings { get; set; }
        public DbSet<Ticket> Tickets { get; set; }


        //NOTE: This is a dbSet that you need to make roles work
        public DbSet<AppRole> AppRoles { get; set; }
    }
}