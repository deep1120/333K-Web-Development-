﻿using System;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
//using MIS333K_Team11_FinalProjectV2.DAL;
using System.Collections.Generic;
using System.Text.RegularExpressions;

// Change this using statement to match your project
using MIS333K_Team11_FinalProjectV2.Models;
using static MIS333K_Team11_FinalProjectV2.Models.AppUser;

// Change this namespace to match your project
namespace MIS333K_Team11_FinalProjectV2.Controllers
{
    [Authorize]
    public class AccountsController : Controller
    {
        public enum ManageMessageId
        {
            AddPhoneSuccess,
            ChangePasswordSuccess,
            SetTwoFactorSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess,
            RemovePhoneSuccess,
            DeatilsChangeSuccess,
            CardDeleteSuccess,
            Error
        }

        private ApplicationSignInManager _signInManager;
        private AppUserManager _userManager;
        private AppDbContext db = new AppDbContext();

        public AccountsController()
        {
        }

        //NOTE: This creates a user manager and a sign-in manager every time someone creates a request to this controller
        public AccountsController(AppUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public AppUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<AppUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        // NOTE:  This is the logic for the login page
        // GET: /Accounts/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            if (HttpContext.User.Identity.IsAuthenticated) //user has been redirected here from a page they're not authorized to see
            {
                return View("Error", new string[] { "Access Denied" });
            }
            AuthenticationManager.SignOut(); //this removes any old cookies hanging around
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Accounts/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            try
            {
                AppUser userLoggingIn = db.Users.FirstOrDefault(x => x.Email == model.EmailAddress);
                if(await UserManager.IsInRoleAsync(userLoggingIn.Id, "DisabledEmployee"))
                {
                    return View("Error", new string[] { "Your employee account has been disabled." });
                }
            }
            catch (NullReferenceException e)
            {
                new ApplicationException("Invalid Email", e);
            }

            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, change to shouldLockout: true
            var result = await SignInManager.PasswordSignInAsync(model.EmailAddress, model.Password, model.RememberMe, shouldLockout: false);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(returnUrl);
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid login attempt.");
                    return View(model);
            }
        }

        // GET: /Accounts/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        // NOTE: Here is your logic for registering a new user
        // POST: /Accounts/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                //TODO: Add fields to user here so they will be saved to do the database
                var user = new AppUser
                {
                    UserName = model.EmailAddress,
                    Email = model.EmailAddress,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    MiddleInitial = model.MiddleInitial,
                    PhoneNumber = model.PhoneNumber,
                    Birthday = model.Birthday,
                    Street = model.Street,
                    City = model.City,
                    State = model.State,
                    ZipCode = model.ZipCode,
                    PopcornPoints = model.PopcornPoints             
                };

                var result = await UserManager.CreateAsync(user, model.Password);

                //TODO:  Once you get roles working, you may want to add users to roles upon creation
                //await UserManager.AddToRoleAsync(user.Id, "Customer");
                // --OR--
                //await UserManager.AddToRoleAsync(user.Id, "Employee");

                if (result.Succeeded)
                {
                    if(User.IsInRole("Manager"))
                    {
                        await UserManager.AddToRoleAsync(user.Id, "Employee");
                        SendEmployeeEmail(user);
                    }
                    else
                    {
                        await UserManager.AddToRoleAsync(user.Id, "Customer");
                        SendRegisterEmail(user);
                            await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                    }
                    
                    // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                    // Send an email with this link
                    // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                    // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                    // await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");

                    return RedirectToAction("Index", "Home");
                }
                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        // POST: /Accounts/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Index", "Home");
        }

        //Logic for change password
        // GET: /Accounts/ChangePassword
        public ActionResult ChangePassword()
        {
            return View();
        }

        //
        // POST: /Accounts/ChangePassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = await UserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword, model.NewPassword);
            if (result.Succeeded)
            {
                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                if (user != null)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                }
                return RedirectToAction("Index", "Home");
            }
            AddErrors(result);
            return View(model);
        }

        //GET: /Account/SetPassword
        public ActionResult SetPassword(string id)
        {
            var userId = id;
            if (string.IsNullOrEmpty(id))
            {
                userId = User.Identity.GetUserId();
            }
            var model = new SetPasswordViewModel
            {
                UserID = userId
            };

            return View(model);
        }

        //POST: /Account/SetPassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SetPassword(SetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            UserManager.RemovePassword(model.UserID);
            var result = await UserManager.AddPasswordAsync(model.UserID, model.NewPassword);
            if(result.Succeeded)
            {
                var user = await UserManager.FindByIdAsync(model.UserID);
                if (user != null)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                }
                return RedirectToAction("Index", new { Message = ManageMessageId.SetPasswordSuccess });
            }
            AddErrors(result);
            return View(model);
        }

        //GET: /Account/Index
        public ActionResult Index(string id, ManageMessageId? message)
        {
            ViewBag.StatusMessage =
                message == ManageMessageId.ChangePasswordSuccess ? "Password has been changed successfully."
                : message == ManageMessageId.SetPasswordSuccess ? "Password has been set successfully."
                : message == ManageMessageId.SetTwoFactorSuccess ? "Your two-factor authentication provider has been set."
                : message == ManageMessageId.Error ? "An error has occurred."
                : message == ManageMessageId.AddPhoneSuccess ? "Your phone number was added."
                : message == ManageMessageId.RemovePhoneSuccess ? "Your phone number was removed."
                : message == ManageMessageId.DeatilsChangeSuccess ? "Account Details have been changed successfully."
                : message == ManageMessageId.CardDeleteSuccess ? "Card was deleted successfully."
                : "";

            var userId = id;
            if (string.IsNullOrEmpty(id))
            {
                userId = User.Identity.GetUserId();
            }

            var user = db.Users.SingleOrDefault(u => u.Id == userId);

            var model = new UserModel()
            {
                FirstName = user.FirstName,
                MiddleInitial = user.MiddleInitial,
                LastName = user.LastName,
                City = user.City,
                State = user.State,
                EmailAddress = user.Email,
                PhoneNumber = user.PhoneNumber,
                Street = user.Street,
                UserModelID = user.Id,
                ZipCode = user.ZipCode,
                HasPassword = (user.PasswordHash != null),
                Cards = user.Cards,
                Orders = user.Orders,
            };

            if(UserManager.IsInRole(user.Id, "Manager"))
            {
                model.Role = "Manager";
            }
            else if
            (UserManager.IsInRole(user.Id, "Employee"))
            {
                model.Role = "Employee";
            }
            else
            {
                model.Role = "Customer";
            }
            return View(model);          
        }

        //GET: Edit Account Details
        public ActionResult Edit(string id)
        {
            var userId = id;
            if (string.IsNullOrEmpty(id))
            {
                userId = User.Identity.GetUserId();
            }
            var user = db.Users.SingleOrDefault(u => u.Id == userId);
            var model = new UserModel()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                City = user.City,
                State = user.State,
                EmailAddress = user.Email,
                PhoneNumber = user.PhoneNumber,
                Street = user.Street,
                UserModelID = user.Id,
                ZipCode = user.ZipCode,
                HasPassword = (user.PasswordHash != null),
                Cards = user.Cards,
            };

            if(UserManager.IsInRole(user.Id, "Manager"))
            {
                model.Role = "Manager";
            }
            else if 
            (UserManager.IsInRole(user.Id, "Employee"))
            {
                model.Role = "Employee";
            }
            else
            {
                model.Role = "Customer";
            }
            return View(model);
        }

        //POST: Edit Account Details
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "FirstName,LastName,Email,PhoneNumber,Street,ZipCode,City,State,UserID")] UserModel model)
        {
            if (ModelState.IsValid)
            {
                var user = UserManager.FindById(model.UserModelID);

                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.Email = model.EmailAddress;
                user.PhoneNumber = model.PhoneNumber;
                user.Street = model.Street;
                user.ZipCode = model.ZipCode;
                user.City = model.City;
                user.State = model.State;

                var result = await UserManager.UpdateAsync(user);
                if (!result.Succeeded)
                {
                    AddErrors(result);
                    return RedirectToAction("Edit", new { Id = model.UserModelID });
                }
                return RedirectToAction("Index", new { Id = model.UserModelID, message = ManageMessageId.DeatilsChangeSuccess });
            }
            return View(model);
        }

        [Authorize(Roles = "Employee, Manager")]
        public ActionResult Customers()
        {
            var role = db.Roles.FirstOrDefault(x => x.Name == "Customer");
            var users = db.Users
            .Where(x => x.Roles.Any(y => y.RoleId.Equals(role.Id)))
            .ToList();
            return View(users);
        }

        [Authorize(Roles = "Manager")]
        public ActionResult Employees()
        {
            var role = db.Roles.FirstOrDefault(x => x.Name == "Employee");
            var users = db.Users
            .Where(x => x.Roles.Any(y => y.RoleId.Equals(role.Id)))
            .ToList();
            return View(users);
        }

        [Authorize]
        public ActionResult AddCard()
        {
            AppUser user = UserManager.FindById(User.Identity.GetUserId());
            var model = new Card() { AppUserId = user.Id, AppUser = user };
            return View(model);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddCard([Bind(Include = "CardID,AppUserId,CardNumber,Type,ExpDate,CVV")] Card card)
        {
            if (ModelState.IsValid)
            {
                db.Cards.Add(card);
                db.SaveChanges();
                return RedirectToAction("Index", new { id = card.AppUserId });
            }
            return View(card);
        }

        [Authorize]
        public ActionResult DeleteCard(int Id)
        {
            var card = db.Cards.SingleOrDefault(c => c.CardID == Id);
            if(card !=null)
            {
                var userID = card.AppUserId;
                db.Cards.Remove(card);
                db.SaveChanges();
                return RedirectToAction("Index", new { id = userID, message = ManageMessageId.CardDeleteSuccess });
            }
            return RedirectToAction("Index", new { id = User.Identity.GetUserId() });
        }

        //Method to edit card information
        public ActionResult CardEdit()
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            return View();
        }

        private bool HasPassword()
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            if (user != null)
            {
                return user.PasswordHash != null;
            }
            return false;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_userManager != null)
                {
                    _userManager.Dispose();
                    _userManager = null;
                }

                if (_signInManager != null)
                {
                    _signInManager.Dispose();
                    _signInManager = null;
                }
            }

            base.Dispose(disposing);
        }

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if(Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        private bool AuthorizedToEdit(string id)
        {
            if(id != User.Identity.GetUserId())
            {
                if(UserManager.IsInRole(id, "Customer") && !(User.IsInRole("Employee") || User.IsInRole("Manager")))
                {
                    return false;
                }
                else if (UserManager.IsInRole(id, "Employee") && !User.IsInRole("Manager"))
                {
                    return false;
                }
            }
            return true;
        }

        private void SendRegisterEmail(AppUser user)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Dear {user.FirstName},<br/><br/>");
            sb.AppendLine("Welcome to Team 11's Movie Theatre!");
            EmailMessaging.SendEmail(user.Email, "Welcome to Team 11's Movie Theatre", sb.ToString());
        }

        private void SendEmployeeEmail(AppUser user)
        {
            var body = $@"Dear {user.FirstName}, you have been registered as an employee";
            EmailMessaging.SendEmail(user.Email, "Team 11 Employee Registration Confirmation", body);
        }

        ////GET: Accounts/Index
        //public ActionResult Index()
        //{
        //    IndexViewModel ivm = new IndexViewModel();

        //    //get user info
        //    String id = User.Identity.GetUserId();
        //    AppUser user = db.Users.Find(id);

        //    //populate the view model
        //    ivm.Email = user.Email;
        //    ivm.HasPassword = true;
        //    ivm.UserID = user.Id;
        //    ivm.UserName = user.UserName;
        //    ivm.Street = user.Street;
        //    ivm.State = user.State;
        //    ivm.City = user.City;
        //    ivm.State = user.State;
        //    ivm.ZipCode = user.ZipCode;
        //    ivm.PhoneNumber = user.PhoneNumber;
        //    ivm.Birthday = user.Birthday;
        //    ivm.PopcornPoints = user.PopcornPoints;  

        //    return View(ivm);
        //}
    }
}