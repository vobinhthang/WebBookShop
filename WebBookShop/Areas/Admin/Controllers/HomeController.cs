using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBookShop.Services;

namespace WebBookShop.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        // GET: Admin/Home
        public ActionResult Index()
        {
            var countOrder = new OrderService().CountOrderMonth();
            var TotalPriceOrder = new OrderService().TotalPriceMonth();
            var countUser = new UserService().CountUserMonth();
            var TotalPriceInvocie = new InvoiceService().TotalPriceMonth();
            ViewBag.CountOrder = countOrder;
            ViewBag.CountUser = countUser;
            ViewBag.TotalPriceOrder = TotalPriceOrder;
            ViewBag.TotalPriceInvocie = TotalPriceInvocie;
            return View();
        }

        
    }
}