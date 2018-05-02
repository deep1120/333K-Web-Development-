using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
//using MIS333K_Team11_FinalProjectV2.DAL;
using MIS333K_Team11_FinalProjectV2.Models;
using MIS333K_Team11_FinalProjectV2.Utilities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using static MIS333K_Team11_FinalProjectV2.Models.AppUser;

namespace MIS333K_Team11_FinalProjectV2.Controllers
{
    public class OrdersController : Controller
    {
        private AppDbContext db = new AppDbContext();

        // GET: Orders
        public ActionResult Index()
        {
            //Maybe make this the order history page???
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
        public ActionResult Create()
        {
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
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
        public ActionResult AddShowing(int OrderID)
        {
            //Ticket td = new Ticket();
            Order ord = db.Orders.Find(OrderID);
            //td.Order = ord;
            ViewBag.AllShowings = GetAllShowings();
            return View(ord); //td?
        }

        [HttpPost]
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
            //ViewBag.AllSeats = GetAllTicketSeats(/*SelectedShowing*/);
            ViewBag.AllShowings = GetAllShowings();
            return View(order);
        }

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

            //    //PUT THIS TICKETPRICE IN SHOWINGS CONTROLLER WHEN CREATING A SHOWING
            //    //PUT validation in here for 48 hour discount
            //    //other discounts with age, etc.

            //    DateTime weekday = Convert.ToDateTime("12:00");
            //    DateTime tuesday = Convert.ToDateTime("5:00");

        }

        public ActionResult Gift(Order order)
        {
            order.Gift = false;
            return View(order);
        }

        [HttpPost]
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

        public ActionResult Checkout(int OrderID)
        {
            ViewBag.AllCards = GetAllCards();
            //when in edit view page, user clicks checkout and then and will pass the whole order object over to Checkout method
            Order ord = db.Orders.Find(OrderID);
            return View(ord);
        }

        [HttpPost]
        public ActionResult Checkout([Bind(Include = "OrderID,OrderNumber,ConfirmationNumber,EarlyDiscount,SeniorDiscount,OrderDate,CardNumber,Orderstatus,OrderSubtotal,SalesTax,OrderTotal,Gift,GiftEmail")] Order ord/*, int SelectedCards*/) //without bind...might not include stuff
        {

            //Order od = db.Orders.Include(OD => OD.Tickets)
            //                    .FirstOrDefault(x => x.OrderID == ord.OrderID);
            Order od = db.Orders.Find(ord.OrderID);
            //do we add confirmation number here as well??

            if (ModelState.IsValid)
            {
                //od.Tickets = ord.Tickets;
                od.Orderstatus = OrderStatus.Completed;
                db.Entry(od).State = EntityState.Modified;
                db.SaveChanges();
                //return RedirectToAction("Checkout", "Orders", new { id = od.OrderID }); //checkout gift, checkoutfinal view. 
                return RedirectToAction("Index");
            }

            //ticket.Order = td.Order;
            return View(od);

            //ord.ListOfCards = new List<SelectListItem>();
            //var userID = User.Identity.GetUserID();
            //var user = db.Users.Include(x => x.Cards).Single(x => x.Id == userId);

            //if(user != null)
            //{
            //    foreach(var card in user.Cards)
            //    {
            //        ord.ListOfCards.Add(new SelectListItem { Text = $"{card.CardNumber} {card.Type.ToString()}", Value = $"{card.CardNumber} {card.Type.ToString()}" });
            //    }
            //}
        }

        public ActionResult Confirm()
        {
            //TODO: Add generate confirmation number
            return View();
        }


        // GET: Orders/Edit/5
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
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
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
        public ActionResult DeleteConfirmed(int id)
        {
            Order order = db.Orders.Find(id);
            db.Orders.Remove(order);
            db.SaveChanges();
            return RedirectToAction("Index");
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
            //Get the list of showings in order by showing name 
            List<Showing> allShowings = db.Showings.OrderBy(s => s.SponsoringMovie.MovieTitle).ToList();

            //convert the list to a select list
            SelectList selShowings = new SelectList(allShowings, "ShowingID", "ShowingNameAndDate");

            //return the select list        
            return selShowings;
        }

        public SelectList GetAllCards()
        {
            List<Card> allCards = db.Cards.ToList();  //TODO: sepecific for each user

            SelectList selcards = new SelectList(allCards, "CardID", "CardNumber");

            return selcards;
        }

        private AppUserManager UserManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<AppUserManager>();
            }
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
