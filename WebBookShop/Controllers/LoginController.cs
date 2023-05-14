using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBookShop.Commons;
using WebBookShop.Models;
using WebBookShop.Services;

namespace WebBookShop.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            var user = new UserModel
            {
                Email = SharedData.Email,
                Password = SharedData.Password,
            };
            if (user.Email != null && user.Password!=null)
            {
                return View(user);
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(UserModel user)
        {
            var service = new UserService();
            var result = service.Login(user.Email, user.Password,"Khách hàng");

            if (user.Email != null && user.Password != null)
            {
                if (result)
                {
                    var name = service.FindName(user.Email);
                    Session["LOGIN_CLIENT"] = user.Email;
                    Session["NameAccount"] = name;
                    SharedData.Email = null;
                    SharedData.Password = null;
                    return RedirectToAction("index", "home");
                }
                else
                {
                    TempData["MESSAGE"] = "Email hoặc mật khẩu không đúng.";
                }
            }


            return View(user);
        }
    }
}