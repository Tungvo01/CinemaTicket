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
using OfficeOpenXml;
using PagedList;

namespace CinemaTicket.Areas.Admin.Controllers
{
    [Authorize(Users = "admin@gmail.com")]

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
        public void ExportToExcel()
        {

            List<Movie> listMovies = db.Movies.ToList();

            ExcelPackage pck = new ExcelPackage();
            ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Report");

            ws.Cells["A1"].Value = "MOVIE TABLE WORKSHEET";

            ws.Cells["A2"].Value = "Date";
            ws.Cells["B2"].Value = string.Format("{0:dd MMMM yyyy} at {0:H:mm tt}", DateTimeOffset.Now);

            ws.Cells["A3"].Value = "Author";
            ws.Cells["B3"].Value = User.Identity.Name;


            ws.Cells["A6"].Value = "MovieId";
            ws.Cells["B6"].Value = "MovieName";
            ws.Cells["C6"].Value = "Description";
            ws.Cells["D6"].Value = "ImageURL";
            ws.Cells["E6"].Value = "StartDate";
            ws.Cells["F6"].Value = "EndDate";

            int rowStart = 7;
            foreach (var item in listMovies)
            {
                ws.Cells[string.Format("A{0}", rowStart)].Value = item.MovieId;
                ws.Cells[string.Format("B{0}", rowStart)].Value = item.MovieName;
                ws.Cells[string.Format("C{0}", rowStart)].Value = item.Description;
                ws.Cells[string.Format("D{0}", rowStart)].Value = item.ImageURL;
                ws.Cells[string.Format("E{0}", rowStart)].Value = item.StartDate.ToString("dd/MM/yyyy");
                ws.Cells[string.Format("F{0}", rowStart)].Value = item.EndDate.ToString("dd/MM/yyyy");
                rowStart++;
            }

            //ws.Cells["A:AZ"].AutoFitColumns();
            Response.Clear();
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment: filename=" + "ExcelReport.xlsx");
            Response.BinaryWrite(pck.GetAsByteArray());
            Response.End();
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