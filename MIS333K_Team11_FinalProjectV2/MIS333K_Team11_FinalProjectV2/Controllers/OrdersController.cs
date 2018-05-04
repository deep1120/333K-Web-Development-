using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MIS333K_Team11_FinalProjectV2.Models;
using MIS333K_Team11_FinalProjectV2.Utilities;
using MIS333K_Team11_FinalProjectV2.ViewModel;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using static MIS333K_Team11_FinalProjectV2.Models.AppUser;

namespace MIS333K_Team11_FinalProjectV2.Controllers
{
    public class OrdersController : Controller
    {
        private AppDbContext db = new AppDbContext();
        private ApplicationSignInManager _signInManager;
        private AppUserManager _userManager;

        public OrdersController()
        {
        }

        public OrdersController(AppUserManager userManager, ApplicationSignInManager signInManager)
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

        // GET: Orders
        [Authorize(Roles = "Customer, Manager")]
        public ActionResult Index()
        {
            if (User.IsInRole("Manager"))
            {
                return View(db.Orders.ToList());
            }
            else
            {
                String UserID = User.Identity.GetUserId();
                List<Order> Orders = db.Orders.Where(o => o.OrderAppUser.Id == UserID).ToList();
                return View(Orders);
            }
        }

        // GET: Orders/Details/5
        [Authorize(Roles = "Customer, Manager")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            if (User.IsInRole("Manager") || order.OrderAppUser.Id == User.Identity.GetUserId())
            {
                return View(order);
            }
            else
            {
                return View("Error", new string[] { "This is not your order!" });
            }
        }

        // GET: Orders/Create
        [Authorize(Roles = "Customer")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Orders/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Customer")]
        public ActionResult Create([Bind(Include = "OrderID,OrderNumber,OrderDate,Orderstatus")] Order order)
        {
            //Find next order number
            order.OrderNumber = Utilities.GenerateNextOrderNumber.GetNextOrderNumber();

            //record date of order
            order.OrderDate = DateTime.Today;
            order.Orderstatus = OrderStatus.Pending;
            order.OrderAppUser = db.Users.Find(User.Identity.GetUserId());

            if (ModelState.IsValid)
            {
                db.Orders.Add(order);
                db.SaveChanges();
                return RedirectToAction("AddShowing", new { OrderID = order.OrderID });
            }
            return View(order);
        }

        //GET
        [Authorize(Roles = "Customer")]
        public ActionResult AddShowing(int OrderID)
        {
            //Ticket td = new Ticket();
            Order ord = db.Orders.Find(OrderID);
            //td.Order = ord;
            ViewBag.AllShowings = GetAllShowings();
            return View(ord); //td?
        }

        [HttpPost]
        [Authorize(Roles = "Customer")]
        public ActionResult AddShowing([Bind(Include = "OrderID,OrderNumber,ConfirmationNumber,EarlyDiscount,SeniorDiscount,OrderDate,CardNumber,Orderstatus,OrderSubtotal,SalesTax,OrderTotal,Gift,GiftEmail")] Order ord, int SelectedShowing)
        {
            Showing showing = db.Showings.Find(SelectedShowing);
            Order order = db.Orders.Find(ord.OrderID);
            order.Orderstatus = OrderStatus.Pending;
            Ticket td = new Ticket();
            td.Order = order;
            td.Showing = showing;

            if (ModelState.IsValid)
            {
                db.Tickets.Add(td);
                db.SaveChanges();
                return RedirectToAction("AddToOrder", new { OrderID = order.OrderID, TicketID = td.TicketID });
            }

            //model state is not valid; repopulate viewbags and return
            ViewBag.AllShowings = GetAllShowings();
            return View(order);
        }

        [Authorize(Roles = "Customer")]
        public ActionResult AddToOrder(int OrderID, int TicketID)
        {

            Ticket td = db.Tickets.Find(TicketID);
            Order ord = db.Orders.Find(OrderID);
            td.Order = ord;
            ViewBag.AllSeats = GetAllTicketSeats(td.Showing.ShowingID); //add in SelectedShowing into the parameter 

            return View(td);
            //return RedirectToAction("Checkout", new { OrderID = td.Order.OrderID });
        }

        [HttpPost]
        [Authorize(Roles = "Customer")]
        public ActionResult AddToOrder([Bind(Include = "TicketID,OrderID,MovieID,Quantity,Subtotal,TicketPrice,TicketSeat,TotalFees")] Ticket td, string SelectedTicket)
        {
            //Showing showing = db.Showings.Find(SelectedShowing);
            Ticket ticket = db.Tickets.Find(td.TicketID);
            //find the order 
            Order ord = db.Orders.Find(ticket.OrderID);

            //LOGIC HERE TO STOP USER FROM ADDING A SHOWING THAT OVERLAPS WITH A ANOTHER SHOWING IN CART
            //List<Showing> CurrentShowingsInCart = db.Showings.Where(o=>o.Tickets.....

            ticket.TicketSeat = SelectedTicket;
            ticket.Order = ord;
            ticket.Order.Orderstatus = OrderStatus.Pending;
            ticket.TicketPrice = ticket.Showing.TicketPrice;

            TimeSpan diff = ticket.Showing.ShowDate - ord.OrderDate;
            if (diff.Days >= 2)
            {
                ord.EarlyDiscount = 1.00m;
                ticket.TicketPrice = ticket.Showing.TicketPrice - ord.EarlyDiscount;
            }
            else ord.EarlyDiscount = 0;

            TimeSpan diff2 = DateTime.Today - ord.OrderAppUser.Birthday;
            if (((diff2.Days) / 365) >= 60)
            {
                ord.SeniorDiscount = 2.00m;
                ticket.TicketPrice = ticket.Showing.TicketPrice - ord.SeniorDiscount;
            }
            else ord.SeniorDiscount = 0;

            //both senior discount and early discount
            if (diff.Days >= 2 && ((diff2.Days) / 365) >= 60)
            {
                ord.EarlyDiscount = 1.00m;
                ord.SeniorDiscount = 2.00m;
                ticket.TicketPrice = ticket.Showing.TicketPrice - ord.SeniorDiscount - ord.EarlyDiscount;
            }

            //if customer is younger than 18, can't purchase a R movie
            //TimeSpan rating = DateTime.Today - ord.OrderAppUser.Birthday;
            //if ((((rating.Days) / 365) < 18) && ticket.Movie.MPAAratings == MPAArating.R)
            //{
            //    ViewBag.YoungErrorMessage = "You must be at least 18 years old to purchase an R-rated movie";
            //    ViewBag.AllSeats = GetAllTicketSeats(ticket.Showing.ShowingID);
            //    return View(ticket);
            //}

            //cant purchase tickets that have already started
            if (ticket.Showing.ShowDate < DateTime.Now)
            {
                ViewBag.ShowingAlreadyStarted = "You cannot purchase a ticket to this showing. The showing has already started";
                ViewBag.AllSeats = GetAllTicketSeats(ticket.Showing.ShowingID);
                return View(ticket);
            }

            //cant purchase tickets that have overlap with another ticket in the order
            List<Ticket> TicketsInOrder = db.Tickets.Where(t => t.Order.OrderID == ord.OrderID).ToList();
            DateTime ticket_start = ticket.Showing.ShowDate;
            DateTime ticket_end = ticket.Showing.ShowDate.AddMinutes(ticket.Showing.SponsoringMovie.RunningTime);

            //foreach (Ticket tick in TicketsInOrder)
            //{
            //    DateTime tick_start = tick.Showing.ShowDate;
            //    DateTime tick_end = tick.Showing.ShowDate.AddMinutes(tick.Showing.SponsoringMovie.RunningTime);

            //    //showing_start >= sh_end || showing_end <= sh_start
            //    if (ticket_start <= tick_end || tick_start >= ticket_end)     //I think it might be diff/other way around 
            //    {
            //        ViewBag.Overlap = "You cannot purchase tickets that have overlapping times";
            //        ViewBag.AllSeats = GetAllTicketSeats(ticket.Showing.ShowingID);
            //        return View(ticket);
            //    }
            //}


            if (ModelState.IsValid)
            {
                db.Entry(ticket).State = EntityState.Modified;
                db.SaveChanges();
                //redirect to edit so that they can continue adding to cart if they want to 
                return RedirectToAction("Edit", "Orders", new { id = ord.OrderID });
            }

            //model state is not valid; repopulate viewbags and return
            ViewBag.AllSeats = GetAllTicketSeats(ticket.Showing.ShowingID);
            return View(ticket);

        }

        [Authorize(Roles = "Customer")]
        public ActionResult Gift(Order order)
        {
            order.Gift = false;
            return View(order);
        }

        [HttpPost]
        [Authorize(Roles = "Customer")]
        public ActionResult Gift(bool Gift, String GiftEmail)
        {
            var errorMessage = string.Empty;

            if (Gift == true)
            {
                if (GiftEmail == null)
                {
                    errorMessage = $"Please enter the recipient's email";
                    ViewBag.ErrorMessage = errorMessage;
                }
                else
                {
                    var giftEmail = GiftEmail;
                    var giftReceiver = UserManager.FindByEmail(giftEmail);
                    if (giftReceiver == null) { errorMessage = $"No user record was found with this email."; }
                    ViewBag.ErrorMessage = errorMessage;
                }
            }
            return RedirectToAction("Checkout");
        }

        [Authorize(Roles = "Customer")]
        public ActionResult Checkout(int OrderID)
        {
            ViewBag.AllCards = GetAllCards();
            //when in edit view page, user clicks checkout and then and will pass the whole order object over to Checkout method
            Order ord = db.Orders.Find(OrderID);
            return View(ord);
        }

        [HttpPost]
        [Authorize(Roles = "Customer")]
        public ActionResult Checkout([Bind(Include = "OrderID,OrderNumber,ConfirmationNumber,EarlyDiscount,SeniorDiscount,OrderDate,CardNumber,Orderstatus,OrderSubtotal,SalesTax,OrderTotal,Gift,GiftEmail")] Order ord/*, int SelectedCards*/) //without bind...might not include stuff
        {
            //Order od = db.Orders.Include(OD => OD.Tickets)
            //                    .FirstOrDefault(x => x.OrderID == ord.OrderID);
            Order od = db.Orders.Find(ord.OrderID);
            //do we add confirmation number here as well??

            List<Ticket> TicketsInOrder = od.Tickets.ToList();
            Int32 CheckoutCount = TicketsInOrder.Count();

            //if(CheckoutCount == 0)
            //{
            //    ViewBag.EmptyCart = "You cannot checkout because you have no items in your cart";
            //    return View(od);
            //}

            //var TicketArray = TicketsInOrder.ToArray();
            //for(int i = 0; i<TicketArray.Length - 1; i++)
            //{
            //    String firstmovie = TicketArray[i].Movie.MovieTitle;
            //    String secondmovie = TicketArray[i + 1].Movie.MovieTitle;

            //    if(firstmovie == secondmovie)
            //    {
            //        ViewBag.SameMovieError = "You cannot have duplicate movies in cart";
            //        return View(od);
            //    }
            //}

            if (ModelState.IsValid)
            {
                od.ConfirmationNumber = Utilities.GenerateNextConfirmationNumber.GetNextConfirmation(); 
                //od.Tickets = ord.Tickets;
                od.Orderstatus = OrderStatus.Completed;
                db.Entry(od).State = EntityState.Modified;
                db.SaveChanges();
                var user = UserManager.FindById(User.Identity.GetUserId());
                SendCompleteCheckoutEmail(user);
                return RedirectToAction("Confirm");
            }
            return View(od);
        }

        private void SendCompleteCheckoutEmail(AppUser user)
        {
            var body = $@"Dear {user.FirstName}, you have placed an order and completed checkout";
            EmailMessaging.SendEmail(user.Email, "Team 11 Order Completed Confirmation", body);
        }

        [Authorize(Roles = "Customer")]
        public ActionResult Confirm()
        {
            return View();
        }

        // GET: Orders/Edit/5
        [Authorize(Roles = "Customer")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Orders/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Customer")]
        public ActionResult Edit([Bind(Include = "OrderID,OrderNumber,ConfirmationNumber,EarlyDiscount,SeniorDiscount,OrderDate,CardNumber,Orderstatus,OrderSubtotal,SalesTax,OrderTotal,Gift,GiftEmail")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(order);
        }

        [Authorize(Roles = "Customer")]
        public ActionResult RemoveFromOrder(int OrderID)
        {
            Order ord = db.Orders.Find(OrderID);

            if (ord == null) //order is not found
            {
                return RedirectToAction("Details", new { id = OrderID });
            }

            if (ord.Tickets.Count == 0) //There are no registration details
            {
                return RedirectToAction("Details", new { id = OrderID });
            }

            //pass the list of order details to the view
            return View(ord.Tickets);
        }
        // GET: Orders/Delete/5
        [Authorize(Roles = "Customer")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Customer")]
        public ActionResult DeleteConfirmed(int id)
        {
            Order order = db.Orders.Find(id);
            order.Orderstatus = OrderStatus.Cancelled;
            db.SaveChanges();
            var user = UserManager.FindById(User.Identity.GetUserId());
            SendOrderCancelEmail(user);
            return RedirectToAction("Index");
        }

        private void SendOrderCancelEmail(AppUser user)
        {
            var body = $@"Dear {user.FirstName}, you have cancelled your order";
            EmailMessaging.SendEmail(user.Email, "Team 11 Order Cancelled Confirmation", body);
        }

        [Authorize(Roles = "Customer")]
        public ActionResult StatusSearch()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Customer")]
        public ActionResult StatusSearch([Bind(Include = "OrderStatus,OrderID,OrderNumber,ConfirmationNumber,EarlyDiscount,SeniorDiscount,OrderDate,CardNumber,Total,Orderstatus,OrderSubtotal,SalesTax,OrderTotal,Gift,GiftEmail")] OrderStatus SelectOrderStatus)
        {
            var query = from o in db.Orders
                        select o;

            switch (SelectOrderStatus)
            {
                case OrderStatus.Pending:
                    query = query.Where(o => o.Orderstatus == OrderStatus.Pending);
                    break;
                case OrderStatus.Completed:
                    query = query.Where(o => o.Orderstatus == OrderStatus.Completed);
                    break;
                case OrderStatus.Cancelled:
                    query = query.Where(o => o.Orderstatus == OrderStatus.Cancelled);
                    break;
            }

            List<Order> SelectedOrders = query.ToList();
            return View("Index", SelectedOrders);

        }

        [Authorize(Roles = "Manager")]
        public ActionResult Reports()
        {
            return View();
        }

        //GET: TicketReport
        [Authorize(Roles = "Manager")]
        public ActionResult TicketReport() //this simply gives the overall report(total tickets sold and total revenue for all orders confirmed) 
        {
            List<Ticket> allTickets = db.Tickets.Where(t => t.Order.Orderstatus == OrderStatus.Completed).ToList();
            List<Order> allOrders = db.Orders.Where(o => o.Orderstatus == OrderStatus.Completed).ToList();
            MovieReportVM tivm = new MovieReportVM();

            Decimal tax = 0.0825m;
            foreach (Ticket td in allTickets)
            {
                tivm.NumberOfPurchase += 1;
                tivm.Revenue += (td.TicketPrice + (td.TicketPrice * tax));
            }
            return View(tivm);
        }

        [Authorize(Roles = "Manager")]
        public ActionResult ReportSearch()
        {
            ViewBag.AllShowingMovies = GetAllShowingMovies();
            return View();
        }

        [Authorize(Roles = "Manager")]
        public ActionResult DisplaySearchResults(DateTime? FirstDate, DateTime? SecondDate, int[] SearchShowingMovies, 
                                                 MPAArating SelectedMPAARating/*, DateTime? FirsTime, DateTime? SecondTime*/)
        {
            var query = from t in db.Tickets
                        where t.Order.Orderstatus == OrderStatus.Completed
                        select t;

            if (SearchShowingMovies != null)
            {
                foreach (int MovieID in SearchShowingMovies)
                {
                    Movie MovieToFind = db.Movies.Find(MovieID);
                    query = query.Where(t => t.Showing.SponsoringMovie.MovieID == MovieToFind.MovieID);
                }
            }

            switch (SelectedMPAARating)
            {
                case MPAArating.G:
                    query = query.Where(t => t.Movie.MPAAratings == MPAArating.G);
                    break;

                case MPAArating.PG:
                    query = query.Where(t => t.Movie.MPAAratings == MPAArating.PG);
                    break;

                case MPAArating.PG13:
                    query = query.Where(t => t.Movie.MPAAratings == MPAArating.PG13);
                    break;

                case MPAArating.R:
                    query = query.Where(t => t.Movie.MPAAratings == MPAArating.R);
                    break;

                case MPAArating.Unrated:
                    query = query.Where(t => t.Movie.MPAAratings == MPAArating.Unrated);
                    break;

                case MPAArating.All:
                    break;
            }

            if (FirstDate != null && SecondDate != null)
            {
                query = query.Where(t => DbFunctions.TruncateTime(t.Showing.ShowDate) >= FirstDate && DbFunctions.TruncateTime(t.Showing.ShowDate) <= SecondDate);
            }

            List<Ticket> SelectedTickets = query.ToList();

            //List<Ticket> allTickets = db.Tickets.Where(t => t.Order.Orderstatus == OrderStatus.Completed).ToList();
            List<Order> allOrders = db.Orders.Where(o => o.Orderstatus == OrderStatus.Completed).ToList();
            MovieReportVM tivm = new MovieReportVM();

            Decimal tax = 0.0825m;
            foreach(Ticket td in SelectedTickets)
            {
                tivm.NumberOfPurchase += 1;
                tivm.Revenue += (td.TicketPrice + (td.TicketPrice * tax));
            }
            //return View(tivm);
            return View("DisplaySearchResults", tivm);
        }

        public SelectList GetAllTicketSeats(int SelectedShowing)
        {
            Showing showing = db.Showings.Find(SelectedShowing);
            List<Ticket> tickets = db.Tickets.Where(t => t.Order.Orderstatus == OrderStatus.Completed && t.Showing.ShowingID == showing.ShowingID).ToList();
            SelectList selSeats = SeatHelper.FindAvailableSeats(tickets);
            return selSeats;
        }

        //method to get all courses for the ViewBag
        public SelectList GetAllShowings()
        {
            List<Showing> allShowings = db.Showings.Where(s => s.IsPublished == PublishedStatus.IsPublished).OrderBy(s => s.SponsoringMovie.MovieTitle).ToList();
            SelectList selShowings = new SelectList(allShowings, "ShowingID", "ShowingNameAndDate");     
            return selShowings;
        }

        public MultiSelectList GetAllShowingMovies()
        {
            List<Movie> allShowingMovies = db.Movies.ToList();
            MultiSelectList ShowingsReport = new MultiSelectList(allShowingMovies, "MovieID", "MovieTitle");
            return ShowingsReport;
        }

        public SelectList GetAllCards()
        {
            List<Card> allCards = db.Cards.ToList();  //TODO: sepecific for each user
            SelectList selcards = new SelectList(allCards, "CardID", "CardNumber");
            return selcards;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
