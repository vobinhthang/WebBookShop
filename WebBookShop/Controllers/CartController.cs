using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBookShop.Models;

namespace WebBookShop.Controllers
{
    public class CartController : Controller
    {
        // GET: Cart
        public ActionResult Index()
        {
            var cartSession = Session["CartSession"];
            var list = new List<CartItem>();
            if(cartSession != null)
            {
                list = (List<CartItem>)cartSession;
            }
            if (list.Count == 0)
            {
                Session["CartSession"] =null;
            }
            return View(list);
        }

        [HttpPost]
        public JsonResult Update(int id, int quantity)
        {
            var cartSession = (List<CartItem>)Session["CartSession"];
            if (cartSession.Count>0)
            {
                var list = cartSession.SingleOrDefault(x => x.ProductModel.Id == id);
                if (list != null)
                {
                    list.Quantity = quantity;
                }
                return Json(new { Success = true });
            }
            return Json(new { Success = false });
            
        }


        [HttpPost]
        public JsonResult Delete(int id)
        {
            var cartSession = (List<CartItem>)Session["CartSession"];
            cartSession.RemoveAll(x => x.ProductModel.Id == id);
            Session["CartSession"] = cartSession;
            return Json(new { Success = true });
        }
    }
}