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
        public void ExportToExcel()
        {
            List<Celebrity> listCelebrities = db.Celebrities.ToList();

            ExcelPackage pck = new ExcelPackage();
            ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Report");

            ws.Cells["A1"].Value = "CELEBRITY TABLE WORKSHEET";

            ws.Cells["A2"].Value = "Date";
            ws.Cells["B2"].Value = string.Format("{0:dd MMMM yyyy} at {0:H:mm tt}", DateTimeOffset.Now);

            ws.Cells["A3"].Value = "Author";
            ws.Cells["B3"].Value = User.Identity.Name;


            ws.Cells["A6"].Value = "CelebrityId";
            ws.Cells["B6"].Value = "CelebrityName";
            ws.Cells["C6"].Value = "Height";
            ws.Cells["D6"].Value = "Weight";
            ws.Cells["E6"].Value = "Avatar";
            ws.Cells["F6"].Value = "Description";
            ws.Cells["G6"].Value = "Language";

            int rowStart = 7;
            foreach (var item in listCelebrities)
            {
                ws.Cells[string.Format("A{0}", rowStart)].Value = item.CelebrityId;
                ws.Cells[string.Format("B{0}", rowStart)].Value = item.Name;
                ws.Cells[string.Format("C{0}", rowStart)].Value = item.Height;
                ws.Cells[string.Format("D{0}", rowStart)].Value = item.Weight;
                ws.Cells[string.Format("E{0}", rowStart)].Value = item.UrlAvatar;
                ws.Cells[string.Format("F{0}", rowStart)].Value = item.Description;
                ws.Cells[string.Format("G{0}", rowStart)].Value = item.Language;
                rowStart++;
            }

            //ws.Cells["A:AZ"].AutoFitColumns();
            Response.Clear();
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment: filename=" + "ExcelReport.xlsx");
            Response.BinaryWrite(pck.GetAsByteArray());
            Response.End();
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