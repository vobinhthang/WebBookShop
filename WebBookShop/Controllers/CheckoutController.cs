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
    public class CheckoutController : Controller
    {
        // GET: Checkout
        public ActionResult Index()
        {
            var cartSession = Session["CartSession"];
            if (cartSession != null)
            {
                if(Session["LOGIN_CLIENT"] == null)
                {
                    return View();
                }
                var service = new UserService();
                var email = (string)Session["LOGIN_CLIENT"];
                var account = service.GetByEmail(email);
                
                return View(account);
            }
            else
            {
                return Redirect("/cart");
            }
            
        }

        [ChildActionOnly]
        public ActionResult ItemCheckOut()
        {
            var cartSession = Session["CartSession"];
            var list = new List<CartItem>();
            if (cartSession != null)
            {
                list = (List<CartItem>)cartSession;
            }

            return PartialView(list);
        }

        
        public ActionResult Payment()
        {
            var model = SharedData.customerAddress;
            var cartSession = Session["CartSession"];
            if (cartSession != null)
            {
                if (Session["LOGIN_CLIENT"] == null)
                {                   
                    return View(model);
                }

                var service = new UserService();
                var email = (string)Session["LOGIN_CLIENT"];
                var account = service.GetByEmail(email);
                var customerAddress = new CustomerAddress
                {
                    _name = account.Fullname,
                    _phone = account.Phone,
                    _email = account.Email,
                    _address = account.Address,
                };

                SharedData.UserId = account.Id;
                SharedData.customerAddress = customerAddress;
                return View(customerAddress);
            }
            else
            {
                return Redirect("/cart");
            }
            
        }

        [HttpPost]
        public ActionResult Index(int chose, UserModel user)
        {         
            if (chose == 1)
            {               
                return Redirect("/checkout/address");
            }
            else if(chose==2)
            {
                var service = new UserService();
                var result = service.Login(user.Email, user.Password, "Khách hàng");

                if (user.Email != null && user.Password != null)
                {
                    if (result)
                    {
                        var name = service.FindName(user.Email);
                        Session["LOGIN_CLIENT"] = user.Email;
                        Session["NameAccount"] = name;
                        SharedData.Email = null;
                        SharedData.Password = null;
                        
                    }
                    else
                    {
                        
                        TempData["MESSAGE"] = "Email hoặc mật khẩu không đúng!";
                    }
                }
                return Redirect("/checkout");
            }
            return View();
        }

        public ActionResult Address()
        {
            
            var cartSession = Session["CartSession"];
            if (cartSession != null)
            {
                if (SharedData.customerAddress != null)
                {
                    var model = SharedData.customerAddress;
                    return View(model);
                }
                return View();
            }
            else
            {
                return Redirect("/cart");
            }
            
        }

        [HttpPost]
        public ActionResult Address(string name, string phone, string email, string address)
        {
            var customerAddress = new CustomerAddress
            {
                _name = name,
                _phone=phone,
                _email=email,
                _address = address,
            };
            
            SharedData.customerAddress = customerAddress;

            return Redirect("/checkout/payment");
        }

        public ActionResult Confirmation()
        {
            var model = SharedData.customerAddress;

            var cartSession = Session["CartSession"];
            var list = new List<CartItem>();
            if (cartSession != null)
            {
                list = (List<CartItem>)cartSession;
                var count = list.Sum(x => x.Quantity);
                ViewBag.CountItem = count;
                ViewBag.ListItems = list;
                ViewBag.OrderId = SharedData.OrderId;
               
                if (Session["LOGIN_CLIENT"] == null)
                {
                    Session["CartSession"] = null;
                    return View(model);
                }

                var service = new UserService();
                var email = (string)Session["LOGIN_CLIENT"];
                var account = service.GetByEmail(email);
                var customerAddress = new CustomerAddress
                {
                    _name = account.Fullname,
                    _phone = account.Phone,
                    _email = account.Email,
                    _address = account.Address,
                };

                SharedData.UserId = account.Id;
                Session["CartSession"] = null;
                return View(customerAddress);
            }
            else
            {
                return Redirect("/cart");
            }

        }

        [HttpPost]
        public ActionResult Payment(int chose)
        {
            if(chose == 1)
            {
                var service = new OrderService();

                var cartSession = Session["CartSession"];
                var list = new List<CartItem>();
                if (cartSession != null)
                {
                    list = (List<CartItem>)cartSession;

                }
                if (Session["LOGIN_CLIENT"] == null)
                {
                    var orderId = service.ConfirmPayment(list, SharedData.customerAddress, null, false);
                    SharedData.OrderId = orderId;
                }
                else
                {
                    var orderId = service.ConfirmPayment(list, SharedData.customerAddress, SharedData.UserId, true);
                    SharedData.OrderId = orderId;
                }
                
                return Redirect("/checkout/confirmation");
            }
            else 
            {
                return Redirect("/chose2");
            }
        }
    }
}