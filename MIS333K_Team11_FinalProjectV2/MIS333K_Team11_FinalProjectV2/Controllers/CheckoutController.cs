//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using System.Web.Mvc;
//using MIS333K_Team11_FinalProjectV2.Models;
//using Microsoft.AspNet.Identity;
//using System.Data.Entity;
//using Microsoft.AspNet.Identity.Owin;
//using System.Text;
//using static MIS333K_Team11_FinalProjectV2.Models.AppUser;

//namespace MIS333K_Team11_FinalProjectV2.Controllers
//{
//    public class CheckoutController : Controller
//    {
//        // GET: Checkout
//        public ActionResult Index()
//        {
//            return View();
//        }

//        AppDbContext db = new AppDbContext();
//        private ApplicationSignInManager _signInManager;
//        private AppUserManager _userManager;

//        public CheckoutController()
//        {

//        }

//        public CheckoutController(AppUserManager userManager, ApplicationSignInManager signInManager)
//        {
//            UserManager = userManager;
//            SignInManager = signInManager;
//        }

//        public ApplicationSignInManager SignInManager
//        {
//            get
//            {
//                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
//            }

//            private set
//            {
//                _signInManager = value;
//            }
//        }

//        public AppUserManager UserManager
//        {
//            get
//            {
//                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<AppUserManager>();
//            }

//            private set
//            {
//                _userManager = value;
//            }      
//        }

//        // GET: Checkout
//        public ActionResult NewOrder()
//        {
//            // Set up our ViewModel
//            var model = new CheckoutViewModel();

//            model.IsGift = false;
//            PopulateViewModel(model);
//            return View(model);
//        }

//        private void PopulateViewModel(CheckoutViewModel model)
//        {
//            var cart = ShoppingCart.GetCart(this.HttpContext);

//            // Set up our ViewModel

//            model.Subtotal = cart.GetTotal().ToString("C");
//            model.Tax = cart.GetTax().ToString("C");
//            model.Total = cart.GrandTotal().ToString("C");

//            model.ListOfCards = new List<SelectListItem>();

//            var userId = User.Identity.GetUserId();
//            var user = db.Users.Include(x => x.Cards).Single(x => x.Id == userId);

//            if (user != null)
//            {

//                foreach (var card in user.Cards)
//                {
//                    model.ListOfCards.Add(new SelectListItem { Text = $"{card.CardNumber} {card.Type.ToString()}", Value = $"{card.CardNumber} {card.Type.ToString()}" });
//                }
//            }
//        }

//        // POST: /Checkout/AddressAndPayment
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public ActionResult NewOrder(CheckoutViewModel model, string CardOption)
//        {
//            if (!ModelState.IsValid)
//            {
//                PopulateViewModel(model);
//                return View(model);
//            }
//            /*
//            if (CardOption == "NewCard")
//            {
//                Card card = new Card();
//                card.AppUser = db.Users.Find(model.AppUserId);
//                card.AppUserId = model.AppUserId;
//                card.CardNumber = model.CardNumber;
//                card.CVV = model.CVV;
//                card.ExpDate = model.ExpDate;
//                card.Type = model.Type;
//            }
//            */
//            var errorMessage = ValidateModelAndGetErrorMessage(model);
//            if (!string.IsNullOrEmpty(errorMessage))
//            {
//                PopulateViewModel(model);
//                ViewBag.ErrorMessage = errorMessage;
//                return View(model);
//            }

//            TempData["checkout"] = model;
//            return RedirectToAction("Review");
//        }

//        [Authorize]
//        public ActionResult AddCard()
//        {
//            AppUser user = UserManager.FindById(User.Identity.GetUserId());
//            var model = new Card() { AppUserId = user.Id, AppUser = user };
//            return View(model);
//        }

//        [Authorize]
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public ActionResult AddCard([Bind(Include = "CardID,AppUserId,CardNumber,Type,ExpDate,CVV")]Card card)
//        {
//            if (ModelState.IsValid)
//            {
//                db.Cards.Add(card);
//                db.SaveChanges();
//                return RedirectToAction("NewOrder");
//            }
//            return View(card);
//        }

//        private string ValidateModelAndGetErrorMessage(CheckoutViewModel model)
//        {
//            var errorMessage = string.Empty;

//            if (model.IsGift == true)
//            {
//                if (model.GiftEmail == null)
//                {
//                    errorMessage = $"Please enter the recipient's email";
//                }
//                else
//                {
//                    var giftEmail = model.GiftEmail;
//                    var giftReceiver = UserManager.FindByEmail(giftEmail);
//                    if (giftReceiver == null) { errorMessage = $"No user record was found with the email : {model.GiftEmail}"; }
//                }

//            }
//            return errorMessage;
//        }

//        public ActionResult Review()
//        {
//            var model = TempData["checkout"] as CheckoutViewModel;
//            var cart = ShoppingCart.GetCart(this.HttpContext);

//            // Set up our ViewModel

//            var viewModel = new ReviewViewModel
//            {
//                CartItems = cart.GetCartItems(),
//                CartTotal = cart.GetTotal().ToString("C"),
//                Tax = cart.GetTax().ToString("C"),
//                Total = cart.GrandTotal().ToString("C")
//            };

//            if (model.CardOption == "NewCard")
//            {
//                viewModel.CardNumber = model.CardNumber + " " + model.Type;
//            }

//            else
//            {
//                viewModel.CardNumber = model.SelectedCardNumber;
//            }

//            viewModel.GiftEmail = model.GiftEmail;
//            viewModel.IsGift = model.IsGift;
//            // Return the view
//            return View(viewModel);

//        }

//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public ActionResult Review(ReviewViewModel model)
//        {
//            var order = new Order();

//            //order.Username = User.Identity.Name;

//            order.OrderDate = DateTime.Now;

//            //Save Order
//            order.AppUserId = User.Identity.GetUserId();
//            order.CardNumber = model.CardNumber;
//            order.IsGift = model.IsGift;
//            order.GiftEmail = model.GiftEmail;

//            db.Orders.Add(order);
//            db.SaveChanges();

//            //Process the order
//            var cart = ShoppingCart.GetCart(this.HttpContext);
//            var listOfItems = GetListOfPurchasedItem(cart.GetCartItems());

//            order = cart.CreateOrder(order);
//            db.Entry(order).State = EntityState.Modified;
//            db.SaveChanges();

//            var user = UserManager.FindById(User.Identity.GetUserId());
//            if (string.IsNullOrEmpty(model.GiftEmail))
//            {
//                SendEmailToUser(user, listOfItems);
//            }

//            else
//            {
//                SendEmailToGiftReciever(model.GiftEmail, user, listOfItems);
//                SendEmailToGiftPurchaser(user);
//            }

//            return RedirectToAction("Complete", new { id = order.OrderID });
//            //return View(model);

//        }

//        private void SendEmailToGiftPurchaser(AppUser user)
//        {
//            var body = $@"Dear {user.FirstName}, thanks for ordering! Your gift has been sent.";
//            EmailMessaging.SendEmail(user.Email, "Order Confirmation", body);
//        }

//        private string GetListOfPurchasedItem(List<Cart> cartItem)
//        {
//            var body = new StringBuilder();

//            foreach (var item in cartItem)
//            {
//                body.Append(((item.AlbumID != null) ? item.Album.AlbumTitle : item.Song.SongTitle));
//                if (cartItem.IndexOf(item) < cartItem.Count() - 1)
//                {
//                    body.Append(", ");
//                }
//            }
//            return body.ToString();

//        }

//        private void SendEmailToGiftReciever(string giftEmail, AppUser user, string listOfItem)
//        {

//            var giftReceiver = UserManager.FindByEmail(giftEmail);
//            var body = new StringBuilder();
//            body.AppendLine($@"Dear {giftReceiver.FirstName},<br/><br/>You have recieved the following gift from {user.FirstName}:<br/>");
//            body.AppendLine(listOfItem);
//            EmailMessaging.SendEmail(giftReceiver.Email, "You have recieved a gift from Longhorn Music.", body.ToString());

//        }

//        private void SendEmailToUser(AppUser user, string listOfItem)
//        {
//            var body = new StringBuilder();
//            body.AppendLine($@"Dear {user.FirstName},<br/><br/>Thanks for ordering! You have purchased the following items:<br/>");
//            body.AppendLine(listOfItem);
//            EmailMessaging.SendEmail(user.Email, "Order Confirmation", body.ToString());
//        }

//        // GET: /Checkout/Complete
//        public ActionResult Complete(int id)
//        {
//            // Validate customer owns this order
//            var userId = User.Identity.GetUserId();
//            bool isValid = db.Orders.Any(
//            o => o.OrderID == id &&
//            o.AppUserId == userId);

//            if (isValid)
//            {
//                return View(id);
//            }

//            else
//            {
//                return View("Error");
//            }

//        }

//    }
//}

