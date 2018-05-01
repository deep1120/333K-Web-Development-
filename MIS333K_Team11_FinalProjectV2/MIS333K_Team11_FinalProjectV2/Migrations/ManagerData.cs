using MIS333K_Team11_FinalProjectV2.Models;
//using MIS333K_Team11_FinalProjectV2.DAL;
using static MIS333K_Team11_FinalProjectV2.Models.AppUser;
using System.Data.Entity.Migrations;
using System;
using System.Linq;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace MIS333K_Team11_FinalProjectV2.Migrations
{
	public class ManagerData
	{

		public void AddManager(AppDbContext db)
		{

			AppUserManager UserManager = new AppUserManager(new UserStore<AppUser>(db));
			AppRoleManager RoleManager = new AppRoleManager(new RoleStore<AppRole>(db));
			AppUser Manager1 = db.Users.FirstOrDefault(u => u.Email == "e.rice@longhorncinema.com");

			if (Manager1 == null)
			{
				Manager1 = new AppUser();
				Manager1.UserName = "e.rice@longhorncinema.com";
				Manager1.Email = "e.rice@longhorncinema.com";
				Manager1.FirstName = "Eryn";
				Manager1.LastName = "Rice";
				Manager1.MiddleInitial = "M";
				Manager1.PhoneNumber = "9073876657";
				Manager1.Birthday = new DateTime(1959, 7, 2);
				Manager1.Street = "3405 Rio Grande";
				Manager1.City = "Austin";
				Manager1.State = StateAbbr.TX;
				Manager1.ZipCode = "78746";

				var result = UserManager.Create(Manager1,"ricearoni");
				db.SaveChanges();
				Manager1 = db.Users.First(u => u.UserName == "e.rice@longhorncinema.com");

			}

			if (RoleManager.RoleExists("Manager") == false)
			{
				RoleManager.Create(new AppRole("Manager"));
			}

			if (UserManager.IsInRole(Manager1.Id, "Manager") == false)
			{
				UserManager.AddToRole(Manager1.Id, "Manager");
			}

			db.SaveChanges();

			AppUser Manager2 = db.Users.FirstOrDefault(u => u.Email == "r.taylor@longhorncinema.com");

			if (Manager2 == null)
			{
				Manager2 = new AppUser();
				Manager2.UserName = "r.taylor@longhorncinema.com";
				Manager2.Email = "r.taylor@longhorncinema.com";
				Manager2.FirstName = "Rachel";
				Manager2.LastName = "Taylor";
				Manager2.MiddleInitial = "O";
				Manager2.PhoneNumber = "9074512631";
				Manager2.Birthday = new DateTime(1972, 12, 20);
				Manager2.Street = "345 Longview Dr.";
				Manager2.City = "Austin";
				Manager2.State = StateAbbr.TX;
				Manager2.ZipCode = "78746";

				var result = UserManager.Create(Manager2,"swansong");
				db.SaveChanges();
				Manager2 = db.Users.First(u => u.UserName == "r.taylor@longhorncinema.com");

			}

			if (RoleManager.RoleExists("Manager") == false)
			{
				RoleManager.Create(new AppRole("Manager"));
			}

			if (UserManager.IsInRole(Manager2.Id, "Manager") == false)
			{
				UserManager.AddToRole(Manager2.Id, "Manager");
			}

			db.SaveChanges();

			AppUser Manager3 = db.Users.FirstOrDefault(u => u.Email == "a.rogers@longhorncinema.com");

			if (Manager3 == null)
			{
				Manager3 = new AppUser();
				Manager3.UserName = "a.rogers@longhorncinema.com";
				Manager3.Email = "a.rogers@longhorncinema.com";
				Manager3.FirstName = "Allen";
				Manager3.LastName = "Rogers";
				Manager3.MiddleInitial = "H";
				Manager3.PhoneNumber = "9078752943";
				Manager3.Birthday = new DateTime(1978, 6, 21);
				Manager3.Street = "4965 Oak Hill";
				Manager3.City = "Austin";
				Manager3.State = StateAbbr.TX;
				Manager3.ZipCode = "78705";

				var result = UserManager.Create(Manager3,"evanescent");
				db.SaveChanges();
				Manager3 = db.Users.First(u => u.UserName == "a.rogers@longhorncinema.com");

			}

			if (RoleManager.RoleExists("Manager") == false)
			{
				RoleManager.Create(new AppRole("Manager"));
			}

			if (UserManager.IsInRole(Manager3.Id, "Manager") == false)
			{
				UserManager.AddToRole(Manager3.Id, "Manager");
			}

			db.SaveChanges();

			AppUser Manager4 = db.Users.FirstOrDefault(u => u.Email == "w.sewell@longhorncinema.com");

			if (Manager4 == null)
			{
				Manager4 = new AppUser();
				Manager4.UserName = "w.sewell@longhorncinema.com";
				Manager4.Email = "w.sewell@longhorncinema.com";
				Manager4.FirstName = "William";
				Manager4.LastName = "Sewell";
				Manager4.MiddleInitial = "G";
				Manager4.PhoneNumber = "9074510084";
				Manager4.Birthday = new DateTime(1986, 5, 25);
				Manager4.Street = "2365 51st St.";
				Manager4.City = "Austin";
				Manager4.State = StateAbbr.TX;
				Manager4.ZipCode = "78755";

				var result = UserManager.Create(Manager4,"walkamile");
				db.SaveChanges();
				Manager4 = db.Users.First(u => u.UserName == "w.sewell@longhorncinema.com");

			}

			if (RoleManager.RoleExists("Manager") == false)
			{
				RoleManager.Create(new AppRole("Manager"));
			}

			if (UserManager.IsInRole(Manager4.Id, "Manager") == false)
			{
				UserManager.AddToRole(Manager4.Id, "Manager");
			}

			db.SaveChanges();

	

		}
	}
}
