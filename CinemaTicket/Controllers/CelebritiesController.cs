using CinemaTicket.Models;
using CinemaTicket.Models.CinemaModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace CinemaTicket.Controllers
{
    public class CelebritiesController : Controller
    {
        private ApplicationDbContext db;
        public CelebritiesController()
        {
            db = new ApplicationDbContext();
        }
        // GET: Celebrities
        public ActionResult Index()
        {
            
            return View(db.Celebrities.FirstOrDefault());
        }

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

    }
}