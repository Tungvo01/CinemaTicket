using CinemaTicket.Models;
using CinemaTicket.Models.CinemaModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Web;
using System.Web.Mvc;

namespace CinemaTicket.Controllers
{
    public class ReservationsController : Controller
    {
        private ApplicationDbContext db;
        public ReservationsController()
        {
            db = new ApplicationDbContext();
        }
        // GET: Reservations
        //public ActionResult Index(int MovieId)
        //{
        //    int MovieIds = int.Parse(Request.Params["MovieId"]);

        //    ViewBag.Reservations = db.Reservations.ToList();
        //    ViewBag.Shows = db.Shows.Where(p => p.MovieId == MovieId).ToList();

        //    ViewBag.ShowDays = new SelectList(db.Shows.Where(p => p.MovieId == MovieId).Include("ShowDays"), "ShowDayId", "ShowDayId");
        //    ViewBag.ShowId = new SelectList(db.Shows, "ShowId", "ShowId");

        //    //day movie
        //    var ShowDays = db.ShowDays
        //        .Join(db.Shows,
        //            sh => sh.ShowDayId,
        //            s => s.ShowDayId,
        //            (sh, s) => new { ShowDays = sh, Shows = s })
        //        .Where(x => x.Shows.ShowId == ShowIds)
        //        .Select(x => x.ShowDays.Day).ToList();
        //    ViewBag.ShowDays = ShowDays;

        //    //show time
        //    var ShowTimes = db.ShowTimes
        //       .Join(db.Shows,
        //           sh => sh.ShowTimeId,
        //           s => s.ShowTimeId,
        //           (sh, s) => new { ShowTimes = sh, Shows = s })
        //       .Where(x => x.Shows.ShowId == ShowIds)
        //       .Select(x => x.ShowTimes.Time).ToList();
        //    ViewBag.ShowTimes = ShowTimes;

        //    //cinema show
        //    var Cinemas = db.Cinemas
        //       .Join(db.Shows,
        //           sh => sh.CinemaId,
        //           s => s.CinemaId,
        //           (sh, s) => new { Cinemas = sh, Shows = s })
        //       .Where(x => x.Shows.ShowId == ShowIds)
        //       .Select(x => x.Cinemas.CinemaName).ToList();

        //    ViewBag.Cimemas = Cinemas;

        //    return View();
        //}
        public ActionResult ChooseShow(int? MovieId, int? ShowId)
        {
            //int MovieIds = int.Parse(Request.Params["MovieId"]);
            //int ShowIds = int.Parse(Request.Params["ShowId"]);

            ViewBag.Movie = db.Movies.Find(MovieId);
            ViewBag.ShowDays = new SelectList(db.Shows.Where(p => p.MovieId == MovieId).Include("ShowDays"), "ShowDayId", "ShowDayId");
            ViewBag.ShowId = new SelectList(db.Shows, "ShowId", "ShowId");
     

            ViewBag.ReservationId = db.Reservations.Where(c=>c.ShowId == ShowId).ToList();
            //day movie
            var ShowDays = db.ShowDays
                .Join(db.Shows,
                    sh => sh.ShowDayId,
                    s => s.ShowDayId,
                    (sh, s) => new { ShowDays = sh, Shows = s })
                .Where(x => x.Shows.ShowId == ShowId)
                .Select(x => x.ShowDays.Day).ToList();
            ViewBag.ShowDays = ShowDays;

         //show time
            var ShowTimes = db.ShowTimes
               .Join(db.Shows,
                   sh => sh.ShowTimeId,
                   s => s.ShowTimeId,
                   (sh, s) => new { ShowTimes = sh, Shows = s })
               .Where(x => x.Shows.ShowId == ShowId)
               .Select(x => x.ShowTimes.Time).ToList();
            ViewBag.ShowTimes = ShowTimes;

            //cinema show
            var Cinemas = db.Cinemas
               .Join(db.Shows,
                   sh => sh.CinemaId,
                   s => s.CinemaId,
                   (sh, s) => new { Cinemas = sh, Shows = s })
               .Where(x => x.Shows.ShowId == ShowId)
               .Select(x => x.Cinemas.CinemaName).ToList();

            ViewBag.Cimemas = Cinemas;


            var seatIds = from r in db.Reservations
                          where r.ShowId == ShowId
                          orderby r.SeatId
                          select r.SeatId;

            ViewBag.SeatIds = seatIds;



            //ViewBag.statusList = statusList;
            //status = true;
            //db.SaveChanges();



            return View(db.Reservations.Where(c => c.ShowId == ShowId).ToList());
        }


        public ActionResult a(int[] seats)
        {
            //int[] b = { 1, 2 };
            //for( int i = 0; i < b.Length; i++)
            //{
                var status = (from r in db.Reservations
                              join s in db.Seats on r.SeatId equals s.SeatId
                              where r.SeatId == 1
                              select s.Status).FirstOrDefault();

                //status. = true;
                //db.SaveChanges();
            //}

            //select s.Status from reservations r, Seats s where r.SeatId = s.SeatId and r.SeatId = 2 and r.ReservationId ==??


            //db.Movies.Add(movie);???? s.Status = true;
            //db.SaveChanges();

            return View();
        }



    }
}