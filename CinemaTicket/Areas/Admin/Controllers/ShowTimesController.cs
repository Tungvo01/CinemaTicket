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
    [Authorize]
    public class ShowTimesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/ShowTimes
        public ActionResult Index()
        {
            return View(db.ShowTimes.ToList());
        }

        // GET: Admin/ShowTimes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ShowTime showTime = db.ShowTimes.Find(id);
            if (showTime == null)
            {
                return HttpNotFound();
            }
            return View(showTime);
        }

        // GET: Admin/ShowTimes/Create
        public ActionResult Create()
        {

            return View();
        }

        // POST: Admin/ShowTimes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ShowTimeId,Time")] ShowTime showTime)
        {
            if (ModelState.IsValid)
            {
                db.ShowTimes.Add(showTime);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(showTime);
        }

        // GET: Admin/ShowTimes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ShowTime showTime = db.ShowTimes.Find(id);
            if (showTime == null)
            {
                return HttpNotFound();
            }
            return View(showTime);
        }

        // POST: Admin/ShowTimes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ShowTimeId,Time")] ShowTime showTime)
        {
            if (ModelState.IsValid)
            {
                db.Entry(showTime).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(showTime);
        }

        // GET: Admin/ShowTimes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ShowTime showTime = db.ShowTimes.Find(id);
            if (showTime == null)
            {
                return HttpNotFound();
            }
            return View(showTime);
        }

        // POST: Admin/ShowTimes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ShowTime showTime = db.ShowTimes.Find(id);
            db.ShowTimes.Remove(showTime);
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