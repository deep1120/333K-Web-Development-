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
        public ActionResult AddToOrder([Bind(Include = "TicketID,OrderID,MovieID,Quantity,Subtotal,TicketPrice,TicketSeat,TotalFees")] Ticket td, int SelectedTicket)
        {
            //Showing showing = db.Showings.Find(SelectedShowing);

            //find the order 
            Order ord = db.Orders.Find(td.OrderID);
            ord.Orderstatus = OrderStatus.Pending;

            //LOGIC HERE TO STOP USER FROM ADDING A SHOWING THAT OVERLAPS WITH A ANOTHER SHOWING IN CART
            //List<Showing> CurrentShowingsInCart = db.Showings.Where(o=>o.Tickets.....

            td.TicketSeat = SeatHelper.GetSeatName(SelectedTicket);
            td.Order = ord;
            td.Order.Orderstatus = OrderStatus.Pending;

            if (ModelState.IsValid)
            {
                db.Entry(td).State = EntityState.Modified;
                db.SaveChanges();
                //redirect to edit so that they can continue adding to cart if they want to 
                return RedirectToAction("Edit", "Orders", new { id = ord.OrderID });
            }

            //model state is not valid; repopulate viewbags and return
            ViewBag.AllSeats = GetAllTicketSeats(td.Showing.ShowingID);
            return View(td);

            //for (int i = 0; i < SelectedTickets.Length; i++)
            //{               
            //    Ticket ticket = new Ticket();
            //    ticket.TicketSeat = SeatHelper.GetSeatName(i);
            //    ticket.Order = ord;
            //    ticket.Showing = td.Showing;
            //    ticket.Order.Orderstatus = OrderStatus.Pending;

            //    db.Tickets.Add(ticket);
            //    db.SaveChanges();

            //    if (ModelState.IsValid)
            //    {
            //        //redirect to edit so that they can continue adding to cart if they want to 
            //        return RedirectToAction("Edit", "Orders", new { id = ord.OrderID });
            //    }

            //    //model state is not valid; repopulate viewbags and return
            //    ViewBag.AllSeats = GetAllTicketSeats(td.Showing.ShowingID);
            //    //ViewBag.AllShowings = GetAllShowings();
            //    return View(td);

            //    //PUT THIS TICKETPRICE IN SHOWINGS CONTROLLER WHEN CREATING A SHOWING
            //    //PUT validation in here for 48 hour discount
            //    //other discounts with age, etc.

            //    DateTime weekday = Convert.ToDateTime("12:00");
            //    DateTime tuesday = Convert.ToDateTime("5:00");

            //    if ((td.Showing.ShowDate.DayOfWeek == DayOfWeek.Monday) && (td.Showing.ShowDate < weekday))
            //    {
            //        ticket.TicketPrice = 5.00m;
            //    }
            //    if ((td.Showing.ShowDate.DayOfWeek == DayOfWeek.Tuesday) && (td.Showing.ShowDate < weekday))
            //    {
            //        ticket.TicketPrice = 5.00m;
            //    }
            //    if ((td.Showing.ShowDate.DayOfWeek == DayOfWeek.Wednesday) && (td.Showing.ShowDate < weekday))
            //    {
            //        ticket.TicketPrice = 5.00m;
            //    }
            //    if ((td.Showing.ShowDate.DayOfWeek == DayOfWeek.Thursday) && (td.Showing.ShowDate < weekday))
            //    {
            //        ticket.TicketPrice = 5.00m;
            //    }
            //    if ((td.Showing.ShowDate.DayOfWeek == DayOfWeek.Friday) && (td.Showing.ShowDate < weekday))
            //    {
            //        ticket.TicketPrice = 5.00m;
            //    }
            //    if ((td.Showing.ShowDate.DayOfWeek == DayOfWeek.Tuesday) && (td.Showing.ShowDate <= tuesday))
            //    {
            //        ticket.TicketPrice = 8.00m;
            //    }
            //    if ((td.Showing.ShowDate.DayOfWeek == DayOfWeek.Monday) && (td.Showing.ShowDate >= weekday))
            //    {
            //        ticket.TicketPrice = 10.00m;
            //    }
            //    if ((td.Showing.ShowDate.DayOfWeek == DayOfWeek.Tuesday) && (td.Showing.ShowDate >= weekday))
            //    {
            //        ticket.TicketPrice = 10.00m;
            //    }
            //    if ((td.Showing.ShowDate.DayOfWeek == DayOfWeek.Wednesday) && (td.Showing.ShowDate >= weekday))
            //    {
            //        ticket.TicketPrice = 10.00m;
            //    }
            //    if ((td.Showing.ShowDate.DayOfWeek == DayOfWeek.Thursday) && (td.Showing.ShowDate >= weekday))
            //    {
            //        ticket.TicketPrice = 10.00m;
            //    }
            //    if ((td.Showing.ShowDate.DayOfWeek == DayOfWeek.Friday) && (td.Showing.ShowDate >= weekday))
            //    {
            //        ticket.TicketPrice = 12.00m;
            //    }
            //    if (td.Showing.ShowDate.DayOfWeek == DayOfWeek.Saturday)
            //    {
            //        ticket.TicketPrice = 12.00m;
            //    }
            //    if (td.Showing.ShowDate.DayOfWeek == DayOfWeek.Sunday)
            //    {
            //        ticket.TicketPrice = 12.00m;
            //    }

            //    db.Tickets.Add(ticket);
            //    db.SaveChanges();
            //}            

            //if (ModelState.IsValid)
            //{
            //    //redirect to edit so that they can continue adding to cart if they want to 
            //    return RedirectToAction("Edit", "Orders", new { id = ord.OrderID });
            //}

            ////model state is not valid; repopulate viewbags and return
            //ViewBag.AllSeats = GetAllTicketSeats(td.Showing.ShowingID);
            ////ViewBag.AllShowings = GetAllShowings();
            //return View(td);
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
            //when in edit view page, user clicks checkout and then and will pass the whole order object over to Checkout method
            Order ord = db.Orders.Find(OrderID);
            return View(ord);
        }

        [HttpPost]
        public ActionResult Checkout(Order ord/*, string CardOption*/) //without bind...might not include stuff
        {
            //have to add modify and and do entity stuff
            //fuzzy object like past codes in edit
            //this is because it is going through httppost
            //find the product associated with this order
            Order od = db.Orders.Include(OD => OD.Tickets)
                                 //.Include(OD => OD.Showings?)
                                 .FirstOrDefault(x => x.OrderID == ord.OrderID);
            //Order ord = db.Orders.Find(OrderID);

            if (ModelState.IsValid)
            {
                od.Tickets = ord.Tickets;
                od.Orderstatus = OrderStatus.Completed;
                db.Entry(ord).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Checkout", "Orders", new { id = od.OrderID }); //checkout gift, checkoutfinal view. 
            }

            //ticket.Order = td.Order;
            return View(ord);

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

        public MultiSelectList GetAllTicketSeats(int SelectedShowing)
        {
            Showing showing = db.Showings.Find(SelectedShowing);

            List<Ticket> tickets = db.Tickets/*.Where(t => t.Order.Orderstatus == OrderStatus.Completed && t.Showing == showing)*/.ToList();

            MultiSelectList selSeats = SeatHelper.FindAvailableSeats(tickets);

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
