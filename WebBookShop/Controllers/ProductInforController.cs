using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBookShop.Models;
using WebBookShop.Services;

namespace WebBookShop.Controllers
{
    public class ProductInforController : Controller
    {
        // GET: ProductInfor
        public ActionResult Index(int id)
        {
            var service = new ProductService();
            var product = service.GetById(id);
            var parentCates = new CateService().GetCateParentId((int)product.CategoryId);
            ViewBag.ParentIdCates = parentCates.Id;
            ViewBag.ParentIdCatesName = parentCates.CategoryName;

            var productbycate = service.GetByCate((int)product.CategoryId);
            ViewBag.ProductByCate = productbycate.Where(x=>x.Status==true).Take(10);

            var images = new ImageProductService().GetByProductId(id);
            ViewBag.Images = images;
            return View(product);
        }

        [HttpPost]
        public ActionResult Index(int productId, int quantity)
        {

            var product = new ProductService().GetById(productId);
            var parentCates = new CateService().GetCateParentId((int)product.CategoryId);
            var images = new ImageProductService().GetByProductId(productId);
            var productbycate = new ProductService().GetByCate((int)product.CategoryId);
            ViewBag.ProductByCate = productbycate.Where(x => x.Status == true).Take(10);
            ViewBag.ParentIdCatesName = parentCates.CategoryName;
            ViewBag.ParentIdCates = parentCates.Id;
            ViewBag.Images = images;

            var cartSession = Session["CartSession"];
            
            if (cartSession != null)
            {
                var listItem = (List<CartItem>)cartSession;
                if (listItem.Exists(x => x.ProductModel.Id == productId))
                {
                    foreach(var item in listItem)
                    {
                        if (item.ProductModel.Id == productId)
                        {
                            item.Quantity += quantity;
                        }
                        
                    }
                    
                }
                else
                {
                    var item = new CartItem
                    {
                        ProductModel = product,
                        Quantity = quantity,
                    };
                    listItem.Add(item);
                }
                Session["CartSession"] = listItem;
                
            }
            else
            {
                var item = new CartItem
                {
                    ProductModel = product,
                    Quantity = quantity,
                };
                var listItems = new List<CartItem>();
                listItems.Add(item);
                Session["CartSession"] = listItems;
                

            }
            TempData["Alert"] = "Đã thêm sản phẩm vào giỏ hàng";
            return Redirect("/productinfor?id="+ productId);
        }
    }
}