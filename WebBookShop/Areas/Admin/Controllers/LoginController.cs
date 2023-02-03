using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using WebBookShop.Models;
using WebBookShop.Services;

namespace WebBookShop.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {

        // GET: Admin/Login
        [HttpGet]
        public ActionResult Index()
        {
            Session["LOGINSESSION"] = null;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(UserModel user)
        {
            var service = new UserService();
            var result = service.Login(user.Email,user.Password);

            if(user.Email!=null && user.Password != null) {
                if (result)
                {
                    Session["LOGINSESSION"] = user.Email;
                    return RedirectToAction("index", "home");
                }
                else
                {
                    TempData["LOGINMESSAGE"] = "Email hoặc mật khẩu không đúng.";
                }
            }
            else
            {
                TempData["LOGINMESSAGE"] = "Xin mời nhập đầy đủ thông tin.";
            }
            

            return View(user);
        }
    }
}