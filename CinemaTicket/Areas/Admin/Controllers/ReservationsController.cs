using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CinemaTicket.Models;
using CinemaTicket.Models.CinemaModels;

namespace CinemaTicket.Areas.Admin.Controllers
{
    public class ReservationsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/Reservations
        public ActionResult Index()
        {
            var reservations = db.Reservations.Include(r => r.Customer).Include(r => r.Seat).Include(r => r.Show).Include(r => r.Show.Cinema).Include(r => r.Show.ShowDay).Include(r => r.Show.ShowTime).Include(r => r.Show.Movie);
            return View(reservations.ToList());
        }

        // GET: Admin/Reservations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reservation reservation = db.Reservations.Find(id);
            if (reservation == null)
            {
                return HttpNotFound();
            }
            return View(reservation);
        }

        // GET: Admin/Reservations/Create
        public ActionResult Create()
        {
            ViewBag.CustomerId = new SelectList(db.Users, "Id", "Name");
            ViewBag.SeatId = new SelectList(db.Seats, "SeatId", "SeatId");
            ViewBag.ShowId = new SelectList(db.Shows, "ShowId", "ShowId");
            return View();
        }

        // POST: Admin/Reservations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ReservationId,CustomerId,SeatId,ShowId")] Reservation reservation)
        {
            if (ModelState.IsValid)
            {
                db.Reservations.Add(reservation);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CustomerId = new SelectList(db.Users, "Id", "Name", reservation.CustomerId);
            ViewBag.SeatId = new SelectList(db.Seats, "SeatId", "SeatId", reservation.SeatId);
            ViewBag.ShowId = new SelectList(db.Shows, "ShowId", "ShowId", reservation.ShowId);
            return View(reservation);
        }

        // GET: Admin/Reservations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reservation reservation = db.Reservations.Find(id);
            if (reservation == null)
            {
                return HttpNotFound();
            }
            ViewBag.CustomerId = new SelectList(db.Users, "Id", "Name", reservation.CustomerId);
            ViewBag.SeatId = new SelectList(db.Seats, "SeatId", "SeatId", reservation.SeatId);
            ViewBag.ShowId = new SelectList(db.Shows, "ShowId", "ShowId", reservation.ShowId);
            return View(reservation);
        }

        // POST: Admin/Reservations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ReservationId,CustomerId,SeatId,ShowId")] Reservation reservation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(reservation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CustomerId = new SelectList(db.Users, "Id", "Name", reservation.CustomerId);
            ViewBag.SeatId = new SelectList(db.Seats, "SeatId", "SeatId", reservation.SeatId);
            ViewBag.ShowId = new SelectList(db.Shows, "ShowId", "ShowId", reservation.ShowId);
            return View(reservation);
        }

        // GET: Admin/Reservations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reservation reservation = db.Reservations.Find(id);
            if (reservation == null)
            {
                return HttpNotFound();
            }
            return View(reservation);
        }

        // POST: Admin/Reservations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Reservation reservation = db.Reservations.Find(id);
            db.Reservations.Remove(reservation);
            db.SaveChanges();
            return RedirectToAction("Index");
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
