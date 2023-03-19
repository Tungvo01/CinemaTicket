using CinemaTicket.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.Entity;
using System.Net;

using CinemaTicket.Models.CinemaModels;

namespace CinemaTicket.Controllers
{
    public class MovieDetailsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: MovieDetails
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult de()
        {

            var lastThreeItems = db.MovieDetails.Include(c => c.Celebrity).Include(c => c.Movie)
                .Take(3)
                .ToList();

            return View(lastThreeItems);
        }
    }
}