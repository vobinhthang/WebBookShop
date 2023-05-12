using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBookShop.Services;

namespace WebBookShop.Controllers
{
    public class NewsController : Controller
    {
        // GET: News
        public ActionResult Index(int page=1 , int pageSize=12)
        {
            var service = new NewsService();
            var news = service.GetAll(page,pageSize);

            ViewBag.PageSize = pageSize;
            ViewBag.Page = page;

            return View(news);
        }

        public ActionResult Detail(int id)
        {
            var service = new NewsService();
            var news = service.GetById(id);
            return View(news);
        }
    }
}