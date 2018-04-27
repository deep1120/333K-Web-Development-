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
    public class EmployeeTest
    {
        public void AddCustomer1(AppDbContext db)
        {
            //create a user manager and a role manager to use for this method
            AppUserManager UserManager = new AppUserManager(new UserStore<AppUser>(db));

            //create a role manager
            AppRoleManager RoleManager = new AppRoleManager(new RoleStore<AppRole>(db));

            //check to see if the manager has been added
            AppUser employee = db.Users.FirstOrDefault(u => u.Email == "Employee@example.com");

            //if manager hasn't been created, then add them
            if (employee == null)
            {
                //TODO: Add any additional fields for user here
                employee = new AppUser();
                employee.UserName = "employee@example.com";
                employee.FirstName = "employeeFirstName";
                employee.LastName = "employeeLastName";
                employee.Birthday = new DateTime(1991, 1, 1);
                employee.PhoneNumber = "(512)555-5555";
                employee.MiddleInitial = "L";
                employee.Street = "1245 Lake Anchorage Blvd.";
                employee.City = "Austin";
                employee.State = StateAbbr.TX;
                employee.ZipCode = "78705";

                var result = UserManager.Create(employee, "Abc123!");
                db.SaveChanges();
                employee = db.Users.First(u => u.UserName == "employee@example.com");
            }
            if (RoleManager.RoleExists("Employee") == false)
            {
                RoleManager.Create(new AppRole("Employee"));
            }

            //make sure user is in role
            if (UserManager.IsInRole(employee.Id, "Employee") == false)
            {
                UserManager.AddToRole(employee.Id, "Customer");
            }

            //save changes
            db.SaveChanges();
        }
    }
}
