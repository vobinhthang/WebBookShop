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
    public class CommentController : BaseController
    {
        // GET: Admin/Comment
        public ActionResult Index(string keyword, int page =1 , int pageSize =10)
        {
            var service = new CommentService();

            ViewBag.PageSize = pageSize;
            ViewBag.Page = page;


            PageListModel.pageSize = pageSize;
            PageListModel.page = page;
            ShowOption();
            IEnumerable<ProductCommentModel> comments;
            if (keyword != null)
            {
                ViewBag.Search = keyword;
                PageListModel.keyword = keyword;

                comments = service.Search(keyword, page, pageSize);
            }
            else
            {

                comments = service.GetAll(page, pageSize);
            }
            return View(comments);
        }

        [HttpPost]
        public ActionResult Index(string keyword)
        {
            var service = new CommentService();
            var comments = service.Search(keyword, PageListModel.page, PageListModel.pageSize);
            ViewBag.Search = keyword;

            PageListModel.keyword = keyword;
            ShowOption();
            return View(comments);
        }
        public void ShowOption()
        {
            var options = SharedData.Option(PageListModel.page, PageListModel.pageSize, PageListModel.keyword);
            TempData["showpagesize"] = options;

        }
        public ActionResult Delete(int id)
        {
            var service = new CommentService();
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

        [HttpPost]
        public JsonResult ChangeStatus(int id)
        {
            var rs = new CommentService().ChangeStatus(id);
            return Json(new
            {
                status = rs
            });
        }
    }
}