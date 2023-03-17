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

namespace CinemaTicket.Controllers
{
    public class ShowsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Shows
        public ActionResult Index()
        {
            var shows = db.Shows.Include(s => s.Cinema).Include(s => s.Movie).Include(s => s.ShowDay).Include(s => s.ShowTime);
            return View(shows.ToList());
        }

        // GET: Shows/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Show show = db.Shows.Find(id);
            if (show == null)
            {
                return HttpNotFound();
            }
            return View(show);
        }

        //// GET: Shows/Create
        //public ActionResult Create()
        //{
        //    ViewBag.CinemaId = new SelectList(db.Cinemas, "CinemaId", "CinemaName");
        //    ViewBag.MovieId = new SelectList(db.Movies, "MovieId", "MovieName");
        //    ViewBag.ShowDayId = new SelectList(db.ShowDays, "ShowDayId", "ShowDayId");
        //    ViewBag.ShowTimeId = new SelectList(db.ShowTimes, "ShowTimeId", "Time");
        //    return View();
        //}

        //// POST: Shows/Create
        //// To protect from overposting attacks, enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "ShowId,CinemaId,MovieId,ShowDayId,ShowTimeId")] Show show)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Shows.Add(show);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    ViewBag.CinemaId = new SelectList(db.Cinemas, "CinemaId", "CinemaName", show.CinemaId);
        //    ViewBag.MovieId = new SelectList(db.Movies, "MovieId", "MovieName", show.MovieId);
        //    ViewBag.ShowDayId = new SelectList(db.ShowDays, "ShowDayId", "ShowDayId", show.ShowDayId);
        //    ViewBag.ShowTimeId = new SelectList(db.ShowTimes, "ShowTimeId", "Time", show.ShowTimeId);
        //    return View(show);
        //}

        //// GET: Shows/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Show show = db.Shows.Find(id);
        //    if (show == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    ViewBag.CinemaId = new SelectList(db.Cinemas, "CinemaId", "CinemaName", show.CinemaId);
        //    ViewBag.MovieId = new SelectList(db.Movies, "MovieId", "MovieName", show.MovieId);
        //    ViewBag.ShowDayId = new SelectList(db.ShowDays, "ShowDayId", "ShowDayId", show.ShowDayId);
        //    ViewBag.ShowTimeId = new SelectList(db.ShowTimes, "ShowTimeId", "Time", show.ShowTimeId);
        //    return View(show);
        //}

        //// POST: Shows/Edit/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "ShowId,CinemaId,MovieId,ShowDayId,ShowTimeId")] Show show)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(show).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    ViewBag.CinemaId = new SelectList(db.Cinemas, "CinemaId", "CinemaName", show.CinemaId);
        //    ViewBag.MovieId = new SelectList(db.Movies, "MovieId", "MovieName", show.MovieId);
        //    ViewBag.ShowDayId = new SelectList(db.ShowDays, "ShowDayId", "ShowDayId", show.ShowDayId);
        //    ViewBag.ShowTimeId = new SelectList(db.ShowTimes, "ShowTimeId", "Time", show.ShowTimeId);
        //    return View(show);
        //}

        //// GET: Shows/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Show show = db.Shows.Find(id);
        //    if (show == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(show);
        //}

        //// POST: Shows/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    Show show = db.Shows.Find(id);
        //    db.Shows.Remove(show);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

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
