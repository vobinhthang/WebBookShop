using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBookShop.EF;
using WebBookShop.Models;
using WebBookShop.Services;

namespace WebBookShop.Controllers
{
    public class ContactController : Controller
    {
        // GET: Contact
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(tbl_Feedback feedback)
        {
            var service = new FeedbackService();
            var contact = service.Create(feedback);
            if (contact != null)
            {
                TempData["CREATE"] = "Gửi liên hệ thành công!";
                TempData["ALEART"] = "success";
            }
            else
            {
                TempData["CREATE"] = "Gửi thất bại";
                TempData["ALEART"] = "danger";
            }
            return Redirect("/contact");
        }
    }
}