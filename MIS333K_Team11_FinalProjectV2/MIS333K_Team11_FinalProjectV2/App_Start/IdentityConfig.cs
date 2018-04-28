using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;

// Change this using statement to match your project
//using MIS333K_Team11_FinalProjectV2.DAL;
using MIS333K_Team11_FinalProjectV2.Models;
using static MIS333K_Team11_FinalProjectV2.Models.AppUser;

// Change this namespace to match your project
namespace MIS333K_Team11_FinalProjectV2
{

    // Configure the application user manager used in this application. UserManager is defined in ASP.NET Identity and is used by the application.
    public class AppUserManager : UserManager<AppUser>
    {
        public AppUserManager(IUserStore<AppUser> store)
            : base(store)
        {
        }

        //  Change any settings related to your user password rules, etc.
        public static AppUserManager Create(IdentityFactoryOptions<AppUserManager> options, IOwinContext context)
        {
            var manager = new AppUserManager(new UserStore<AppUser>(context.Get<AppDbContext>()));
            // Configure validation logic for usernames
            manager.UserValidator = new UserValidator<AppUser>(manager)
            {

                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };

            // TODO: Configure validation logic for passwords
            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 5,
                RequireNonLetterOrDigit = false,
                RequireDigit = false,
                RequireLowercase = false,
                RequireUppercase = false
            };
            return manager;
        }
    }

    // Configure the application sign-in manager which is used in this application.
    public class ApplicationSignInManager : SignInManager<AppUser, string>
    {
        public ApplicationSignInManager(AppUserManager userManager, IAuthenticationManager authenticationManager)
            : base(userManager, authenticationManager)
        {
        }

        public override Task<ClaimsIdentity> CreateUserIdentityAsync(AppUser user)
        {
            return user.GenerateUserIdentityAsync((AppUserManager)UserManager);
        }

        public static ApplicationSignInManager Create(IdentityFactoryOptions<ApplicationSignInManager> options, IOwinContext context)
        {
            return new ApplicationSignInManager(context.GetUserManager<AppUserManager>(), context.Authentication);
        }
    }
}