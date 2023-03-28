using CinemaTicket.Models.CinemaModels;
using CinemaTicket.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PagedList;

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
        public ActionResult Index(int? page, string SearchString = "")
        {
            ViewBag.RecentPosts = db.News.ToList();

            if (page == null) page = 1;

          

            int pageSize = 5;
            int pageNumber = (page ?? 1);

            if (SearchString != "")
            {
                var News = db.News.Where((x) => x.Title.ToUpper().Contains(SearchString.ToUpper())).OrderBy(m => m.MovieId);
                return View(News.ToPagedList(pageNumber, pageSize));
            }
            else
            {
                var News = db.News.ToList();
                return View(News.ToPagedList(pageNumber, pageSize));

            }

           

           
        }

        public ActionResult Details(int? id)
        {
            ViewBag.RecentPosts = db.News.ToList();
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