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
	public class EmployeeData
	{

		public void AddEmployee(AppDbContext db)
		{

			AppUserManager UserManager = new AppUserManager(new UserStore<AppUser>(db));
			AppRoleManager RoleManager = new AppRoleManager(new RoleStore<AppRole>(db));
			AppUser employee1 = db.Users.FirstOrDefault(u => u.Email == "t.jacobs@longhorncinema.com");

			if (employee1 == null)
			{
				employee1 = new AppUser();
				employee1.UserName = "t.jacobs@longhorncinema.com";
				employee1.Email = "t.jacobs@longhorncinema.com";
				employee1.FirstName = "Todd";
				employee1.LastName = "Jacobs";
				employee1.MiddleInitial = "L";
				employee1.PhoneNumber = "9074653365";
				employee1.Birthday = new DateTime(4/25/1958);
				employee1.Street = "4564 Elm St.";
				employee1.City = "Georgetown";
				employee1.State = StateAbbr.TX;
				employee1.ZipCode = "78628";

				var result = UserManager.Create(employee1,"society");
				db.SaveChanges();
				employee1 = db.Users.First(u => u.UserName == "t.jacobs@longhorncinema.com");

			}

			if (RoleManager.RoleExists("Employee") == false)
			{
				RoleManager.Create(new AppRole("Employee"));
			}

			if (UserManager.IsInRole(employee1.Id, "Employee") == false)
			{
				UserManager.AddToRole(employee1.Id, "Employee");
			}

			db.SaveChanges();

			AppUser employee2 = db.Users.FirstOrDefault(u => u.Email == "e.rice@longhorncinema.com");

			if (employee2 == null)
			{
				employee2 = new AppUser();
				employee2.UserName = "e.rice@longhorncinema.com";
				employee2.Email = "e.rice@longhorncinema.com";
				employee2.FirstName = "Eryn";
				employee2.LastName = "Rice";
				employee2.MiddleInitial = "M";
				employee2.PhoneNumber = "9073876657";
				employee2.Birthday = new DateTime(7/2/1959);
				employee2.Street = "3405 Rio Grande";
				employee2.City = "Austin";
				employee2.State = StateAbbr.TX;
				employee2.ZipCode = "78746";

				var result = UserManager.Create(employee2,"ricearoni");
				db.SaveChanges();
				employee2 = db.Users.First(u => u.UserName == "e.rice@longhorncinema.com");

			}

			if (RoleManager.RoleExists("Employee") == false)
			{
				RoleManager.Create(new AppRole("Employee"));
			}

			if (UserManager.IsInRole(employee2.Id, "Employee") == false)
			{
				UserManager.AddToRole(employee2.Id, "Employee");
			}

			db.SaveChanges();

			AppUser employee3 = db.Users.FirstOrDefault(u => u.Email == "b.ingram@longhorncinema.com");

			if (employee3 == null)
			{
				employee3 = new AppUser();
				employee3.UserName = "b.ingram@longhorncinema.com";
				employee3.Email = "b.ingram@longhorncinema.com";
				employee3.FirstName = "Brad";
				employee3.LastName = "Ingram";
				employee3.MiddleInitial = "S";
				employee3.PhoneNumber = "9074678821";
				employee3.Birthday = new DateTime(8/25/1962);
				employee3.Street = "6548 La Posada Ct.";
				employee3.City = "Austin";
				employee3.State = StateAbbr.TX;
				employee3.ZipCode = "78705";

				var result = UserManager.Create(employee3,"ingram45");
				db.SaveChanges();
				employee3 = db.Users.First(u => u.UserName == "b.ingram@longhorncinema.com");

			}

			if (RoleManager.RoleExists("Employee") == false)
			{
				RoleManager.Create(new AppRole("Employee"));
			}

			if (UserManager.IsInRole(employee3.Id, "Employee") == false)
			{
				UserManager.AddToRole(employee3.Id, "Employee");
			}

			db.SaveChanges();

			AppUser employee4 = db.Users.FirstOrDefault(u => u.Email == "a.taylor@longhorncinema.com");

			if (employee4 == null)
			{
				employee4 = new AppUser();
				employee4.UserName = "a.taylor@longhorncinema.com";
				employee4.Email = "a.taylor@longhorncinema.com";
				employee4.FirstName = "Allison";
				employee4.LastName = "Taylor";
				employee4.MiddleInitial = "R";
				employee4.PhoneNumber = "9074748452";
				employee4.Birthday = new DateTime(9/2/1964);
				employee4.Street = "467 Nueces St.";
				employee4.City = "Austin";
				employee4.State = StateAbbr.TX;
				employee4.ZipCode = "78727";

				var result = UserManager.Create(employee4,"nostalgic");
				db.SaveChanges();
				employee4 = db.Users.First(u => u.UserName == "a.taylor@longhorncinema.com");

			}

			if (RoleManager.RoleExists("Employee") == false)
			{
				RoleManager.Create(new AppRole("Employee"));
			}

			if (UserManager.IsInRole(employee4.Id, "Employee") == false)
			{
				UserManager.AddToRole(employee4.Id, "Employee");
			}

			db.SaveChanges();

			AppUser employee5 = db.Users.FirstOrDefault(u => u.Email == "g.martinez@longhorncinema.com");

			if (employee5 == null)
			{
				employee5 = new AppUser();
				employee5.UserName = "g.martinez@longhorncinema.com";
				employee5.Email = "g.martinez@longhorncinema.com";
				employee5.FirstName = "Gregory";
				employee5.LastName = "Martinez";
				employee5.MiddleInitial = "R";
				employee5.PhoneNumber = "9078746718";
				employee5.Birthday = new DateTime(3/30/1992);
				employee5.Street = "8295 Sunset Blvd.";
				employee5.City = "Austin";
				employee5.State = StateAbbr.TX;
				employee5.ZipCode = "78712";

				var result = UserManager.Create(employee5,"fungus");
				db.SaveChanges();
				employee5 = db.Users.First(u => u.UserName == "g.martinez@longhorncinema.com");

			}

			if (RoleManager.RoleExists("Employee") == false)
			{
				RoleManager.Create(new AppRole("Employee"));
			}

			if (UserManager.IsInRole(employee5.Id, "Employee") == false)
			{
				UserManager.AddToRole(employee5.Id, "Employee");
			}

			db.SaveChanges();

			AppUser employee6 = db.Users.FirstOrDefault(u => u.Email == "m.sheffield@longhorncinema.com");

			if (employee6 == null)
			{
				employee6 = new AppUser();
				employee6.UserName = "m.sheffield@longhorncinema.com";
				employee6.Email = "m.sheffield@longhorncinema.com";
				employee6.FirstName = "Martin";
				employee6.LastName = "Sheffield";
				employee6.MiddleInitial = "J";
				employee6.PhoneNumber = "9075479167";
				employee6.Birthday = new DateTime(12/29/1996);
				employee6.Street = "3886 Avenue A";
				employee6.City = "San Marcos";
				employee6.State = StateAbbr.TX;
				employee6.ZipCode = "78666";

				var result = UserManager.Create(employee6,"longhorns");
				db.SaveChanges();
				employee6 = db.Users.First(u => u.UserName == "m.sheffield@longhorncinema.com");

			}

			if (RoleManager.RoleExists("Employee") == false)
			{
				RoleManager.Create(new AppRole("Employee"));
			}

			if (UserManager.IsInRole(employee6.Id, "Employee") == false)
			{
				UserManager.AddToRole(employee6.Id, "Employee");
			}

			db.SaveChanges();

			AppUser employee7 = db.Users.FirstOrDefault(u => u.Email == "j.macleod@longhorncinema.com");

			if (employee7 == null)
			{
				employee7 = new AppUser();
				employee7.UserName = "j.macleod@longhorncinema.com";
				employee7.Email = "j.macleod@longhorncinema.com";
				employee7.FirstName = "Jennifer";
				employee7.LastName = "MacLeod";
				employee7.MiddleInitial = "D";
				employee7.PhoneNumber = "9074748138";
				employee7.Birthday = new DateTime(6/10/1997);
				employee7.Street = "2504 Far West Blvd.";
				employee7.City = "Austin";
				employee7.State = StateAbbr.TX;
				employee7.ZipCode = "78705";

				var result = UserManager.Create(employee7,"smitty");
				db.SaveChanges();
				employee7 = db.Users.First(u => u.UserName == "j.macleod@longhorncinema.com");

			}

			if (RoleManager.RoleExists("Employee") == false)
			{
				RoleManager.Create(new AppRole("Employee"));
			}

			if (UserManager.IsInRole(employee7.Id, "Employee") == false)
			{
				UserManager.AddToRole(employee7.Id, "Employee");
			}

			db.SaveChanges();

			AppUser employee8 = db.Users.FirstOrDefault(u => u.Email == "j.tanner@longhorncinema.com");

			if (employee8 == null)
			{
				employee8 = new AppUser();
				employee8.UserName = "j.tanner@longhorncinema.com";
				employee8.Email = "j.tanner@longhorncinema.com";
				employee8.FirstName = "Jeremy";
				employee8.LastName = "Tanner";
				employee8.MiddleInitial = "S";
				employee8.PhoneNumber = "9074590929";
				employee8.Birthday = new DateTime(8/12/1970);
				employee8.Street = "4347 Almstead";
				employee8.City = "Austin";
				employee8.State = StateAbbr.TX;
				employee8.ZipCode = "78712";

				var result = UserManager.Create(employee8,"tanman");
				db.SaveChanges();
				employee8 = db.Users.First(u => u.UserName == "j.tanner@longhorncinema.com");

			}

			if (RoleManager.RoleExists("Employee") == false)
			{
				RoleManager.Create(new AppRole("Employee"));
			}

			if (UserManager.IsInRole(employee8.Id, "Employee") == false)
			{
				UserManager.AddToRole(employee8.Id, "Employee");
			}

			db.SaveChanges();

			AppUser employee9 = db.Users.FirstOrDefault(u => u.Email == "m.rhodes@longhorncinema.com");

			if (employee9 == null)
			{
				employee9 = new AppUser();
				employee9.UserName = "m.rhodes@longhorncinema.com";
				employee9.Email = "m.rhodes@longhorncinema.com";
				employee9.FirstName = "Megan";
				employee9.LastName = "Rhodes";
				employee9.MiddleInitial = "C";
				employee9.PhoneNumber = "9073744746";
				employee9.Birthday = new DateTime(12/18/1970);
				employee9.Street = "4587 Enfield Rd.";
				employee9.City = "Austin";
				employee9.State = StateAbbr.TX;
				employee9.ZipCode = "78729";

				var result = UserManager.Create(employee9,"countryrhodes");
				db.SaveChanges();
				employee9 = db.Users.First(u => u.UserName == "m.rhodes@longhorncinema.com");

			}

			if (RoleManager.RoleExists("Employee") == false)
			{
				RoleManager.Create(new AppRole("Employee"));
			}

			if (UserManager.IsInRole(employee9.Id, "Employee") == false)
			{
				UserManager.AddToRole(employee9.Id, "Employee");
			}

			db.SaveChanges();

			AppUser employee10 = db.Users.FirstOrDefault(u => u.Email == "e.stuart@longhorncinema.com");

			if (employee10 == null)
			{
				employee10 = new AppUser();
				employee10.UserName = "e.stuart@longhorncinema.com";
				employee10.Email = "e.stuart@longhorncinema.com";
				employee10.FirstName = "Eric";
				employee10.LastName = "Stuart";
				employee10.MiddleInitial = "F";
				employee10.PhoneNumber = "9078178335";
				employee10.Birthday = new DateTime(3/11/1971);
				employee10.Street = "5576 Toro Ring";
				employee10.City = "Austin";
				employee10.State = StateAbbr.TX;
				employee10.ZipCode = "78758";

				var result = UserManager.Create(employee10,"stewboy");
				db.SaveChanges();
				employee10 = db.Users.First(u => u.UserName == "e.stuart@longhorncinema.com");

			}

			if (RoleManager.RoleExists("Employee") == false)
			{
				RoleManager.Create(new AppRole("Employee"));
			}

			if (UserManager.IsInRole(employee10.Id, "Employee") == false)
			{
				UserManager.AddToRole(employee10.Id, "Employee");
			}

			db.SaveChanges();

			AppUser employee11 = db.Users.FirstOrDefault(u => u.Email == "c.miller@longhorncinema.com");

			if (employee11 == null)
			{
				employee11 = new AppUser();
				employee11.UserName = "c.miller@longhorncinema.com";
				employee11.Email = "c.miller@longhorncinema.com";
				employee11.FirstName = "Charles";
				employee11.LastName = "Miller";
				employee11.MiddleInitial = "R";
				employee11.PhoneNumber = "9077458615";
				employee11.Birthday = new DateTime(7/20/1972);
				employee11.Street = "8962 Main St.";
				employee11.City = "Austin";
				employee11.State = StateAbbr.TX;
				employee11.ZipCode = "78709";

				var result = UserManager.Create(employee11,"squirrel");
				db.SaveChanges();
				employee11 = db.Users.First(u => u.UserName == "c.miller@longhorncinema.com");

			}

			if (RoleManager.RoleExists("Employee") == false)
			{
				RoleManager.Create(new AppRole("Employee"));
			}

			if (UserManager.IsInRole(employee11.Id, "Employee") == false)
			{
				UserManager.AddToRole(employee11.Id, "Employee");
			}

			db.SaveChanges();

			AppUser employee12 = db.Users.FirstOrDefault(u => u.Email == "r.taylor@longhorncinema.com");

			if (employee12 == null)
			{
				employee12 = new AppUser();
				employee12.UserName = "r.taylor@longhorncinema.com";
				employee12.Email = "r.taylor@longhorncinema.com";
				employee12.FirstName = "Rachel";
				employee12.LastName = "Taylor";
				employee12.MiddleInitial = "O";
				employee12.PhoneNumber = "9074512631";
				employee12.Birthday = new DateTime(12/20/1972);
				employee12.Street = "345 Longview Dr.";
				employee12.City = "Austin";
				employee12.State = StateAbbr.TX;
				employee12.ZipCode = "78746";

				var result = UserManager.Create(employee12,"swansong");
				db.SaveChanges();
				employee12 = db.Users.First(u => u.UserName == "r.taylor@longhorncinema.com");

			}

			if (RoleManager.RoleExists("Employee") == false)
			{
				RoleManager.Create(new AppRole("Employee"));
			}

			if (UserManager.IsInRole(employee12.Id, "Employee") == false)
			{
				UserManager.AddToRole(employee12.Id, "Employee");
			}

			db.SaveChanges();

			AppUser employee13 = db.Users.FirstOrDefault(u => u.Email == "v.lawrence@longhorncinema.com");

			if (employee13 == null)
			{
				employee13 = new AppUser();
				employee13.UserName = "v.lawrence@longhorncinema.com";
				employee13.Email = "v.lawrence@longhorncinema.com";
				employee13.FirstName = "Victoria";
				employee13.LastName = "Lawrence";
				employee13.MiddleInitial = "Y";
				employee13.PhoneNumber = "9079457399";
				employee13.Birthday = new DateTime(4/28/1973);
				employee13.Street = "6639 Butterfly Ln.";
				employee13.City = "Austin";
				employee13.State = StateAbbr.TX;
				employee13.ZipCode = "78712";

				var result = UserManager.Create(employee13,"lottery");
				db.SaveChanges();
				employee13 = db.Users.First(u => u.UserName == "v.lawrence@longhorncinema.com");

			}

			if (RoleManager.RoleExists("Employee") == false)
			{
				RoleManager.Create(new AppRole("Employee"));
			}

			if (UserManager.IsInRole(employee13.Id, "Employee") == false)
			{
				UserManager.AddToRole(employee13.Id, "Employee");
			}

			db.SaveChanges();

			AppUser employee14 = db.Users.FirstOrDefault(u => u.Email == "a.rogers@longhorncinema.com");

			if (employee14 == null)
			{
				employee14 = new AppUser();
				employee14.UserName = "a.rogers@longhorncinema.com";
				employee14.Email = "a.rogers@longhorncinema.com";
				employee14.FirstName = "Allen";
				employee14.LastName = "Rogers";
				employee14.MiddleInitial = "H";
				employee14.PhoneNumber = "9078752943";
				employee14.Birthday = new DateTime(6/21/1978);
				employee14.Street = "4965 Oak Hill";
				employee14.City = "Austin";
				employee14.State = StateAbbr.TX;
				employee14.ZipCode = "78705";

				var result = UserManager.Create(employee14,"evanescent");
				db.SaveChanges();
				employee14 = db.Users.First(u => u.UserName == "a.rogers@longhorncinema.com");

			}

			if (RoleManager.RoleExists("Employee") == false)
			{
				RoleManager.Create(new AppRole("Employee"));
			}

			if (UserManager.IsInRole(employee14.Id, "Employee") == false)
			{
				UserManager.AddToRole(employee14.Id, "Employee");
			}

			db.SaveChanges();

			AppUser employee15 = db.Users.FirstOrDefault(u => u.Email == "e.markham@longhorncinema.com");

			if (employee15 == null)
			{
				employee15 = new AppUser();
				employee15.UserName = "e.markham@longhorncinema.com";
				employee15.Email = "e.markham@longhorncinema.com";
				employee15.FirstName = "Elizabeth";
				employee15.LastName = "Markham";
				employee15.MiddleInitial = "K";
				employee15.PhoneNumber = "9074579845";
				employee15.Birthday = new DateTime(5/21/1990);
				employee15.Street = "7861 Chevy Chase";
				employee15.City = "Austin";
				employee15.State = StateAbbr.TX;
				employee15.ZipCode = "78785";

				var result = UserManager.Create(employee15,"monty3");
				db.SaveChanges();
				employee15 = db.Users.First(u => u.UserName == "e.markham@longhorncinema.com");

			}

			if (RoleManager.RoleExists("Employee") == false)
			{
				RoleManager.Create(new AppRole("Employee"));
			}

			if (UserManager.IsInRole(employee15.Id, "Employee") == false)
			{
				UserManager.AddToRole(employee15.Id, "Employee");
			}

			db.SaveChanges();

			AppUser employee16 = db.Users.FirstOrDefault(u => u.Email == "c.baker@longhorncinema.com");

			if (employee16 == null)
			{
				employee16 = new AppUser();
				employee16.UserName = "c.baker@longhorncinema.com";
				employee16.Email = "c.baker@longhorncinema.com";
				employee16.FirstName = "Christopher";
				employee16.LastName = "Baker";
				employee16.MiddleInitial = "E";
				employee16.PhoneNumber = "9075571146";
				employee16.Birthday = new DateTime(3/16/1993);
				employee16.Street = "1245 Lake Anchorage Blvd.";
				employee16.City = "Cedar Park";
				employee16.State = StateAbbr.TX;
				employee16.ZipCode = "78613";

				var result = UserManager.Create(employee16,"hecktour");
				db.SaveChanges();
				employee16 = db.Users.First(u => u.UserName == "c.baker@longhorncinema.com");

			}

			if (RoleManager.RoleExists("Employee") == false)
			{
				RoleManager.Create(new AppRole("Employee"));
			}

			if (UserManager.IsInRole(employee16.Id, "Employee") == false)
			{
				UserManager.AddToRole(employee16.Id, "Employee");
			}

			db.SaveChanges();

			AppUser employee17 = db.Users.FirstOrDefault(u => u.Email == "s.saunders@longhorncinema.com");

			if (employee17 == null)
			{
				employee17 = new AppUser();
				employee17.UserName = "s.saunders@longhorncinema.com";
				employee17.Email = "s.saunders@longhorncinema.com";
				employee17.FirstName = "Sarah";
				employee17.LastName = "Saunders";
				employee17.MiddleInitial = "M";
				employee17.PhoneNumber = "9073497810";
				employee17.Birthday = new DateTime(1/5/1997);
				employee17.Street = "332 Avenue C";
				employee17.City = "Austin";
				employee17.State = StateAbbr.TX;
				employee17.ZipCode = "78733";

				var result = UserManager.Create(employee17,"rankmary");
				db.SaveChanges();
				employee17 = db.Users.First(u => u.UserName == "s.saunders@longhorncinema.com");

			}

			if (RoleManager.RoleExists("Employee") == false)
			{
				RoleManager.Create(new AppRole("Employee"));
			}

			if (UserManager.IsInRole(employee17.Id, "Employee") == false)
			{
				UserManager.AddToRole(employee17.Id, "Employee");
			}

			db.SaveChanges();

			AppUser employee18 = db.Users.FirstOrDefault(u => u.Email == "w.sewell@longhorncinema.com");

			if (employee18 == null)
			{
				employee18 = new AppUser();
				employee18.UserName = "w.sewell@longhorncinema.com";
				employee18.Email = "w.sewell@longhorncinema.com";
				employee18.FirstName = "William";
				employee18.LastName = "Sewell";
				employee18.MiddleInitial = "G";
				employee18.PhoneNumber = "9074510084";
				employee18.Birthday = new DateTime(5/25/1986);
				employee18.Street = "2365 51st St.";
				employee18.City = "Austin";
				employee18.State = StateAbbr.TX;
				employee18.ZipCode = "78755";

				var result = UserManager.Create(employee18,"walkamile");
				db.SaveChanges();
				employee18 = db.Users.First(u => u.UserName == "w.sewell@longhorncinema.com");

			}

			if (RoleManager.RoleExists("Employee") == false)
			{
				RoleManager.Create(new AppRole("Employee"));
			}

			if (UserManager.IsInRole(employee18.Id, "Employee") == false)
			{
				UserManager.AddToRole(employee18.Id, "Employee");
			}

			db.SaveChanges();

			AppUser employee19 = db.Users.FirstOrDefault(u => u.Email == "j.mason@longhorncinema.com");

			if (employee19 == null)
			{
				employee19 = new AppUser();
				employee19.UserName = "j.mason@longhorncinema.com";
				employee19.Email = "j.mason@longhorncinema.com";
				employee19.FirstName = "Jack";
				employee19.LastName = "Mason";
				employee19.MiddleInitial = "L";
				employee19.PhoneNumber = "9018833432";
				employee19.Birthday = new DateTime(6/6/1986);
				employee19.Street = "444 45th St";
				employee19.City = "Austin";
				employee19.State = StateAbbr.TX;
				employee19.ZipCode = "78701";

				var result = UserManager.Create(employee19,"changalang");
				db.SaveChanges();
				employee19 = db.Users.First(u => u.UserName == "j.mason@longhorncinema.com");

			}

			if (RoleManager.RoleExists("Employee") == false)
			{
				RoleManager.Create(new AppRole("Employee"));
			}

			if (UserManager.IsInRole(employee19.Id, "Employee") == false)
			{
				UserManager.AddToRole(employee19.Id, "Employee");
			}

			db.SaveChanges();

			AppUser employee20 = db.Users.FirstOrDefault(u => u.Email == "j.jackson@longhorncinema.com");

			if (employee20 == null)
			{
				employee20 = new AppUser();
				employee20.UserName = "j.jackson@longhorncinema.com";
				employee20.Email = "j.jackson@longhorncinema.com";
				employee20.FirstName = "Jack";
				employee20.LastName = "Jackson";
				employee20.MiddleInitial = "J";
				employee20.PhoneNumber = "9075554545";
				employee20.Birthday = new DateTime(10/16/1986);
				employee20.Street = "222 Main";
				employee20.City = "Austin";
				employee20.State = StateAbbr.TX;
				employee20.ZipCode = "78760";

				var result = UserManager.Create(employee20,"offbeat");
				db.SaveChanges();
				employee20 = db.Users.First(u => u.UserName == "j.jackson@longhorncinema.com");

			}

			if (RoleManager.RoleExists("Employee") == false)
			{
				RoleManager.Create(new AppRole("Employee"));
			}

			if (UserManager.IsInRole(employee20.Id, "Employee") == false)
			{
				UserManager.AddToRole(employee20.Id, "Employee");
			}

			db.SaveChanges();

			AppUser employee21 = db.Users.FirstOrDefault(u => u.Email == "m.nguyen@longhorncinema.com");

			if (employee21 == null)
			{
				employee21 = new AppUser();
				employee21.UserName = "m.nguyen@longhorncinema.com";
				employee21.Email = "m.nguyen@longhorncinema.com";
				employee21.FirstName = "Mary";
				employee21.LastName = "Nguyen";
				employee21.MiddleInitial = "J";
				employee21.PhoneNumber = "9075524141";
				employee21.Birthday = new DateTime(4/5/1988);
				employee21.Street = "465 N. Bear Cub";
				employee21.City = "Austin";
				employee21.State = StateAbbr.TX;
				employee21.ZipCode = "78734";

				var result = UserManager.Create(employee21,"landus");
				db.SaveChanges();
				employee21 = db.Users.First(u => u.UserName == "m.nguyen@longhorncinema.com");

			}

			if (RoleManager.RoleExists("Employee") == false)
			{
				RoleManager.Create(new AppRole("Employee"));
			}

			if (UserManager.IsInRole(employee21.Id, "Employee") == false)
			{
				UserManager.AddToRole(employee21.Id, "Employee");
			}

			db.SaveChanges();

			AppUser employee22 = db.Users.FirstOrDefault(u => u.Email == "s.barnes@longhorncinema.com");

			if (employee22 == null)
			{
				employee22 = new AppUser();
				employee22.UserName = "s.barnes@longhorncinema.com";
				employee22.Email = "s.barnes@longhorncinema.com";
				employee22.FirstName = "Susan";
				employee22.LastName = "Barnes";
				employee22.MiddleInitial = "M";
				employee22.PhoneNumber = "9556662323";
				employee22.Birthday = new DateTime(2/22/1993);
				employee22.Street = "888 S. Main";
				employee22.City = "Kyle";
				employee22.State = StateAbbr.TX;
				employee22.ZipCode = "78640";

				var result = UserManager.Create(employee22,"rhythm");
				db.SaveChanges();
				employee22 = db.Users.First(u => u.UserName == "s.barnes@longhorncinema.com");

			}

			if (RoleManager.RoleExists("Employee") == false)
			{
				RoleManager.Create(new AppRole("Employee"));
			}

			if (UserManager.IsInRole(employee22.Id, "Employee") == false)
			{
				UserManager.AddToRole(employee22.Id, "Employee");
			}

			db.SaveChanges();

			AppUser employee23 = db.Users.FirstOrDefault(u => u.Email == "l.jones@longhorncinema.com");

			if (employee23 == null)
			{
				employee23 = new AppUser();
				employee23.UserName = "l.jones@longhorncinema.com";
				employee23.Email = "l.jones@longhorncinema.com";
				employee23.FirstName = "Lester";
				employee23.LastName = "Jones";
				employee23.MiddleInitial = "L";
				employee23.PhoneNumber = "9886662222";
				employee23.Birthday = new DateTime(6/29/1996);
				employee23.Street = "999 LeBlat";
				employee23.City = "Austin";
				employee23.State = StateAbbr.TX;
				employee23.ZipCode = "78747";

				var result = UserManager.Create(employee23,"kindly");
				db.SaveChanges();
				employee23 = db.Users.First(u => u.UserName == "l.jones@longhorncinema.com");

			}

			if (RoleManager.RoleExists("Employee") == false)
			{
				RoleManager.Create(new AppRole("Employee"));
			}

			if (UserManager.IsInRole(employee23.Id, "Employee") == false)
			{
				UserManager.AddToRole(employee23.Id, "Employee");
			}

			db.SaveChanges();

			AppUser employee24 = db.Users.FirstOrDefault(u => u.Email == "h.garcia@longhorncinema.com");

			if (employee24 == null)
			{
				employee24 = new AppUser();
				employee24.UserName = "h.garcia@longhorncinema.com";
				employee24.Email = "h.garcia@longhorncinema.com";
				employee24.FirstName = "Hector";
				employee24.LastName = "Garcia";
				employee24.MiddleInitial = "W";
				employee24.PhoneNumber = "9221114444";
				employee24.Birthday = new DateTime(5/13/1997);
				employee24.Street = "777 PBR Drive";
				employee24.City = "Austin";
				employee24.State = StateAbbr.TX;
				employee24.ZipCode = "78712";

				var result = UserManager.Create(employee24,"instrument");
				db.SaveChanges();
				employee24 = db.Users.First(u => u.UserName == "h.garcia@longhorncinema.com");

			}

			if (RoleManager.RoleExists("Employee") == false)
			{
				RoleManager.Create(new AppRole("Employee"));
			}

			if (UserManager.IsInRole(employee24.Id, "Employee") == false)
			{
				UserManager.AddToRole(employee24.Id, "Employee");
			}

			db.SaveChanges();

			AppUser employee25 = db.Users.FirstOrDefault(u => u.Email == "c.silva@longhorncinema.com");

			if (employee25 == null)
			{
				employee25 = new AppUser();
				employee25.UserName = "c.silva@longhorncinema.com";
				employee25.Email = "c.silva@longhorncinema.com";
				employee25.FirstName = "Cindy";
				employee25.LastName = "Silva";
				employee25.MiddleInitial = "S";
				employee25.PhoneNumber = "9221113333";
				employee25.Birthday = new DateTime(12/29/1997);
				employee25.Street = "900 4th St";
				employee25.City = "Austin";
				employee25.State = StateAbbr.TX;
				employee25.ZipCode = "78758";

				var result = UserManager.Create(employee25,"arched");
				db.SaveChanges();
				employee25 = db.Users.First(u => u.UserName == "c.silva@longhorncinema.com");

			}

			if (RoleManager.RoleExists("Employee") == false)
			{
				RoleManager.Create(new AppRole("Employee"));
			}

			if (UserManager.IsInRole(employee25.Id, "Employee") == false)
			{
				UserManager.AddToRole(employee25.Id, "Employee");
			}

			db.SaveChanges();

			AppUser employee26 = db.Users.FirstOrDefault(u => u.Email == "m.lopez@longhorncinema.com");

			if (employee26 == null)
			{
				employee26 = new AppUser();
				employee26.UserName = "m.lopez@longhorncinema.com";
				employee26.Email = "m.lopez@longhorncinema.com";
				employee26.FirstName = "Marshall";
				employee26.LastName = "Lopez";
				employee26.MiddleInitial = "T";
				employee26.PhoneNumber = "9234442222";
				employee26.Birthday = new DateTime(11/4/1996);
				employee26.Street = "90 SW North St";
				employee26.City = "Austin";
				employee26.State = StateAbbr.TX;
				employee26.ZipCode = "78729";

				var result = UserManager.Create(employee26,"median");
				db.SaveChanges();
				employee26 = db.Users.First(u => u.UserName == "m.lopez@longhorncinema.com");

			}

			if (RoleManager.RoleExists("Employee") == false)
			{
				RoleManager.Create(new AppRole("Employee"));
			}

			if (UserManager.IsInRole(employee26.Id, "Employee") == false)
			{
				UserManager.AddToRole(employee26.Id, "Employee");
			}

			db.SaveChanges();

			AppUser employee27 = db.Users.FirstOrDefault(u => u.Email == "b.larson@longhorncinema.com");

			if (employee27 == null)
			{
				employee27 = new AppUser();
				employee27.UserName = "b.larson@longhorncinema.com";
				employee27.Email = "b.larson@longhorncinema.com";
				employee27.FirstName = "Bill";
				employee27.LastName = "Larson";
				employee27.MiddleInitial = "B";
				employee27.PhoneNumber = "9795554444";
				employee27.Birthday = new DateTime(11/14/1999);
				employee27.Street = "1212 N. First Ave";
				employee27.City = "Round Rock";
				employee27.State = StateAbbr.TX;
				employee27.ZipCode = "78665";

				var result = UserManager.Create(employee27,"approval");
				db.SaveChanges();
				employee27 = db.Users.First(u => u.UserName == "b.larson@longhorncinema.com");

			}

			if (RoleManager.RoleExists("Employee") == false)
			{
				RoleManager.Create(new AppRole("Employee"));
			}

			if (UserManager.IsInRole(employee27.Id, "Employee") == false)
			{
				UserManager.AddToRole(employee27.Id, "Employee");
			}

			db.SaveChanges();

			AppUser employee28 = db.Users.FirstOrDefault(u => u.Email == "s.rankin@longhorncinema.com");

			if (employee28 == null)
			{
				employee28 = new AppUser();
				employee28.UserName = "s.rankin@longhorncinema.com";
				employee28.Email = "s.rankin@longhorncinema.com";
				employee28.FirstName = "Suzie";
				employee28.LastName = "Rankin";
				employee28.MiddleInitial = "R";
				employee28.PhoneNumber = "9893336666";
				employee28.Birthday = new DateTime(12/17/1999);
				employee28.Street = "23 Polar Bear Road";
				employee28.City = "Austin";
				employee28.State = StateAbbr.TX;
				employee28.ZipCode = "78712";

				var result = UserManager.Create(employee28,"decorate");
				db.SaveChanges();
				employee28 = db.Users.First(u => u.UserName == "s.rankin@longhorncinema.com");

			}

			if (RoleManager.RoleExists("Employee") == false)
			{
				RoleManager.Create(new AppRole("Employee"));
			}

			if (UserManager.IsInRole(employee28.Id, "Employee") == false)
			{
				UserManager.AddToRole(employee28.Id, "Employee");
			}

			db.SaveChanges();

			}
	}
}
