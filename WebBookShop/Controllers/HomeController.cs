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
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            var products = new ProductService().GetAll();
            return View(products);
        }

        [ChildActionOnly]
        public ActionResult MainMenu()
        {
            var cates = new CateService().GetAll();
            return PartialView(cates);
        }

        [ChildActionOnly]
        public ActionResult MainSlide()
        {
            var banners = new BannerService().GetSlide();
            return PartialView(banners);
        }
        [ChildActionOnly]
        public ActionResult BannerTop()
        {
            var banners = new BannerService().GetBanner();
            return PartialView(banners);
        }
        [ChildActionOnly]
        public ActionResult BannerMid()
        {
            var banners = new BannerService().GetBanner();
            return PartialView(banners);
        }
        [ChildActionOnly]
        public ActionResult BannerBot()
        {
            var banners = new BannerService().GetBanner();
            return PartialView(banners);
        }
        [ChildActionOnly]
        public ActionResult SellingProduct()
        {
            var lists = new List<OrderDetailModel>();
            
            var products = new OrderService().GetTopSelling();
            foreach(var p in products)
            {
                var getproduct = new ProductService().GetById((int)p.ProductId);
                var model = new OrderDetailModel
                {
                    ProductName = getproduct.ProductName,
                    Price = getproduct.Price,
                    ProductId = getproduct.Id
                };
                lists.Add(model);
            }

            return PartialView(lists);
        }
        [ChildActionOnly]
        public ActionResult NewProduct()
        {

            var products = new ProductService().GetNews();

            return PartialView(products);
        }

        [HttpGet]
        [ChildActionOnly]
        public ActionResult HeaderSearch()
        {

            return PartialView();
        }
        
       
        [HttpPost]
        public ActionResult HeaderSearch(string keyword)
        {
            SharedData.Keyword = keyword;
            ViewBag.Keyword = keyword;
            return Redirect("/productcate/search?keyword="+keyword);
        }
    }
}