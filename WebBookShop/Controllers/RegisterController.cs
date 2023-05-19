using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBookShop.Areas.Admin.Commons;
using WebBookShop.Commons;
using WebBookShop.EF;
using WebBookShop.Models;
using WebBookShop.Services;

namespace WebBookShop.Controllers
{
    public class RegisterController : Controller
    {
        // GET: Register
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(tbl_User user, UserModel model)
        {
            var service = new UserService();
            var findEmail = service.FindEmail(model.Email);
            if(model.Email== findEmail && model.Email!=null)
            {
                TempData["MESSAGE"] = "Email đã tồn tại";
            }
            if (model.Email != null && model.Email != findEmail && model.Password != null && model.Password.Length >= 6)
            {
                user.Password = Encryptor.GetMd5Hash(user.Password);
                var rs = service.CreateClient(user);
                if (rs != null)
                {
                    SharedData.Email = model.Email;
                    SharedData.Password = model.Password;
                    return RedirectToAction("index", "login");
                }
                
            }

            return View();
        }
    }
}