using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBookShop.Commons;
using WebBookShop.Models;

namespace WebBookShop.Controllers
{
    public class CheckoutController : Controller
    {
        // GET: Checkout
        public ActionResult Index()
        {
            return View();
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
            return View(model);
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
            if (SharedData.customerAddress!=null)
            {
                var model = SharedData.customerAddress;
                return View(model);
            }
            return View();
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
    }
}