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
using static MIS333K_Team11_FinalProjectV2.Models.AppUser;

namespace MIS333K_Team11_FinalProjectV2.Controllers
{
    public class TicketsController : Controller
    {
        private AppDbContext db = new AppDbContext();

        //// GET: Tickets
        //public ActionResult Index()
        //{
        //    return View(db.Tickets.ToList());
        //}

        //// GET: Tickets/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Ticket ticket = db.Tickets.Find(id);
        //    if (ticket == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(ticket);
        //}

        //// GET: Tickets/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: Tickets/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "TicketID,TicketPrice,TicketSeat,TotalFees")] Ticket ticket)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Tickets.Add(ticket);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    return View(ticket);
        //}

        // GET: Tickets/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ticket ticket = db.Tickets.Find(id);
            if (ticket == null)
            {
                return HttpNotFound();
            }
            return View(ticket);
        }

        // POST: Tickets/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TicketID,TicketPrice,TicketSeat,TotalFees")] Ticket ticket)
        {
            //find the product associated with this order
            Ticket td = db.Tickets.Include(TD => TD.Order)
                                  .Include(TD => TD.Showing)
                                  .FirstOrDefault(x => x.TicketID == ticket.TicketID);

            if (ModelState.IsValid)
            {
                //update the number of students
                td.TicketSeat = ticket.TicketSeat;

                //update the course fee from the related course
                td.TicketPrice = td.Showing.TicketPrice;

                //update the total fees
                td.TotalFees = td.TicketPrice /** td.TicketSeat*/;

                db.Entry(ticket).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details", "Orders", new { id = td.Order.OrderID });
            }
            ticket.Order = td.Order;
            return View(ticket);
        }

        // GET: Tickets/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ticket ticket = db.Tickets.Find(id);
            if (ticket == null)
            {
                return HttpNotFound();
            }
            return View(ticket);
        }

        // POST: Tickets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Ticket ticket = db.Tickets.Find(id);
            Order ord = ticket.Order;
            db.Tickets.Remove(ticket);
            db.SaveChanges();
            return RedirectToAction("Details", "Orders", new { id = ord.OrderID });
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
