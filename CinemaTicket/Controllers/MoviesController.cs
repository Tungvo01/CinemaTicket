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
    public class MoviesController : Controller
    {
        private ApplicationDbContext db;
        public MoviesController()
        {
            db = new ApplicationDbContext();
        }
        // GET: Movies
        public ActionResult Index()
        {
            return View(db.Movies.ToList());
        }

        // GET: Movies/Details/5
        public ActionResult Details(int? id)
        {
            //ViewBag.MovieDetails = db.MovieDetails.Find()
            ViewBag.Movies = db.Movies.Find(id);//lay dc ten film
            ViewBag.Shows = db.Shows.ToList();
              ViewBag.ShowDays = db.ShowDays.ToList();
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
