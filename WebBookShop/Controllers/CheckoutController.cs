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
                return View();
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
                return View(model);
            }
            else
            {
                return Redirect("/cart");
            }
            
        }

        [HttpPost]
        public ActionResult Index(int chose)
        {         
            if (chose == 1)
            {               
                return Redirect("/checkout/address");
            }
            else if(chose==2)
            {
                
                return Redirect("/chose2");
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

                Session["CartSession"] = null;
                return View(model);
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

                var orderId = service.ConfirmPayment(list, SharedData.customerAddress,null);
                SharedData.OrderId = orderId;
                return Redirect("/checkout/confirmation");
            }
            else 
            {
                return Redirect("/chose2");
            }
        }
    }
}