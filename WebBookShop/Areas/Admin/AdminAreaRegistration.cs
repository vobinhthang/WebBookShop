using System.Web.Mvc;

namespace WebBookShop.Areas.Admin
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
                new { action = "Index", id = UrlParameter.Optional }
            );
            context.MapRoute(
                "myrole",
                "admin/{controller}/{action}/{id}",
                new { action = "index", id = UrlParameter.Optional }
            );
        }
    }
}