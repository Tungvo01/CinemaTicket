using CinemaTicket.Identity;
using CinemaTicket.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace CinemaTicket.Areas.Admin.Controllers
{
    public class AccountController : Controller
    {
        private ApplicationUserManager _userManager;
        // GET: Admin/Account
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult DashBoard()
        {
            ViewBag.PageView = HttpContext.Application["PageView"].ToString();
            ViewBag.Online = HttpContext.Application["Online"].ToString();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return Redirect("~/Account/Login");
        }

        //public ActionResult Profile()
        //{
        //    var user =  UserManager.FindByIdAsync(User.Identity.GetUserId());
        //    return View(user);
        //}

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        //public ApplicationUserManager UserManager
        //{
        //    get
        //    {
        //        return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
        //    }
        //    private set
        //    {
        //        _userManager = value;
        //    }
        //}


    }
}