using CinemaTicket.Models;
using CinemaTicket.Models.CinemaModels;
using CinemaTicket.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CinemaTicket.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db;
        public HomeController()
        {
            db = new ApplicationDbContext();
        }
        public ActionResult Index(int? page)
        {
            int pageSize = 9;
            int pageNumber = (page ?? 1);
            ViewBag.pageSize = pageSize;
            ViewBag.pageNumber = pageNumber;


            ViewBag.MovieDetails = db.MovieDetails.ToList();
            // lay 3 movie sap chieu
            ViewBag.Celebrities = db.Celebrities.ToList();// lấy hết celebrities của 3 phim trên
            //List<Movie> movies = db.Movies.ToList();

            //List<MovieDetail> movieDetails = db.MovieDetails.ToList();



            //HomeViewModel homeViewModel = new HomeViewModel()
            //{
            //    movies = movies,
            //    movieDetails = movieDetails
            //};

            ViewBag.News = db.News.ToList();

            var items = db.Movies.OrderBy(x => x.MovieName)
                      .Skip((pageNumber - 1) * pageSize)
                      .Take(pageSize)
                      .ToList();

            ViewBag.Movie = items;
            return View(db.Celebrities.ToList());
        }

        public ActionResult GetAllMovie()
        {

            List<Movie> movies = db.Movies.ToList();
            return PartialView(movies);
        }





    }
}