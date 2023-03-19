using CinemaTicket.Models.CinemaModels;
using CinemaTicket.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace CinemaTicket.Controllers
{
    public class NewsController : Controller
    {
        
      
        private ApplicationDbContext db;
        public NewsController()
        {
            db = new ApplicationDbContext();
        }
        // GET: News
        public ActionResult Index()
        {

            return View(db.News.ToList());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            News news = db.News.Find(id);
            if (news == null)
            {
                return HttpNotFound();
            }
            return View(news);
        }

    }
}