using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Linq;

// Change these using statements to match your project
//using MIS333K_Team11_FinalProjectV2.DAL;
using MIS333K_Team11_FinalProjectV2.Models;
using System;
using static MIS333K_Team11_FinalProjectV2.Models.AppUser;

// Change this namespace to match your project
namespace MIS333K_Team11_FinalProjectV2.Migrations
{
    //add identity data
    public class SeedIdentity
    {
        public void AddAdmin(AppDbContext db)
        {
            //create a user manager and a role manager to use for this method
            AppUserManager UserManager = new AppUserManager(new UserStore<AppUser>(db));

            //create a role manager
            AppRoleManager RoleManager = new AppRoleManager(new RoleStore<AppRole>(db));

            //check to see if the manager has been added
            AppUser manager = db.Users.FirstOrDefault(u => u.Email == "admin@example.com");

            //if manager hasn't been created, then add them
            if (manager == null)
            {
                //TODO: Add any additional fields for user here
                manager = new AppUser();
                manager.UserName = "admin@example.com";
                manager.FirstName = "AdminFirstName";
                manager.LastName = "AdminLastName";
                manager.Birthday = new DateTime(1990,01,01);
                manager.PhoneNumber = "(512)555-5555";
                manager.PopcornPoints = 0;

                var result = UserManager.Create(manager, "Abc123!");
                db.SaveChanges();
                manager = db.Users.First(u => u.UserName == "admin@example.com");
            }

            //TODO: Add the needed roles
            //if role doesn't exist, add it
            if (RoleManager.RoleExists("Manager") == false)
            {
                RoleManager.Create(new AppRole("Manager"));
            }

            if (RoleManager.RoleExists("Customer") == false)
            {
                RoleManager.Create(new AppRole("Customer"));
            }

            //make sure user is in role
            if (UserManager.IsInRole(manager.Id, "Manager") == false)
            {
                UserManager.AddToRole(manager.Id, "Manager");
            }

            //save changes
            db.SaveChanges();
        }
    }
}