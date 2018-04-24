using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Linq;

// Change these using statements to match your project
using MIS333K_Team11_FinalProjectV2.DAL;
using MIS333K_Team11_FinalProjectV2.Models;
using System;

// Change this namespace to match your project
namespace MIS333K_Team11_FinalProjectV2.Migrations
{
    //add identity data
    public class AddCustomer
    {
        public void AddingCustomer(AppDbContext db)
        {
            //create a user manager and a role manager to use for this method
            AppUserManager UserManager = new AppUserManager(new UserStore<AppUser>(db));

            //create a role manager
            AppRoleManager RoleManager = new AppRoleManager(new RoleStore<AppRole>(db));

            //check to see if the manager has been added
            AppUser manager = db.Users.FirstOrDefault(u => u.Email == "cbaker@example.com");

            //if manager hasn't been created, then add them
            if (manager == null)
            {
                //TODO: Add any additional fields for user here
                manager = new AppUser();
                manager.UserName = "cbaker@example.com";
                manager.FirstName = "Christopher";
                manager.LastName = "Baker";
                manager.MiddleInitial = "L";
                manager.Birthday = new DateTime(1949, 11, 23);
                manager.PhoneNumber = "(512)5550180";
                manager.Street = "1245 Lake Anchorage Blvd.";
                manager.City = "Austin";
                manager.State = "TX";
                manager.ZipCode = "78705";

                var result = UserManager.Create(manager, "hello1");
                db.SaveChanges();
                manager = db.Users.First(u => u.UserName == "cbaker@example.com");
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