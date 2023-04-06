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
    [Authorize]
    public class MoviesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/Movies
        public ActionResult Index(int? page, string SearchString = "")
        {

            if (page == null) page = 1;


            int pageSize = 6;
            int pageNumber = (page ?? 1);
            if (SearchString != "")
            {
                var Movies = db.Movies.Where(x => x.MovieName.ToUpper().Contains(SearchString.ToUpper())).OrderBy(x => x.MovieId);
                return View(Movies.ToPagedList(pageNumber, pageSize));
            }
            else
            {
                var Movies = (from l in db.Movies select l).OrderBy(x => x.MovieId);
                return View(Movies.ToPagedList(pageNumber, pageSize));

            }


            //ViewBag.Search = searchMovie;

        }

        // GET: Admin/Movies/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movie movie = db.Movies.Find(id);
            if (movie == null)
            {
                return HttpNotFound();
            }
            return View(movie);
        }

        // GET: Admin/Movies/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Movies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MovieId,MovieName,Description,ImageURL,StartDate,EndDate")] Movie movie)
        {
            //End Upload


            if (ModelState.IsValid)
            {

                HttpPostedFileBase file = Request.Files["upload"];
                if (file != null && file.ContentLength > 0)
                {


                    string fileName = Path.GetFileName(file.FileName);
                    string path = Path.Combine(Server.MapPath("~/Content/assets/img"), fileName);

                    file.SaveAs(path);
                    movie.ImageURL = fileName;
                }
                //End Upload

                db.Movies.Add(movie);
                db.SaveChanges();
                return RedirectToAction("Index");
                //HttpPostedFileBase file = Request.Files["upload"];
                //if (file != null && file.ContentLength > 0)
                //{

                //    string fileName = Path.GetFileName(file.FileName);
                //    string path = Path.Combine(Server.MapPath("~/Images"), fileName);

                //    file.SaveAs(path);
                //    movie.ImageURL = fileName;
                //}
                //db.Movies.Add(movie);
                //db.SaveChanges();
                //return RedirectToAction("Index");
            }

            return View(movie);
        }



        // GET: Admin/Movies/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movie movie = db.Movies.Find(id);
            if (movie == null)
            {
                return HttpNotFound();
            }
            return View(movie);
        }

        // POST: Admin/Movies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MovieId,MovieName,Description,ImageURL,StartDate,EndDate")] Movie movie)
        {
            if (ModelState.IsValid)
            {
                HttpPostedFileBase file = Request.Files["upload"];
                if (file != null && file.ContentLength > 0)
                {

                    string fileName = Path.GetFileName(file.FileName);
                    string path = Path.Combine(Server.MapPath("~/Content/assets/img"), fileName);

                    file.SaveAs(path);
                    movie.ImageURL = fileName;
                }
                db.Entry(movie).State = EntityState.Modified;
                db.SaveChanges();
                if (db.SaveChanges() > 0)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Không lưu đươc!");
                    return RedirectToAction("Index");
                }
            }
            return View(movie);
        }

        // GET: Admin/Movies/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movie movie = db.Movies.Find(id);
            if (movie == null)
            {
                return HttpNotFound();
            }
            return View(movie);
        }

        // POST: Admin/Movies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Movie movie = db.Movies.Find(id);
            db.Movies.Remove(movie);
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