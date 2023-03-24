using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBookShop.Areas.Admin.Models;
using WebBookShop.Models;
using WebBookShop.Services;

namespace WebBookShop.Areas.Admin.Controllers
{
    public class FeedbackController : Controller
    {
        // GET: Admin/Feedback
        public ActionResult Index(string keyword,int page=1, int pageSize = 10)
        {
            var service = new FeedbackService();

            ViewBag.PageSize = pageSize;
            ViewBag.Page = page;


            PageListModel.pageSize = pageSize;
            PageListModel.page = page;
            ShowOption();
            IEnumerable<FeedbackModel> feeds;
            if (keyword != null)
            {
                ViewBag.Search = keyword;
                PageListModel.keyword = keyword;

                feeds = service.Search(keyword, page, pageSize);
            }
            else
            {

                feeds = service.GetAll(page, pageSize);
            }
            return View(feeds);

        }

        [HttpPost]
        public ActionResult Index(string keyword)
        {
            var service = new FeedbackService();
            var feeds = service.Search(keyword, PageListModel.page, PageListModel.pageSize);
            ViewBag.Search = keyword;

            PageListModel.keyword = keyword;
            ShowOption();
            return View(feeds);
        }
        public void ShowOption()
        {
            var options = SharedData.Option(PageListModel.page, PageListModel.pageSize, PageListModel.keyword);
            TempData["showpagesize"] = options;

        }
        public ActionResult Delete(int id)
        {
            var service = new FeedbackService();
            var rs = service.Delete(id);
            if (rs)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }
    }
}