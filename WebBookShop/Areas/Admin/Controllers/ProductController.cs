using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBookShop.Services;

namespace WebBookShop.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {
        // GET: Admin/Product
        public ActionResult Index()
        {
            var service = new ProductService();
            var product = service.GetAll();
            return View(product);
        }

        [HttpGet]
        public ActionResult Create()
        {
            ListCateId();
            return View();
        }
        public void ListCateId(int? selectId = null)
        {
            var service = new CateService();
            var cates = service.GetAll();
            ViewBag.CateList = new SelectList(cates, "Id", "CategoryName", selectId);
        }
    }
}