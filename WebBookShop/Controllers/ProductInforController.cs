using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
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
    }
}