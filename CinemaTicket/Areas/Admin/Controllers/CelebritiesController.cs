using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CinemaTicket.Models;
using CinemaTicket.Models.CinemaModels;
using PagedList;

namespace CinemaTicket.Areas.Admin.Controllers
{
    public class CelebritiesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/Celebrities
        public ActionResult Index(int? page, string SearchString = "")
        {

            if (page == null) page = 1;


            int pageSize = 6;
            int pageNumber = (page ?? 1);
            if (SearchString != "")
            {
                var Movies = db.Celebrities.Where(x => x.Name.ToUpper().Contains(SearchString.ToUpper())).OrderBy(x => x.CelebrityId);
                return View(Movies.ToPagedList(pageNumber, pageSize));
            }
            else
            {
                var Movies = (from l in db.Celebrities select l).OrderBy(x => x.CelebrityId);
                return View(Movies.ToPagedList(pageNumber, pageSize));

            }

            //return View(db.Celebrities.ToList());
        }

        // GET: Admin/Celebrities/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Celebrity celebrity = db.Celebrities.Find(id);
            if (celebrity == null)
            {
                return HttpNotFound();
            }
            return View(celebrity);
        }

        // GET: Admin/Celebrities/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Celebrities/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CelebrityId,Name,Height,Weight,UrlAvatar,Description,Language")] Celebrity celebrity)
        {
            if (ModelState.IsValid)
            {
                HttpPostedFileBase file = Request.Files["upload"];
                if (file != null && file.ContentLength > 0)
                {

                    string fileName = Path.GetFileName(file.FileName);
                    string path = Path.Combine(Server.MapPath("~/Content/assets/img"), fileName);

                    file.SaveAs(path);
                    celebrity.UrlAvatar = fileName;
                }
                db.Celebrities.Add(celebrity);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(celebrity);
        }

        // GET: Admin/Celebrities/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Celebrity celebrity = db.Celebrities.Find(id);
            if (celebrity == null)
            {
                return HttpNotFound();
            }
            return View(celebrity);
        }

        // POST: Admin/Celebrities/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CelebrityId,Name,Height,Weight,UrlAvatar,Description,Language")] Celebrity celebrity)
        {
            if (ModelState.IsValid)
            {
                HttpPostedFileBase file = Request.Files["upload"];
                if (file != null && file.ContentLength > 0)
                {

                    string fileName = Path.GetFileName(file.FileName);
                    string path = Path.Combine(Server.MapPath("~/Content/assets/img"), fileName);

                    file.SaveAs(path);
                    celebrity.UrlAvatar = fileName;
                }
                db.Entry(celebrity).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(celebrity);
        }

        // GET: Admin/Celebrities/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Celebrity celebrity = db.Celebrities.Find(id);
            if (celebrity == null)
            {
                return HttpNotFound();
            }
            return View(celebrity);
        }

        // POST: Admin/Celebrities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Celebrity celebrity = db.Celebrities.Find(id);
            db.Celebrities.Remove(celebrity);
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