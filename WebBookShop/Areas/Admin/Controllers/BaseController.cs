using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using WebBookShop.Areas.Admin.Commons;

namespace WebBookShop.Areas.Admin.Controllers
{
    public class BaseController : Controller
    {
       
        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (!CheckSession.LoginSession())
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(
                    new {controller="login" , action="index"}
                    ));
            }
            base.OnActionExecuted(filterContext);
        }
    }
}