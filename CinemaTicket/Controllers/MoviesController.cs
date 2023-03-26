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
using PagedList;

namespace CinemaTicket.Controllers
{
    public class MoviesController : Controller
    {
        private ApplicationDbContext db;
        public MoviesController()
        {
            db = new ApplicationDbContext();
        }
        // GET: Movies
        public ActionResult Index(int? page)
        {

            if(page == null) page = 1;

            var Movies = (from l in db.Movies select l).OrderBy(x => x.MovieId);

            int pageSize = 6;
            int pageNumber = (page ?? 1);

            return View(Movies.ToPagedList(pageNumber, pageSize));
        }

        // GET: Movies/Details/5
        public ActionResult Details(int? id)
        {
            //ViewBag.MovieDetails = db.MovieDetails.Find()
            ViewBag.Movies = db.Movies.Find(id);//lay dc ten film
            ViewBag.Shows = db.Shows.Where(p => p.MovieId == id).ToList();
            ViewBag.ShowDays = db.ShowDays.ToList();

            
            ViewBag.Rersevations = db.Reservations.ToList();
            ViewBag.ShowTimes = db.ShowTimes.ToList();
            ViewBag.Cinemas = db.Cinemas.ToList();// lấy hết celebrities của 3 phim trên



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


        public ActionResult get3Slide()
        {

            var lastThreeItems = db.MovieDetails.Include(c => c.Movie).Include(c => c.Celebrity)
                .Take(3)
                .ToList();

            return View(lastThreeItems);
        }


        public ActionResult Latest()
        {

          

            return View();
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
