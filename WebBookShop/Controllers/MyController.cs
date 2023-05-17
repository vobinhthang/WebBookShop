using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBookShop.Commons;
using WebBookShop.EF;
using WebBookShop.Models;
using WebBookShop.Services;

namespace WebBookShop.Controllers
{
    public class MyController : Controller
    {
        // GET: My
        public ActionResult Account()
        {
            var service = new UserService();
            if (Session["LOGIN_CLIENT"] != null)
            {
                var email =(string) Session["LOGIN_CLIENT"];
                var account = service.GetByEmail(email);
                ViewBag.NameAccount = account.Fullname;
                SharedData.UserId = account.Id;
                return View(account);
            }
            
            return Redirect("/login");
        }

        [HttpPost]        
        public ActionResult Account(tbl_User user)
        {
            var service = new UserService();
            user.Id = SharedData.UserId;
            var rs = service.UpdateAccount(user);
            if (rs)
            {
                Session["NameAccount"]=user.FullName;
                TempData["UPDATE"] = "Cập nhập thông tin thành công.";
                    
            }
            else
            {
                TempData["UPDATE"] = "Cập nhập thất bại";
                    
            }
   
            return Redirect("/my/account");
        }

        public ActionResult Order()
        {
            var service = new UserService();
            var orderService = new OrderService();
            if (Session["LOGIN_CLIENT"] != null)
            {
                var email = (string)Session["LOGIN_CLIENT"];
                var account = service.GetByEmail(email);
                ViewBag.NameAccount = account.Fullname;

                var orders = orderService.MyOrder(account.Id);

                var listDetail = new List<OrderDetailModel>();

                foreach(var item in orders)
                {         
                    var details = orderService.GetDetail(item.Id);
                    foreach(var d in details)
                    {
                        var modelDetail = new OrderDetailModel
                        {
                            ProductName = d.ProductName,
                            Quantity =d.Quantity,
                            OrderId=d.OrderId,
                            ProductId=d.ProductId,
                            Price =d.Price
                        };
                        listDetail.Add(modelDetail);
                    }                   
                }
                ViewBag.ListDetailOrder = listDetail;

                return View(orders);
            }
            return View();
        }
    }
}