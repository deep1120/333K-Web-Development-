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
            return View(db.Orders.ToList()); 
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
            return View(order);
        }

        // GET: Orders/Create
        public ActionResult Create()
        {
            //ViewBag.AllShowings = GetAllShowings();
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "OrderID,OrderNumber,OrderDate,OrderNotes,Orderstatus")] Order order)
        {

            //Find next order number
            order.OrderNumber = Utilities.GenerateNextOrderNumber.GetNextOrderNumber();

            //record date of order
            order.OrderDate = DateTime.Today;
            
            if (ModelState.IsValid)
            {
                db.Orders.Add(order);
                db.SaveChanges();
                return RedirectToAction("AddToOrder", new { OrderID = order.OrderID });
            }

            return View(order);

        }

        public ActionResult AddToOrder(int OrderID)
        {
            //Create a new instance of the order detail class
            Ticket td = new Ticket();

            //Find the order for this order detail
            Order ord = db.Orders.Find(OrderID);

            //Set the new order detail's order to the new ord we just found
            td.Order = ord;

            ViewBag.AllSeats = GetAllTicketSeats();
            //Populate the view bag with the list of courses
            ViewBag.AllShowings = GetAllShowings();

            //Give the view the registration detail object we just created
            return View(td);
            //return RedirectToAction("Checkout", new { OrderID = td.Order.OrderID });
        }

        [HttpPost]
        public ActionResult AddToOrder(Ticket td, int SelectedShowing, int[] SelectedTickets)
        {
            //Find the course associated with the int SelectedShowing
            Showing showing = db.Showings.Find(SelectedShowing);

            //find the order 
            Order ord = db.Orders.Find(td.Order.OrderID);
            ord.Orderstatus = OrderStatus.Pending;

            //LOGIC HERE TO STOP USER FROM ADDING A SHOWING THAT OVERLAPS WITH A ANOTHER SHOWING IN CART
            //List<Showing> CurrentShowingsInCart = db.Showings.Where(o=>o.Tickets.....

            for (int i = 0; i < SelectedTickets.Length; i++)
            {
                Ticket ticket = new Ticket();
                ticket.TicketSeat = SeatHelper.GetSeatName(i);
                ticket.Order = ord;
                ticket.Showing = showing;
                ticket.Order.Orderstatus = OrderStatus.Pending;

                DateTime weekday = Convert.ToDateTime("12:00");
                DateTime tuesday = Convert.ToDateTime("5:00");

                if ((showing.ShowDate.DayOfWeek == DayOfWeek.Monday) && (showing.ShowDate < weekday))
                {
                    ticket.TicketPrice = 5.00m;
                }
                if ((showing.ShowDate.DayOfWeek == DayOfWeek.Tuesday) && (showing.ShowDate < weekday))
                {
                    ticket.TicketPrice = 5.00m;
                }
                if ((showing.ShowDate.DayOfWeek == DayOfWeek.Wednesday) && (showing.ShowDate < weekday))
                {
                    ticket.TicketPrice = 5.00m;
                }
                if ((showing.ShowDate.DayOfWeek == DayOfWeek.Thursday) && (showing.ShowDate < weekday))
                {
                    ticket.TicketPrice = 5.00m;
                }
                if ((showing.ShowDate.DayOfWeek == DayOfWeek.Friday) && (showing.ShowDate < weekday))
                {
                    ticket.TicketPrice = 5.00m;
                }
                if ((showing.ShowDate.DayOfWeek == DayOfWeek.Tuesday) && (showing.ShowDate <= tuesday))
                {
                    ticket.TicketPrice = 8.00m;
                }
                if ((showing.ShowDate.DayOfWeek == DayOfWeek.Monday) && (showing.ShowDate >= weekday))
                {
                    ticket.TicketPrice = 10.00m;
                }
                if ((showing.ShowDate.DayOfWeek == DayOfWeek.Tuesday) && (showing.ShowDate >= weekday))
                {
                    ticket.TicketPrice = 10.00m;
                }
                if ((showing.ShowDate.DayOfWeek == DayOfWeek.Wednesday) && (showing.ShowDate >= weekday))
                {
                    ticket.TicketPrice = 10.00m;
                }
                if ((showing.ShowDate.DayOfWeek == DayOfWeek.Thursday) && (showing.ShowDate >= weekday))
                {
                    ticket.TicketPrice = 10.00m;
                }
                if ((showing.ShowDate.DayOfWeek == DayOfWeek.Friday) && (showing.ShowDate >= weekday))
                {
                    ticket.TicketPrice = 12.00m;
                }
                if (showing.ShowDate.DayOfWeek == DayOfWeek.Saturday)
                {
                    ticket.TicketPrice = 12.00m;
                }
                if (showing.ShowDate.DayOfWeek == DayOfWeek.Sunday)
                {
                    ticket.TicketPrice = 12.00m;
                }

                db.Tickets.Add(ticket);
                db.SaveChanges();
            }            
            
            if (ModelState.IsValid)
            {
                //redirect to edit so that they can continue adding to cart if they want to 
                return RedirectToAction("Edit", "Orders", new { id = ord.OrderID });
            }

            //model state is not valid; repopulate viewbags and return
            ViewBag.AllSeats = GetAllTicketSeats();
            ViewBag.AllShowings = GetAllShowings();
            return View(td);
        }

        public ActionResult Checkout()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Checkout(int OrderID/*, string CardOption*/)
        {
            Order ord = db.Orders.Find(OrderID);

            ord.Orderstatus = OrderStatus.Completed;

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

            return View(ord);

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
        public ActionResult Edit([Bind(Include = "OrderID,OrderNumber,OrderDate,OrderNotes,Orderstatus")] Order order)
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

        public MultiSelectList GetAllTicketSeats()
        {
            List<Ticket> tickets = db.Tickets.Where(t=>t.Order.Orderstatus == OrderStatus.Completed).ToList();

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
