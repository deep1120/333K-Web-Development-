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
	public class CustomerData
	{

		public void AddCustomer(AppDbContext db)
		{

			AppUserManager UserManager = new AppUserManager(new UserStore<AppUser>(db));
			AppRoleManager RoleManager = new AppRoleManager(new RoleStore<AppRole>(db));
			AppUser customer1 = db.Users.FirstOrDefault(u => u.Email == "cbaker@example.com");

			if (customer1 == null)
			{
				customer1 = new AppUser();
				customer1.UserName = "cbaker@example.com";
				customer1.Email = "cbaker@example.com";
				customer1.FirstName = "Christopher";
				customer1.LastName = "Baker";
				customer1.MiddleInitial = "L.";
				customer1.PhoneNumber = "5125550180";
				customer1.Birthday = new DateTime(1949, 11, 23);
				customer1.Street = "1245 Lake Anchorage Blvd.";
				customer1.City = "Austin";
				customer1.State = StateAbbr.TX;
				customer1.ZipCode = "78705";
				customer1.PopcornPoints = 110;

				var result = UserManager.Create(customer1,"hello1");
				db.SaveChanges();
				customer1 = db.Users.First(u => u.UserName == "cbaker@example.com");

			}

			if (RoleManager.RoleExists("Customer") == false)
			{
				RoleManager.Create(new AppRole("Customer"));
			}

			if (UserManager.IsInRole(customer1.Id, "Customer") == false)
			{
				UserManager.AddToRole(customer1.Id, "Customer");
			}

			db.SaveChanges();

			AppUser customer2 = db.Users.FirstOrDefault(u => u.Email == "banker@longhorn.net");

			if (customer2 == null)
			{
				customer2 = new AppUser();
				customer2.UserName = "banker@longhorn.net";
				customer2.Email = "banker@longhorn.net";
				customer2.FirstName = "Michelle";
				customer2.LastName = "Banks";
				customer2.MiddleInitial = "";
				customer2.PhoneNumber = "5125550183";
				customer2.Birthday = new DateTime(1962, 11, 27);
				customer2.Street = "1300 Tall Pine Lane";
				customer2.City = "Austin";
				customer2.State = StateAbbr.TX;
				customer2.ZipCode = "78712";
				customer2.PopcornPoints = 40;

				var result = UserManager.Create(customer2,"potato");
				db.SaveChanges();
				customer2 = db.Users.First(u => u.UserName == "banker@longhorn.net");

			}

			if (RoleManager.RoleExists("Customer") == false)
			{
				RoleManager.Create(new AppRole("Customer"));
			}

			if (UserManager.IsInRole(customer2.Id, "Customer") == false)
			{
				UserManager.AddToRole(customer2.Id, "Customer");
			}

			db.SaveChanges();

			AppUser customer3 = db.Users.FirstOrDefault(u => u.Email == "franco@example.com");

			if (customer3 == null)
			{
				customer3 = new AppUser();
				customer3.UserName = "franco@example.com";
				customer3.Email = "franco@example.com";
				customer3.FirstName = "Franco";
				customer3.LastName = "Broccolo";
				customer3.MiddleInitial = "V";
				customer3.PhoneNumber = "5125550128";
				customer3.Birthday = new DateTime(1992, 10, 11);
				customer3.Street = "62 Browning Road";
				customer3.City = "Austin";
				customer3.State = StateAbbr.TX;
				customer3.ZipCode = "78704";
				customer3.PopcornPoints = 30;

				var result = UserManager.Create(customer3,"painting");
				db.SaveChanges();
				customer3 = db.Users.First(u => u.UserName == "franco@example.com");

			}

			if (RoleManager.RoleExists("Customer") == false)
			{
				RoleManager.Create(new AppRole("Customer"));
			}

			if (UserManager.IsInRole(customer3.Id, "Customer") == false)
			{
				UserManager.AddToRole(customer3.Id, "Customer");
			}

			db.SaveChanges();

			AppUser customer4 = db.Users.FirstOrDefault(u => u.Email == "wchang@example.com");

			if (customer4 == null)
			{
				customer4 = new AppUser();
				customer4.UserName = "wchang@example.com";
				customer4.Email = "wchang@example.com";
				customer4.FirstName = "Wendy";
				customer4.LastName = "Chang";
				customer4.MiddleInitial = "L";
				customer4.PhoneNumber = "5125550133";
				customer4.Birthday = new DateTime(1997, 5, 16);
				customer4.Street = "202 Bellmont Hall";
				customer4.City = "Round Rock";
				customer4.State = StateAbbr.TX;
				customer4.ZipCode = "78681";
				customer4.PopcornPoints = 0;

				var result = UserManager.Create(customer4,"texas1");
				db.SaveChanges();
				customer4 = db.Users.First(u => u.UserName == "wchang@example.com");

			}

			if (RoleManager.RoleExists("Customer") == false)
			{
				RoleManager.Create(new AppRole("Customer"));
			}

			if (UserManager.IsInRole(customer4.Id, "Customer") == false)
			{
				UserManager.AddToRole(customer4.Id, "Customer");
			}

			db.SaveChanges();

			AppUser customer5 = db.Users.FirstOrDefault(u => u.Email == "limchou@gogle.com");

			if (customer5 == null)
			{
				customer5 = new AppUser();
				customer5.UserName = "limchou@gogle.com";
				customer5.Email = "limchou@gogle.com";
				customer5.FirstName = "Lim";
				customer5.LastName = "Chou";
				customer5.MiddleInitial = "";
				customer5.PhoneNumber = "5125550102";
				customer5.Birthday = new DateTime(1970, 4, 6);
				customer5.Street = "1600 Teresa Lane";
				customer5.City = "Austin";
				customer5.State = StateAbbr.TX;
				customer5.ZipCode = "78705";
				customer5.PopcornPoints = 40;

				var result = UserManager.Create(customer5,"Anchorage");
				db.SaveChanges();
				customer5 = db.Users.First(u => u.UserName == "limchou@gogle.com");

			}

			if (RoleManager.RoleExists("Customer") == false)
			{
				RoleManager.Create(new AppRole("Customer"));
			}

			if (UserManager.IsInRole(customer5.Id, "Customer") == false)
			{
				UserManager.AddToRole(customer5.Id, "Customer");
			}

			db.SaveChanges();

			AppUser customer6 = db.Users.FirstOrDefault(u => u.Email == "shdixon@aoll.com");

			if (customer6 == null)
			{
				customer6 = new AppUser();
				customer6.UserName = "shdixon@aoll.com";
				customer6.Email = "shdixon@aoll.com";
				customer6.FirstName = "Shan";
				customer6.LastName = "Dixon";
				customer6.MiddleInitial = "D";
				customer6.PhoneNumber = "5125550146";
				customer6.Birthday = new DateTime(1984, 1, 12);
				customer6.Street = "234 Holston Circle";
				customer6.City = "Austin";
				customer6.State = StateAbbr.TX;
				customer6.ZipCode = "78712";
				customer6.PopcornPoints = 20;

				var result = UserManager.Create(customer6,"pepperoni");
				db.SaveChanges();
				customer6 = db.Users.First(u => u.UserName == "shdixon@aoll.com");

			}

			if (RoleManager.RoleExists("Customer") == false)
			{
				RoleManager.Create(new AppRole("Customer"));
			}

			if (UserManager.IsInRole(customer6.Id, "Customer") == false)
			{
				UserManager.AddToRole(customer6.Id, "Customer");
			}

			db.SaveChanges();

			AppUser customer7 = db.Users.FirstOrDefault(u => u.Email == "j.b.evans@aheca.org");

			if (customer7 == null)
			{
				customer7 = new AppUser();
				customer7.UserName = "j.b.evans@aheca.org";
				customer7.Email = "j.b.evans@aheca.org";
				customer7.FirstName = "Jim Bob";
				customer7.LastName = "Evans";
				customer7.MiddleInitial = "";
				customer7.PhoneNumber = "5125550170";
				customer7.Birthday = new DateTime(1959, 9, 9);
				customer7.Street = "506 Farrell Circle";
				customer7.City = "Georgetown";
				customer7.State = StateAbbr.TX;
				customer7.ZipCode = "78628";
				customer7.PopcornPoints = 50;

				var result = UserManager.Create(customer7,"longhorns");
				db.SaveChanges();
				customer7 = db.Users.First(u => u.UserName == "j.b.evans@aheca.org");

			}

			if (RoleManager.RoleExists("Customer") == false)
			{
				RoleManager.Create(new AppRole("Customer"));
			}

			if (UserManager.IsInRole(customer7.Id, "Customer") == false)
			{
				UserManager.AddToRole(customer7.Id, "Customer");
			}

			db.SaveChanges();

			AppUser customer8 = db.Users.FirstOrDefault(u => u.Email == "feeley@penguin.org");

			if (customer8 == null)
			{
				customer8 = new AppUser();
				customer8.UserName = "feeley@penguin.org";
				customer8.Email = "feeley@penguin.org";
				customer8.FirstName = "Lou Ann";
				customer8.LastName = "Feeley";
				customer8.MiddleInitial = "K";
				customer8.PhoneNumber = "5125550105";
				customer8.Birthday = new DateTime(2001, 1, 12);
				customer8.Street = "600 S 8th Street W";
				customer8.City = "Austin";
				customer8.State = StateAbbr.TX;
				customer8.ZipCode = "78746";
				customer8.PopcornPoints = 170;

				var result = UserManager.Create(customer8,"aggies");
				db.SaveChanges();
				customer8 = db.Users.First(u => u.UserName == "feeley@penguin.org");

			}

			if (RoleManager.RoleExists("Customer") == false)
			{
				RoleManager.Create(new AppRole("Customer"));
			}

			if (UserManager.IsInRole(customer8.Id, "Customer") == false)
			{
				UserManager.AddToRole(customer8.Id, "Customer");
			}

			db.SaveChanges();

			AppUser customer9 = db.Users.FirstOrDefault(u => u.Email == "tfreeley@minnetonka.ci.us");

			if (customer9 == null)
			{
				customer9 = new AppUser();
				customer9.UserName = "tfreeley@minnetonka.ci.us";
				customer9.Email = "tfreeley@minnetonka.ci.us";
				customer9.FirstName = "Tesa";
				customer9.LastName = "Freeley";
				customer9.MiddleInitial = "P";
				customer9.PhoneNumber = "5125550114";
				customer9.Birthday = new DateTime(1991, 2, 4);
				customer9.Street = "4448 Fairview Ave.";
				customer9.City = "Horseshoe Bay";
				customer9.State = StateAbbr.TX;
				customer9.ZipCode = "78657";
				customer9.PopcornPoints = 160;

				var result = UserManager.Create(customer9,"raiders");
				db.SaveChanges();
				customer9 = db.Users.First(u => u.UserName == "tfreeley@minnetonka.ci.us");

			}

			if (RoleManager.RoleExists("Customer") == false)
			{
				RoleManager.Create(new AppRole("Customer"));
			}

			if (UserManager.IsInRole(customer9.Id, "Customer") == false)
			{
				UserManager.AddToRole(customer9.Id, "Customer");
			}

			db.SaveChanges();

			AppUser customer10 = db.Users.FirstOrDefault(u => u.Email == "mgarcia@gogle.com");

			if (customer10 == null)
			{
				customer10 = new AppUser();
				customer10.UserName = "mgarcia@gogle.com";
				customer10.Email = "mgarcia@gogle.com";
				customer10.FirstName = "Margaret";
				customer10.LastName = "Garcia";
				customer10.MiddleInitial = "L";
				customer10.PhoneNumber = "5125550155";
				customer10.Birthday = new DateTime(1991, 10, 2);
				customer10.Street = "594 Longview";
				customer10.City = "Austin";
				customer10.State = StateAbbr.TX;
				customer10.ZipCode = "78727";
				customer10.PopcornPoints = 10;

				var result = UserManager.Create(customer10,"mustangs");
				db.SaveChanges();
				customer10 = db.Users.First(u => u.UserName == "mgarcia@gogle.com");

			}

			if (RoleManager.RoleExists("Customer") == false)
			{
				RoleManager.Create(new AppRole("Customer"));
			}

			if (UserManager.IsInRole(customer10.Id, "Customer") == false)
			{
				UserManager.AddToRole(customer10.Id, "Customer");
			}

			db.SaveChanges();

			AppUser customer11 = db.Users.FirstOrDefault(u => u.Email == "chaley@thug.com");

			if (customer11 == null)
			{
				customer11 = new AppUser();
				customer11.UserName = "chaley@thug.com";
				customer11.Email = "chaley@thug.com";
				customer11.FirstName = "Charles";
				customer11.LastName = "Haley";
				customer11.MiddleInitial = "E";
				customer11.PhoneNumber = "5125550116";
				customer11.Birthday = new DateTime(1974, 7, 10);
				customer11.Street = "One Cowboy Pkwy";
				customer11.City = "Austin";
				customer11.State = StateAbbr.TX;
				customer11.ZipCode = "78712";
				customer11.PopcornPoints = 40;

				var result = UserManager.Create(customer11,"onetime");
				db.SaveChanges();
				customer11 = db.Users.First(u => u.UserName == "chaley@thug.com");

			}

			if (RoleManager.RoleExists("Customer") == false)
			{
				RoleManager.Create(new AppRole("Customer"));
			}

			if (UserManager.IsInRole(customer11.Id, "Customer") == false)
			{
				UserManager.AddToRole(customer11.Id, "Customer");
			}

			db.SaveChanges();

			AppUser customer12 = db.Users.FirstOrDefault(u => u.Email == "jeffh@sonic.com");

			if (customer12 == null)
			{
				customer12 = new AppUser();
				customer12.UserName = "jeffh@sonic.com";
				customer12.Email = "jeffh@sonic.com";
				customer12.FirstName = "Jeffrey";
				customer12.LastName = "Hampton";
				customer12.MiddleInitial = "T.";
				customer12.PhoneNumber = "5125550150";
				customer12.Birthday = new DateTime(2004, 3, 10);
				customer12.Street = "337 38th St.";
				customer12.City = "San Marcos";
				customer12.State = StateAbbr.TX;
				customer12.ZipCode = "78666";
				customer12.PopcornPoints = 150;

				var result = UserManager.Create(customer12,"hampton1");
				db.SaveChanges();
				customer12 = db.Users.First(u => u.UserName == "jeffh@sonic.com");

			}

			if (RoleManager.RoleExists("Customer") == false)
			{
				RoleManager.Create(new AppRole("Customer"));
			}

			if (UserManager.IsInRole(customer12.Id, "Customer") == false)
			{
				UserManager.AddToRole(customer12.Id, "Customer");
			}

			db.SaveChanges();

			AppUser customer13 = db.Users.FirstOrDefault(u => u.Email == "wjhearniii@umich.org");

			if (customer13 == null)
			{
				customer13 = new AppUser();
				customer13.UserName = "wjhearniii@umich.org";
				customer13.Email = "wjhearniii@umich.org";
				customer13.FirstName = "John";
				customer13.LastName = "Hearn";
				customer13.MiddleInitial = "B";
				customer13.PhoneNumber = "5125550196";
				customer13.Birthday = new DateTime(1950, 8, 5);
				customer13.Street = "4225 North First";
				customer13.City = "Austin";
				customer13.State = StateAbbr.TX;
				customer13.ZipCode = "78705";
				customer13.PopcornPoints = 0;

				var result = UserManager.Create(customer13,"jhearn22");
				db.SaveChanges();
				customer13 = db.Users.First(u => u.UserName == "wjhearniii@umich.org");

			}

			if (RoleManager.RoleExists("Customer") == false)
			{
				RoleManager.Create(new AppRole("Customer"));
			}

			if (UserManager.IsInRole(customer13.Id, "Customer") == false)
			{
				UserManager.AddToRole(customer13.Id, "Customer");
			}

			db.SaveChanges();

			AppUser customer14 = db.Users.FirstOrDefault(u => u.Email == "ahick@yaho.com");

			if (customer14 == null)
			{
				customer14 = new AppUser();
				customer14.UserName = "ahick@yaho.com";
				customer14.Email = "ahick@yaho.com";
				customer14.FirstName = "Anthony";
				customer14.LastName = "Hicks";
				customer14.MiddleInitial = "J";
				customer14.PhoneNumber = "5125550188";
				customer14.Birthday = new DateTime(2004, 12, 8);
				customer14.Street = "32 NE Garden Ln., Ste 910";
				customer14.City = "Austin";
				customer14.State = StateAbbr.TX;
				customer14.ZipCode = "78712";
				customer14.PopcornPoints = 60;

				var result = UserManager.Create(customer14,"hickhickup");
				db.SaveChanges();
				customer14 = db.Users.First(u => u.UserName == "ahick@yaho.com");

			}

			if (RoleManager.RoleExists("Customer") == false)
			{
				RoleManager.Create(new AppRole("Customer"));
			}

			if (UserManager.IsInRole(customer14.Id, "Customer") == false)
			{
				UserManager.AddToRole(customer14.Id, "Customer");
			}

			db.SaveChanges();

			AppUser customer15 = db.Users.FirstOrDefault(u => u.Email == "ingram@jack.com");

			if (customer15 == null)
			{
				customer15 = new AppUser();
				customer15.UserName = "ingram@jack.com";
				customer15.Email = "ingram@jack.com";
				customer15.FirstName = "Brad";
				customer15.LastName = "Ingram";
				customer15.MiddleInitial = "S.";
				customer15.PhoneNumber = "5125550116";
				customer15.Birthday = new DateTime(2001, 9, 5);
				customer15.Street = "6548 La Posada Ct.";
				customer15.City = "New York";
				customer15.State = StateAbbr.NY;
				customer15.ZipCode = "10101";
				customer15.PopcornPoints = 20;

				var result = UserManager.Create(customer15,"ingram2015");
				db.SaveChanges();
				customer15 = db.Users.First(u => u.UserName == "ingram@jack.com");

			}

			if (RoleManager.RoleExists("Customer") == false)
			{
				RoleManager.Create(new AppRole("Customer"));
			}

			if (UserManager.IsInRole(customer15.Id, "Customer") == false)
			{
				UserManager.AddToRole(customer15.Id, "Customer");
			}

			db.SaveChanges();

			AppUser customer16 = db.Users.FirstOrDefault(u => u.Email == "toddj@yourmom.com");

			if (customer16 == null)
			{
				customer16 = new AppUser();
				customer16.UserName = "toddj@yourmom.com";
				customer16.Email = "toddj@yourmom.com";
				customer16.FirstName = "Todd";
				customer16.LastName = "Jacobs";
				customer16.MiddleInitial = "L.";
				customer16.PhoneNumber = "5125550166";
				customer16.Birthday = new DateTime(1999, 1, 20);
				customer16.Street = "4564 Elm St.";
				customer16.City = "Austin";
				customer16.State = StateAbbr.TX;
				customer16.ZipCode = "78729";
				customer16.PopcornPoints = 170;

				var result = UserManager.Create(customer16,"toddy25");
				db.SaveChanges();
				customer16 = db.Users.First(u => u.UserName == "toddj@yourmom.com");

			}

			if (RoleManager.RoleExists("Customer") == false)
			{
				RoleManager.Create(new AppRole("Customer"));
			}

			if (UserManager.IsInRole(customer16.Id, "Customer") == false)
			{
				UserManager.AddToRole(customer16.Id, "Customer");
			}

			db.SaveChanges();

			AppUser customer17 = db.Users.FirstOrDefault(u => u.Email == "thequeen@aska.net");

			if (customer17 == null)
			{
				customer17 = new AppUser();
				customer17.UserName = "thequeen@aska.net";
				customer17.Email = "thequeen@aska.net";
				customer17.FirstName = "Victoria";
				customer17.LastName = "Lawrence";
				customer17.MiddleInitial = "M.";
				customer17.PhoneNumber = "5125550173";
				customer17.Birthday = new DateTime(2000, 4, 14);
				customer17.Street = "6639 Butterfly Ln.";
				customer17.City = "Beverly Hills";
				customer17.State = StateAbbr.CA;
				customer17.ZipCode = "90210";
				customer17.PopcornPoints = 130;

				var result = UserManager.Create(customer17,"something");
				db.SaveChanges();
				customer17 = db.Users.First(u => u.UserName == "thequeen@aska.net");

			}

			if (RoleManager.RoleExists("Customer") == false)
			{
				RoleManager.Create(new AppRole("Customer"));
			}

			if (UserManager.IsInRole(customer17.Id, "Customer") == false)
			{
				UserManager.AddToRole(customer17.Id, "Customer");
			}

			db.SaveChanges();

			AppUser customer18 = db.Users.FirstOrDefault(u => u.Email == "linebacker@gogle.com");

			if (customer18 == null)
			{
				customer18 = new AppUser();
				customer18.UserName = "linebacker@gogle.com";
				customer18.Email = "linebacker@gogle.com";
				customer18.FirstName = "Erik";
				customer18.LastName = "Lineback";
				customer18.MiddleInitial = "W";
				customer18.PhoneNumber = "5125550167";
				customer18.Birthday = new DateTime(2003, 12, 2);
				customer18.Street = "1300 Netherland St";
				customer18.City = "Austin";
				customer18.State = StateAbbr.TX;
				customer18.ZipCode = "78758";
				customer18.PopcornPoints = 60;

				var result = UserManager.Create(customer18,"Password1");
				db.SaveChanges();
				customer18 = db.Users.First(u => u.UserName == "linebacker@gogle.com");

			}

			if (RoleManager.RoleExists("Customer") == false)
			{
				RoleManager.Create(new AppRole("Customer"));
			}

			if (UserManager.IsInRole(customer18.Id, "Customer") == false)
			{
				UserManager.AddToRole(customer18.Id, "Customer");
			}

			db.SaveChanges();

			AppUser customer19 = db.Users.FirstOrDefault(u => u.Email == "elowe@netscare.net");

			if (customer19 == null)
			{
				customer19 = new AppUser();
				customer19.UserName = "elowe@netscare.net";
				customer19.Email = "elowe@netscare.net";
				customer19.FirstName = "Ernest";
				customer19.LastName = "Lowe";
				customer19.MiddleInitial = "S";
				customer19.PhoneNumber = "5125550187";
				customer19.Birthday = new DateTime(1977, 12, 7);
				customer19.Street = "3201 Pine Drive";
				customer19.City = "New Braunfels";
				customer19.State = StateAbbr.TX;
				customer19.ZipCode = "78130";
				customer19.PopcornPoints = 20;

				var result = UserManager.Create(customer19,"aclfest2017");
				db.SaveChanges();
				customer19 = db.Users.First(u => u.UserName == "elowe@netscare.net");

			}

			if (RoleManager.RoleExists("Customer") == false)
			{
				RoleManager.Create(new AppRole("Customer"));
			}

			if (UserManager.IsInRole(customer19.Id, "Customer") == false)
			{
				UserManager.AddToRole(customer19.Id, "Customer");
			}

			db.SaveChanges();

			AppUser customer20 = db.Users.FirstOrDefault(u => u.Email == "cluce@gogle.com");

			if (customer20 == null)
			{
				customer20 = new AppUser();
				customer20.UserName = "cluce@gogle.com";
				customer20.Email = "cluce@gogle.com";
				customer20.FirstName = "Chuck";
				customer20.LastName = "Luce";
				customer20.MiddleInitial = "B";
				customer20.PhoneNumber = "5125550141";
				customer20.Birthday = new DateTime(1949, 3, 16);
				customer20.Street = "2345 Rolling Clouds";
				customer20.City = "Cactus";
				customer20.State = StateAbbr.TX;
				customer20.ZipCode = "79013";
				customer20.PopcornPoints = 180;

				var result = UserManager.Create(customer20,"nothinggood");
				db.SaveChanges();
				customer20 = db.Users.First(u => u.UserName == "cluce@gogle.com");

			}

			if (RoleManager.RoleExists("Customer") == false)
			{
				RoleManager.Create(new AppRole("Customer"));
			}

			if (UserManager.IsInRole(customer20.Id, "Customer") == false)
			{
				UserManager.AddToRole(customer20.Id, "Customer");
			}

			db.SaveChanges();

			AppUser customer21 = db.Users.FirstOrDefault(u => u.Email == "mackcloud@george.com");

			if (customer21 == null)
			{
				customer21 = new AppUser();
				customer21.UserName = "mackcloud@george.com";
				customer21.Email = "mackcloud@george.com";
				customer21.FirstName = "Jennifer";
				customer21.LastName = "MacLeod";
				customer21.MiddleInitial = "D.";
				customer21.PhoneNumber = "5125550185";
				customer21.Birthday = new DateTime(1947, 2, 21);
				customer21.Street = "2504 Far West Blvd.";
				customer21.City = "Marble Falls";
				customer21.State = StateAbbr.TX;
				customer21.ZipCode = "78654";
				customer21.PopcornPoints = 170;

				var result = UserManager.Create(customer21,"whatever");
				db.SaveChanges();
				customer21 = db.Users.First(u => u.UserName == "mackcloud@george.com");

			}

			if (RoleManager.RoleExists("Customer") == false)
			{
				RoleManager.Create(new AppRole("Customer"));
			}

			if (UserManager.IsInRole(customer21.Id, "Customer") == false)
			{
				UserManager.AddToRole(customer21.Id, "Customer");
			}

			db.SaveChanges();

			AppUser customer22 = db.Users.FirstOrDefault(u => u.Email == "cmartin@beets.com");

			if (customer22 == null)
			{
				customer22 = new AppUser();
				customer22.UserName = "cmartin@beets.com";
				customer22.Email = "cmartin@beets.com";
				customer22.FirstName = "Elizabeth";
				customer22.LastName = "Markham";
				customer22.MiddleInitial = "P.";
				customer22.PhoneNumber = "5125550134";
				customer22.Birthday = new DateTime(1972, 3, 20);
				customer22.Street = "7861 Chevy Chase";
				customer22.City = "Kissimmee";
				customer22.State = StateAbbr.FL;
				customer22.ZipCode = "34741";
				customer22.PopcornPoints = 100;

				var result = UserManager.Create(customer22,"whocares");
				db.SaveChanges();
				customer22 = db.Users.First(u => u.UserName == "cmartin@beets.com");

			}

			if (RoleManager.RoleExists("Customer") == false)
			{
				RoleManager.Create(new AppRole("Customer"));
			}

			if (UserManager.IsInRole(customer22.Id, "Customer") == false)
			{
				UserManager.AddToRole(customer22.Id, "Customer");
			}

			db.SaveChanges();

			AppUser customer23 = db.Users.FirstOrDefault(u => u.Email == "clarence@yoho.com");

			if (customer23 == null)
			{
				customer23 = new AppUser();
				customer23.UserName = "clarence@yoho.com";
				customer23.Email = "clarence@yoho.com";
				customer23.FirstName = "Clarence";
				customer23.LastName = "Martin";
				customer23.MiddleInitial = "A";
				customer23.PhoneNumber = "5125550151";
				customer23.Birthday = new DateTime(1992, 7, 19);
				customer23.Street = "87 Alcedo St.";
				customer23.City = "Austin";
				customer23.State = StateAbbr.TX;
				customer23.ZipCode = "78709";
				customer23.PopcornPoints = 130;

				var result = UserManager.Create(customer23,"xcellent");
				db.SaveChanges();
				customer23 = db.Users.First(u => u.UserName == "clarence@yoho.com");

			}

			if (RoleManager.RoleExists("Customer") == false)
			{
				RoleManager.Create(new AppRole("Customer"));
			}

			if (UserManager.IsInRole(customer23.Id, "Customer") == false)
			{
				UserManager.AddToRole(customer23.Id, "Customer");
			}

			db.SaveChanges();

			AppUser customer24 = db.Users.FirstOrDefault(u => u.Email == "gregmartinez@drdre.com");

			if (customer24 == null)
			{
				customer24 = new AppUser();
				customer24.UserName = "gregmartinez@drdre.com";
				customer24.Email = "gregmartinez@drdre.com";
				customer24.FirstName = "Gregory";
				customer24.LastName = "Martinez";
				customer24.MiddleInitial = "R.";
				customer24.PhoneNumber = "5125550120";
				customer24.Birthday = new DateTime(1947, 5, 28);
				customer24.Street = "8295 Sunset Blvd.";
				customer24.City = "Red Rock";
				customer24.State = StateAbbr.TX;
				customer24.ZipCode = "78662";
				customer24.PopcornPoints = 20;

				var result = UserManager.Create(customer24,"snowsnow");
				db.SaveChanges();
				customer24 = db.Users.First(u => u.UserName == "gregmartinez@drdre.com");

			}

			if (RoleManager.RoleExists("Customer") == false)
			{
				RoleManager.Create(new AppRole("Customer"));
			}

			if (UserManager.IsInRole(customer24.Id, "Customer") == false)
			{
				UserManager.AddToRole(customer24.Id, "Customer");
			}

			db.SaveChanges();

			AppUser customer25 = db.Users.FirstOrDefault(u => u.Email == "cmiller@bob.com");

			if (customer25 == null)
			{
				customer25 = new AppUser();
				customer25.UserName = "cmiller@bob.com";
				customer25.Email = "cmiller@bob.com";
				customer25.FirstName = "Charles";
				customer25.LastName = "Miller";
				customer25.MiddleInitial = "R.";
				customer25.PhoneNumber = "5125550198";
				customer25.Birthday = new DateTime(1990, 10, 15);
				customer25.Street = "8962 Main St.";
				customer25.City = "South Padre Island";
				customer25.State = StateAbbr.TX;
				customer25.ZipCode = "78597";
				customer25.PopcornPoints = 20;

				var result = UserManager.Create(customer25,"mydogspot");
				db.SaveChanges();
				customer25 = db.Users.First(u => u.UserName == "cmiller@bob.com");

			}

			if (RoleManager.RoleExists("Customer") == false)
			{
				RoleManager.Create(new AppRole("Customer"));
			}

			if (UserManager.IsInRole(customer25.Id, "Customer") == false)
			{
				UserManager.AddToRole(customer25.Id, "Customer");
			}

			db.SaveChanges();

			AppUser customer26 = db.Users.FirstOrDefault(u => u.Email == "knelson@aoll.com");

			if (customer26 == null)
			{
				customer26 = new AppUser();
				customer26.UserName = "knelson@aoll.com";
				customer26.Email = "knelson@aoll.com";
				customer26.FirstName = "Kelly";
				customer26.LastName = "Nelson";
				customer26.MiddleInitial = "T";
				customer26.PhoneNumber = "5125550177";
				customer26.Birthday = new DateTime(1971, 7, 13);
				customer26.Street = "2601 Red River";
				customer26.City = "Disney";
				customer26.State = StateAbbr.OK;
				customer26.ZipCode = "74340";
				customer26.PopcornPoints = 110;

				var result = UserManager.Create(customer26,"spotmydog");
				db.SaveChanges();
				customer26 = db.Users.First(u => u.UserName == "knelson@aoll.com");

			}

			if (RoleManager.RoleExists("Customer") == false)
			{
				RoleManager.Create(new AppRole("Customer"));
			}

			if (UserManager.IsInRole(customer26.Id, "Customer") == false)
			{
				UserManager.AddToRole(customer26.Id, "Customer");
			}

			db.SaveChanges();

			AppUser customer27 = db.Users.FirstOrDefault(u => u.Email == "joewin@xfactor.com");

			if (customer27 == null)
			{
				customer27 = new AppUser();
				customer27.UserName = "joewin@xfactor.com";
				customer27.Email = "joewin@xfactor.com";
				customer27.FirstName = "Joe";
				customer27.LastName = "Nguyen";
				customer27.MiddleInitial = "C";
				customer27.PhoneNumber = "5125550174";
				customer27.Birthday = new DateTime(1984, 3, 17);
				customer27.Street = "1249 4th SW St.";
				customer27.City = "Del Rio";
				customer27.State = StateAbbr.TX;
				customer27.ZipCode = "78841";
				customer27.PopcornPoints = 150;

				var result = UserManager.Create(customer27,"joejoejoe");
				db.SaveChanges();
				customer27 = db.Users.First(u => u.UserName == "joewin@xfactor.com");

			}

			if (RoleManager.RoleExists("Customer") == false)
			{
				RoleManager.Create(new AppRole("Customer"));
			}

			if (UserManager.IsInRole(customer27.Id, "Customer") == false)
			{
				UserManager.AddToRole(customer27.Id, "Customer");
			}

			db.SaveChanges();

			AppUser customer28 = db.Users.FirstOrDefault(u => u.Email == "orielly@foxnews.cnn");

			if (customer28 == null)
			{
				customer28 = new AppUser();
				customer28.UserName = "orielly@foxnews.cnn";
				customer28.Email = "orielly@foxnews.cnn";
				customer28.FirstName = "Bill";
				customer28.LastName = "O'Reilly";
				customer28.MiddleInitial = "T";
				customer28.PhoneNumber = "5125550167";
				customer28.Birthday = new DateTime(1959, 7, 8);
				customer28.Street = "8800 Gringo Drive";
				customer28.City = "Austin";
				customer28.State = StateAbbr.TX;
				customer28.ZipCode = "78746";
				customer28.PopcornPoints = 190;

				var result = UserManager.Create(customer28,"billyboy");
				db.SaveChanges();
				customer28 = db.Users.First(u => u.UserName == "orielly@foxnews.cnn");

			}

			if (RoleManager.RoleExists("Customer") == false)
			{
				RoleManager.Create(new AppRole("Customer"));
			}

			if (UserManager.IsInRole(customer28.Id, "Customer") == false)
			{
				UserManager.AddToRole(customer28.Id, "Customer");
			}

			db.SaveChanges();

			AppUser customer29 = db.Users.FirstOrDefault(u => u.Email == "ankaisrad@gogle.com");

			if (customer29 == null)
			{
				customer29 = new AppUser();
				customer29.UserName = "ankaisrad@gogle.com";
				customer29.Email = "ankaisrad@gogle.com";
				customer29.FirstName = "Anka";
				customer29.LastName = "Radkovich";
				customer29.MiddleInitial = "L";
				customer29.PhoneNumber = "5125550151";
				customer29.Birthday = new DateTime(1966, 5, 19);
				customer29.Street = "1300 Elliott Pl";
				customer29.City = "Austin";
				customer29.State = StateAbbr.TX;
				customer29.ZipCode = "78712";
				customer29.PopcornPoints = 120;

				var result = UserManager.Create(customer29,"radgirl");
				db.SaveChanges();
				customer29 = db.Users.First(u => u.UserName == "ankaisrad@gogle.com");

			}

			if (RoleManager.RoleExists("Customer") == false)
			{
				RoleManager.Create(new AppRole("Customer"));
			}

			if (UserManager.IsInRole(customer29.Id, "Customer") == false)
			{
				UserManager.AddToRole(customer29.Id, "Customer");
			}

			db.SaveChanges();

			AppUser customer30 = db.Users.FirstOrDefault(u => u.Email == "megrhodes@freserve.co.uk");

			if (customer30 == null)
			{
				customer30 = new AppUser();
				customer30.UserName = "megrhodes@freserve.co.uk";
				customer30.Email = "megrhodes@freserve.co.uk";
				customer30.FirstName = "Megan";
				customer30.LastName = "Rhodes";
				customer30.MiddleInitial = "C.";
				customer30.PhoneNumber = "5125550133";
				customer30.Birthday = new DateTime(1965, 3, 12);
				customer30.Street = "4587 Enfield Rd.";
				customer30.City = "Austin";
				customer30.State = StateAbbr.TX;
				customer30.ZipCode = "78705";
				customer30.PopcornPoints = 190;

				var result = UserManager.Create(customer30,"meganr34");
				db.SaveChanges();
				customer30 = db.Users.First(u => u.UserName == "megrhodes@freserve.co.uk");

			}

			if (RoleManager.RoleExists("Customer") == false)
			{
				RoleManager.Create(new AppRole("Customer"));
			}

			if (UserManager.IsInRole(customer30.Id, "Customer") == false)
			{
				UserManager.AddToRole(customer30.Id, "Customer");
			}

			db.SaveChanges();

			AppUser customer31 = db.Users.FirstOrDefault(u => u.Email == "erynrice@aoll.com");

			if (customer31 == null)
			{
				customer31 = new AppUser();
				customer31.UserName = "erynrice@aoll.com";
				customer31.Email = "erynrice@aoll.com";
				customer31.FirstName = "Eryn";
				customer31.LastName = "Rice";
				customer31.MiddleInitial = "M.";
				customer31.PhoneNumber = "5125550196";
				customer31.Birthday = new DateTime(1975, 4, 28);
				customer31.Street = "3405 Rio Grande";
				customer31.City = "Austin";
				customer31.State = StateAbbr.TX;
				customer31.ZipCode = "78785";
				customer31.PopcornPoints = 190;

				var result = UserManager.Create(customer31,"ricearoni");
				db.SaveChanges();
				customer31 = db.Users.First(u => u.UserName == "erynrice@aoll.com");

			}

			if (RoleManager.RoleExists("Customer") == false)
			{
				RoleManager.Create(new AppRole("Customer"));
			}

			if (UserManager.IsInRole(customer31.Id, "Customer") == false)
			{
				UserManager.AddToRole(customer31.Id, "Customer");
			}

			db.SaveChanges();

			AppUser customer32 = db.Users.FirstOrDefault(u => u.Email == "jorge@noclue.com");

			if (customer32 == null)
			{
				customer32 = new AppUser();
				customer32.UserName = "jorge@noclue.com";
				customer32.Email = "jorge@noclue.com";
				customer32.FirstName = "Jorge";
				customer32.LastName = "Rodriguez";
				customer32.MiddleInitial = "";
				customer32.PhoneNumber = "5125550141";
				customer32.Birthday = new DateTime(1953, 12, 8);
				customer32.Street = "6788 Cotter Street";
				customer32.City = "Littlefield";
				customer32.State = StateAbbr.TX;
				customer32.ZipCode = "79339";
				customer32.PopcornPoints = 20;

				var result = UserManager.Create(customer32,"jrod2017");
				db.SaveChanges();
				customer32 = db.Users.First(u => u.UserName == "jorge@noclue.com");

			}

			if (RoleManager.RoleExists("Customer") == false)
			{
				RoleManager.Create(new AppRole("Customer"));
			}

			if (UserManager.IsInRole(customer32.Id, "Customer") == false)
			{
				UserManager.AddToRole(customer32.Id, "Customer");
			}

			db.SaveChanges();

			AppUser customer33 = db.Users.FirstOrDefault(u => u.Email == "mrrogers@lovelyday.com");

			if (customer33 == null)
			{
				customer33 = new AppUser();
				customer33.UserName = "mrrogers@lovelyday.com";
				customer33.Email = "mrrogers@lovelyday.com";
				customer33.FirstName = "Allen";
				customer33.LastName = "Rogers";
				customer33.MiddleInitial = "B.";
				customer33.PhoneNumber = "5125550189";
				customer33.Birthday = new DateTime(1973, 4, 22);
				customer33.Street = "4965 Oak Hill";
				customer33.City = "Austin";
				customer33.State = StateAbbr.TX;
				customer33.ZipCode = "78733";
				customer33.PopcornPoints = 100;

				var result = UserManager.Create(customer33,"rogerthat");
				db.SaveChanges();
				customer33 = db.Users.First(u => u.UserName == "mrrogers@lovelyday.com");

			}

			if (RoleManager.RoleExists("Customer") == false)
			{
				RoleManager.Create(new AppRole("Customer"));
			}

			if (UserManager.IsInRole(customer33.Id, "Customer") == false)
			{
				UserManager.AddToRole(customer33.Id, "Customer");
			}

			db.SaveChanges();

			AppUser customer34 = db.Users.FirstOrDefault(u => u.Email == "stjean@athome.com");

			if (customer34 == null)
			{
				customer34 = new AppUser();
				customer34.UserName = "stjean@athome.com";
				customer34.Email = "stjean@athome.com";
				customer34.FirstName = "Olivier";
				customer34.LastName = "Saint-Jean";
				customer34.MiddleInitial = "M";
				customer34.PhoneNumber = "5125550152";
				customer34.Birthday = new DateTime(1995, 2, 19);
				customer34.Street = "255 Toncray Dr.";
				customer34.City = "Austin";
				customer34.State = StateAbbr.TX;
				customer34.ZipCode = "78755";
				customer34.PopcornPoints = 250;

				var result = UserManager.Create(customer34,"bunnyhop");
				db.SaveChanges();
				customer34 = db.Users.First(u => u.UserName == "stjean@athome.com");

			}

			if (RoleManager.RoleExists("Customer") == false)
			{
				RoleManager.Create(new AppRole("Customer"));
			}

			if (UserManager.IsInRole(customer34.Id, "Customer") == false)
			{
				UserManager.AddToRole(customer34.Id, "Customer");
			}

			db.SaveChanges();

			AppUser customer35 = db.Users.FirstOrDefault(u => u.Email == "saunders@pen.com");

			if (customer35 == null)
			{
				customer35 = new AppUser();
				customer35.UserName = "saunders@pen.com";
				customer35.Email = "saunders@pen.com";
				customer35.FirstName = "Sarah";
				customer35.LastName = "Saunders";
				customer35.MiddleInitial = "J.";
				customer35.PhoneNumber = "5125550146";
				customer35.Birthday = new DateTime(1978, 2, 19);
				customer35.Street = "332 Avenue C";
				customer35.City = "Austin";
				customer35.State = StateAbbr.TX;
				customer35.ZipCode = "78701";
				customer35.PopcornPoints = 40;

				var result = UserManager.Create(customer35,"penguin12");
				db.SaveChanges();
				customer35 = db.Users.First(u => u.UserName == "saunders@pen.com");

			}

			if (RoleManager.RoleExists("Customer") == false)
			{
				RoleManager.Create(new AppRole("Customer"));
			}

			if (UserManager.IsInRole(customer35.Id, "Customer") == false)
			{
				UserManager.AddToRole(customer35.Id, "Customer");
			}

			db.SaveChanges();

			AppUser customer36 = db.Users.FirstOrDefault(u => u.Email == "willsheff@email.com");

			if (customer36 == null)
			{
				customer36 = new AppUser();
				customer36.UserName = "willsheff@email.com";
				customer36.Email = "willsheff@email.com";
				customer36.FirstName = "William";
				customer36.LastName = "Sewell";
				customer36.MiddleInitial = "T.";
				customer36.PhoneNumber = "5125550192";
				customer36.Birthday = new DateTime(2004, 12, 23);
				customer36.Street = "2365 51st St.";
				customer36.City = "El Paso";
				customer36.State = StateAbbr.TX;
				customer36.ZipCode = "79953";
				customer36.PopcornPoints = 200;

				var result = UserManager.Create(customer36,"alaskaboy");
				db.SaveChanges();
				customer36 = db.Users.First(u => u.UserName == "willsheff@email.com");

			}

			if (RoleManager.RoleExists("Customer") == false)
			{
				RoleManager.Create(new AppRole("Customer"));
			}

			if (UserManager.IsInRole(customer36.Id, "Customer") == false)
			{
				UserManager.AddToRole(customer36.Id, "Customer");
			}

			db.SaveChanges();

			AppUser customer37 = db.Users.FirstOrDefault(u => u.Email == "sheffiled@gogle.com");

			if (customer37 == null)
			{
				customer37 = new AppUser();
				customer37.UserName = "sheffiled@gogle.com";
				customer37.Email = "sheffiled@gogle.com";
				customer37.FirstName = "Martin";
				customer37.LastName = "Sheffield";
				customer37.MiddleInitial = "J.";
				customer37.PhoneNumber = "5125550131";
				customer37.Birthday = new DateTime(1960, 5, 8);
				customer37.Street = "3886 Avenue A";
				customer37.City = "Balmorhea";
				customer37.State = StateAbbr.TX;
				customer37.ZipCode = "79718";
				customer37.PopcornPoints = 130;

				var result = UserManager.Create(customer37,"martin1234");
				db.SaveChanges();
				customer37 = db.Users.First(u => u.UserName == "sheffiled@gogle.com");

			}

			if (RoleManager.RoleExists("Customer") == false)
			{
				RoleManager.Create(new AppRole("Customer"));
			}

			if (UserManager.IsInRole(customer37.Id, "Customer") == false)
			{
				UserManager.AddToRole(customer37.Id, "Customer");
			}

			db.SaveChanges();

			AppUser customer38 = db.Users.FirstOrDefault(u => u.Email == "johnsmith187@aoll.com");

			if (customer38 == null)
			{
				customer38 = new AppUser();
				customer38.UserName = "johnsmith187@aoll.com";
				customer38.Email = "johnsmith187@aoll.com";
				customer38.FirstName = "John";
				customer38.LastName = "Smith";
				customer38.MiddleInitial = "A";
				customer38.PhoneNumber = "5125550190";
				customer38.Birthday = new DateTime(1955, 6, 25);
				customer38.Street = "23 Hidden Forge Dr.";
				customer38.City = "Austin";
				customer38.State = StateAbbr.TX;
				customer38.ZipCode = "78760";
				customer38.PopcornPoints = 130;

				var result = UserManager.Create(customer38,"smitty444");
				db.SaveChanges();
				customer38 = db.Users.First(u => u.UserName == "johnsmith187@aoll.com");

			}

			if (RoleManager.RoleExists("Customer") == false)
			{
				RoleManager.Create(new AppRole("Customer"));
			}

			if (UserManager.IsInRole(customer38.Id, "Customer") == false)
			{
				UserManager.AddToRole(customer38.Id, "Customer");
			}

			db.SaveChanges();

			AppUser customer39 = db.Users.FirstOrDefault(u => u.Email == "dustroud@mail.com");

			if (customer39 == null)
			{
				customer39 = new AppUser();
				customer39.UserName = "dustroud@mail.com";
				customer39.Email = "dustroud@mail.com";
				customer39.FirstName = "Dustin";
				customer39.LastName = "Stroud";
				customer39.MiddleInitial = "P";
				customer39.PhoneNumber = "5125550157";
				customer39.Birthday = new DateTime(1967, 7, 26);
				customer39.Street = "1212 Rita Rd";
				customer39.City = "Austin";
				customer39.State = StateAbbr.TX;
				customer39.ZipCode = "78734";
				customer39.PopcornPoints = 90;

				var result = UserManager.Create(customer39,"dustydusty");
				db.SaveChanges();
				customer39 = db.Users.First(u => u.UserName == "dustroud@mail.com");

			}

			if (RoleManager.RoleExists("Customer") == false)
			{
				RoleManager.Create(new AppRole("Customer"));
			}

			if (UserManager.IsInRole(customer39.Id, "Customer") == false)
			{
				UserManager.AddToRole(customer39.Id, "Customer");
			}

			db.SaveChanges();

			AppUser customer40 = db.Users.FirstOrDefault(u => u.Email == "estuart@anchor.net");

			if (customer40 == null)
			{
				customer40 = new AppUser();
				customer40.UserName = "estuart@anchor.net";
				customer40.Email = "estuart@anchor.net";
				customer40.FirstName = "Eric";
				customer40.LastName = "Stuart";
				customer40.MiddleInitial = "D.";
				customer40.PhoneNumber = "5125550191";
				customer40.Birthday = new DateTime(1947, 12, 4);
				customer40.Street = "5576 Toro Ring";
				customer40.City = "Kyle";
				customer40.State = StateAbbr.TX;
				customer40.ZipCode = "78640";
				customer40.PopcornPoints = 170;

				var result = UserManager.Create(customer40,"stewball");
				db.SaveChanges();
				customer40 = db.Users.First(u => u.UserName == "estuart@anchor.net");

			}

			if (RoleManager.RoleExists("Customer") == false)
			{
				RoleManager.Create(new AppRole("Customer"));
			}

			if (UserManager.IsInRole(customer40.Id, "Customer") == false)
			{
				UserManager.AddToRole(customer40.Id, "Customer");
			}

			db.SaveChanges();

			AppUser customer41 = db.Users.FirstOrDefault(u => u.Email == "peterstump@noclue.com");

			if (customer41 == null)
			{
				customer41 = new AppUser();
				customer41.UserName = "peterstump@noclue.com";
				customer41.Email = "peterstump@noclue.com";
				customer41.FirstName = "Peter";
				customer41.LastName = "Stump";
				customer41.MiddleInitial = "L";
				customer41.PhoneNumber = "5125550136";
				customer41.Birthday = new DateTime(1974, 7, 10);
				customer41.Street = "1300 Kellen Circle";
				customer41.City = "Philadelphia";
				customer41.State = StateAbbr.PA;
				customer41.ZipCode = "19123";
				customer41.PopcornPoints = 50;

				var result = UserManager.Create(customer41,"slowwind");
				db.SaveChanges();
				customer41 = db.Users.First(u => u.UserName == "peterstump@noclue.com");

			}

			if (RoleManager.RoleExists("Customer") == false)
			{
				RoleManager.Create(new AppRole("Customer"));
			}

			if (UserManager.IsInRole(customer41.Id, "Customer") == false)
			{
				UserManager.AddToRole(customer41.Id, "Customer");
			}

			db.SaveChanges();

			AppUser customer42 = db.Users.FirstOrDefault(u => u.Email == "jtanner@mustang.net");

			if (customer42 == null)
			{
				customer42 = new AppUser();
				customer42.UserName = "jtanner@mustang.net";
				customer42.Email = "jtanner@mustang.net";
				customer42.FirstName = "Jeremy";
				customer42.LastName = "Tanner";
				customer42.MiddleInitial = "S.";
				customer42.PhoneNumber = "5125550170";
				customer42.Birthday = new DateTime(1944, 1, 11);
				customer42.Street = "4347 Almstead";
				customer42.City = "Austin";
				customer42.State = StateAbbr.TX;
				customer42.ZipCode = "78747";
				customer42.PopcornPoints = 190;

				var result = UserManager.Create(customer42,"tanner5454");
				db.SaveChanges();
				customer42 = db.Users.First(u => u.UserName == "jtanner@mustang.net");

			}

			if (RoleManager.RoleExists("Customer") == false)
			{
				RoleManager.Create(new AppRole("Customer"));
			}

			if (UserManager.IsInRole(customer42.Id, "Customer") == false)
			{
				UserManager.AddToRole(customer42.Id, "Customer");
			}

			db.SaveChanges();

			AppUser customer43 = db.Users.FirstOrDefault(u => u.Email == "taylordjay@aoll.com");

			if (customer43 == null)
			{
				customer43 = new AppUser();
				customer43.UserName = "taylordjay@aoll.com";
				customer43.Email = "taylordjay@aoll.com";
				customer43.FirstName = "Allison";
				customer43.LastName = "Taylor";
				customer43.MiddleInitial = "R.";
				customer43.PhoneNumber = "5125550160";
				customer43.Birthday = new DateTime(1990, 11, 14);
				customer43.Street = "467 Nueces St.";
				customer43.City = "Austin";
				customer43.State = StateAbbr.TX;
				customer43.ZipCode = "78712";
				customer43.PopcornPoints = 110;

				var result = UserManager.Create(customer43,"allyrally");
				db.SaveChanges();
				customer43 = db.Users.First(u => u.UserName == "taylordjay@aoll.com");

			}

			if (RoleManager.RoleExists("Customer") == false)
			{
				RoleManager.Create(new AppRole("Customer"));
			}

			if (UserManager.IsInRole(customer43.Id, "Customer") == false)
			{
				UserManager.AddToRole(customer43.Id, "Customer");
			}

			db.SaveChanges();

			AppUser customer44 = db.Users.FirstOrDefault(u => u.Email == "rtaylor@gogle.com");

			if (customer44 == null)
			{
				customer44 = new AppUser();
				customer44.UserName = "rtaylor@gogle.com";
				customer44.Email = "rtaylor@gogle.com";
				customer44.FirstName = "Rachel";
				customer44.LastName = "Taylor";
				customer44.MiddleInitial = "K.";
				customer44.PhoneNumber = "5125550127";
				customer44.Birthday = new DateTime(1976, 1, 18);
				customer44.Street = "345 Longview Dr.";
				customer44.City = "Austin";
				customer44.State = StateAbbr.TX;
				customer44.ZipCode = "78758";
				customer44.PopcornPoints = 160;

				var result = UserManager.Create(customer44,"taylorbaylor");
				db.SaveChanges();
				customer44 = db.Users.First(u => u.UserName == "rtaylor@gogle.com");

			}

			if (RoleManager.RoleExists("Customer") == false)
			{
				RoleManager.Create(new AppRole("Customer"));
			}

			if (UserManager.IsInRole(customer44.Id, "Customer") == false)
			{
				UserManager.AddToRole(customer44.Id, "Customer");
			}

			db.SaveChanges();

			AppUser customer45 = db.Users.FirstOrDefault(u => u.Email == "teefrank@noclue.com");

			if (customer45 == null)
			{
				customer45 = new AppUser();
				customer45.UserName = "teefrank@noclue.com";
				customer45.Email = "teefrank@noclue.com";
				customer45.FirstName = "Frank";
				customer45.LastName = "Tee";
				customer45.MiddleInitial = "J";
				customer45.PhoneNumber = "5125550161";
				customer45.Birthday = new DateTime(1998, 9, 6);
				customer45.Street = "5590 Lavell Dr";
				customer45.City = "Austin";
				customer45.State = StateAbbr.TX;
				customer45.ZipCode = "78729";
				customer45.PopcornPoints = 70;

				var result = UserManager.Create(customer45,"teeoff22");
				db.SaveChanges();
				customer45 = db.Users.First(u => u.UserName == "teefrank@noclue.com");

			}

			if (RoleManager.RoleExists("Customer") == false)
			{
				RoleManager.Create(new AppRole("Customer"));
			}

			if (UserManager.IsInRole(customer45.Id, "Customer") == false)
			{
				UserManager.AddToRole(customer45.Id, "Customer");
			}

			db.SaveChanges();

			AppUser customer46 = db.Users.FirstOrDefault(u => u.Email == "ctucker@alphabet.co.uk");

			if (customer46 == null)
			{
				customer46 = new AppUser();
				customer46.UserName = "ctucker@alphabet.co.uk";
				customer46.Email = "ctucker@alphabet.co.uk";
				customer46.FirstName = "Clent";
				customer46.LastName = "Tucker";
				customer46.MiddleInitial = "J";
				customer46.PhoneNumber = "5125550106";
				customer46.Birthday = new DateTime(1943, 2, 25);
				customer46.Street = "312 Main St.";
				customer46.City = "Round Rock";
				customer46.State = StateAbbr.TX;
				customer46.ZipCode = "78665";
				customer46.PopcornPoints = 150;

				var result = UserManager.Create(customer46,"tucksack1");
				db.SaveChanges();
				customer46 = db.Users.First(u => u.UserName == "ctucker@alphabet.co.uk");

			}

			if (RoleManager.RoleExists("Customer") == false)
			{
				RoleManager.Create(new AppRole("Customer"));
			}

			if (UserManager.IsInRole(customer46.Id, "Customer") == false)
			{
				UserManager.AddToRole(customer46.Id, "Customer");
			}

			db.SaveChanges();

			AppUser customer47 = db.Users.FirstOrDefault(u => u.Email == "avelasco@yoho.com");

			if (customer47 == null)
			{
				customer47 = new AppUser();
				customer47.UserName = "avelasco@yoho.com";
				customer47.Email = "avelasco@yoho.com";
				customer47.FirstName = "Allen";
				customer47.LastName = "Velasco";
				customer47.MiddleInitial = "G";
				customer47.PhoneNumber = "5125550170";
				customer47.Birthday = new DateTime(1985, 9, 10);
				customer47.Street = "679 W. 4th";
				customer47.City = "Cedar Park";
				customer47.State = StateAbbr.TX;
				customer47.ZipCode = "78613";
				customer47.PopcornPoints = 0;

				var result = UserManager.Create(customer47,"meow88");
				db.SaveChanges();
				customer47 = db.Users.First(u => u.UserName == "avelasco@yoho.com");

			}

			if (RoleManager.RoleExists("Customer") == false)
			{
				RoleManager.Create(new AppRole("Customer"));
			}

			if (UserManager.IsInRole(customer47.Id, "Customer") == false)
			{
				UserManager.AddToRole(customer47.Id, "Customer");
			}

			db.SaveChanges();

			AppUser customer48 = db.Users.FirstOrDefault(u => u.Email == "vinovino@grapes.com");

			if (customer48 == null)
			{
				customer48 = new AppUser();
				customer48.UserName = "vinovino@grapes.com";
				customer48.Email = "vinovino@grapes.com";
				customer48.FirstName = "Janet";
				customer48.LastName = "Vino";
				customer48.MiddleInitial = "E";
				customer48.PhoneNumber = "5125550128";
				customer48.Birthday = new DateTime(1985, 2, 7);
				customer48.Street = "189 Grape Road";
				customer48.City = "Lockhart";
				customer48.State = StateAbbr.TX;
				customer48.ZipCode = "78644";
				customer48.PopcornPoints = 160;

				var result = UserManager.Create(customer48,"vinovino");
				db.SaveChanges();
				customer48 = db.Users.First(u => u.UserName == "vinovino@grapes.com");

			}

			if (RoleManager.RoleExists("Customer") == false)
			{
				RoleManager.Create(new AppRole("Customer"));
			}

			if (UserManager.IsInRole(customer48.Id, "Customer") == false)
			{
				UserManager.AddToRole(customer48.Id, "Customer");
			}

			db.SaveChanges();

			AppUser customer49 = db.Users.FirstOrDefault(u => u.Email == "westj@pioneer.net");

			if (customer49 == null)
			{
				customer49 = new AppUser();
				customer49.UserName = "westj@pioneer.net";
				customer49.Email = "westj@pioneer.net";
				customer49.FirstName = "Jake";
				customer49.LastName = "West";
				customer49.MiddleInitial = "T";
				customer49.PhoneNumber = "2025550170";
				customer49.Birthday = new DateTime(1976, 1, 9);
				customer49.Street = "RR 3287";
				customer49.City = "Austin";
				customer49.State = StateAbbr.TX;
				customer49.ZipCode = "78705";
				customer49.PopcornPoints = 70;

				var result = UserManager.Create(customer49,"gowest");
				db.SaveChanges();
				customer49 = db.Users.First(u => u.UserName == "westj@pioneer.net");

			}

			if (RoleManager.RoleExists("Customer") == false)
			{
				RoleManager.Create(new AppRole("Customer"));
			}

			if (UserManager.IsInRole(customer49.Id, "Customer") == false)
			{
				UserManager.AddToRole(customer49.Id, "Customer");
			}

			db.SaveChanges();

			AppUser customer50 = db.Users.FirstOrDefault(u => u.Email == "winner@hootmail.com");

			if (customer50 == null)
			{
				customer50 = new AppUser();
				customer50.UserName = "winner@hootmail.com";
				customer50.Email = "winner@hootmail.com";
				customer50.FirstName = "Louis";
				customer50.LastName = "Winthorpe";
				customer50.MiddleInitial = "L";
				customer50.PhoneNumber = "2025550141";
				customer50.Birthday = new DateTime(1953, 4, 19);
				customer50.Street = "2500 Padre Blvd";
				customer50.City = "Austin";
				customer50.State = StateAbbr.TX;
				customer50.ZipCode = "78747";
				customer50.PopcornPoints = 150;

				var result = UserManager.Create(customer50,"louielouie");
				db.SaveChanges();
				customer50 = db.Users.First(u => u.UserName == "winner@hootmail.com");

			}

			if (RoleManager.RoleExists("Customer") == false)
			{
				RoleManager.Create(new AppRole("Customer"));
			}

			if (UserManager.IsInRole(customer50.Id, "Customer") == false)
			{
				UserManager.AddToRole(customer50.Id, "Customer");
			}

			db.SaveChanges();

			AppUser customer51 = db.Users.FirstOrDefault(u => u.Email == "rwood@voyager.net");

			if (customer51 == null)
			{
				customer51 = new AppUser();
				customer51.UserName = "rwood@voyager.net";
				customer51.Email = "rwood@voyager.net";
				customer51.FirstName = "Reagan";
				customer51.LastName = "Wood";
				customer51.MiddleInitial = "B.";
				customer51.PhoneNumber = "2025550128";
				customer51.Birthday = new DateTime(2002, 12, 28);
				customer51.Street = "447 Westlake Dr.";
				customer51.City = "Austin";
				customer51.State = StateAbbr.TX;
				customer51.ZipCode = "78753";
				customer51.PopcornPoints = 20;

				var result = UserManager.Create(customer51,"woodyman1");
				db.SaveChanges();
				customer51 = db.Users.First(u => u.UserName == "rwood@voyager.net");

			}

			if (RoleManager.RoleExists("Customer") == false)
			{
				RoleManager.Create(new AppRole("Customer"));
			}

			if (UserManager.IsInRole(customer51.Id, "Customer") == false)
			{
				UserManager.AddToRole(customer51.Id, "Customer");
			}

			db.SaveChanges();

			}
	}
}
