using System.Web.Mvc;

namespace CinemaTicket.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Admin_default",
                "Admin/{controller}/{action}/{id}",
                new { action = "Index", Controller = "Movies",id = UrlParameter.Optional },
                new[] { "CinemaTicket.Areas.Admin.Controllers" }

            );
        }
    }
}