using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBookShop.Areas.Admin.Models;
using WebBookShop.EF;
using WebBookShop.Models;
using WebBookShop.Services;

namespace WebBookShop.Areas.Admin.Controllers
{
    public class NewsController : BaseController
    {
        // GET: Admin/News
        public ActionResult Index(string keyword,int page=1 , int pageSize = 10)
        {
            var service = new NewsService();

            ViewBag.PageSize = pageSize;
            ViewBag.Page = page;


            PageListModel.pageSize = pageSize;
            PageListModel.page = page;
            ShowOption();
            IEnumerable<NewsModel> news;
            if (keyword != null)
            {
                ViewBag.Search = keyword;
                PageListModel.keyword = keyword;

                news = service.Search(keyword, page, pageSize);
            }
            else
            {

                news = service.GetAll(page, pageSize);
            }
            return View(news);
        }

        [HttpPost]
        public ActionResult Index(string keyword)
        {
            var service = new NewsService();
            var news = service.Search(keyword, PageListModel.page, PageListModel.pageSize);
            ViewBag.Search = keyword;

            PageListModel.keyword = keyword;
            ShowOption();
            return View(news);
        }
        public void ShowOption()
        {
            var options = SharedData.Option(PageListModel.page, PageListModel.pageSize, PageListModel.keyword);
            TempData["showpagesize"] = options;

        }
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(tbl_News news , HttpPostedFileBase fileImage,NewsModel model)
        {
            var service = new NewsService();
            try
            {
                if (fileImage.ContentLength > 0 && model.NewsName != null && model.Detail != null && model.Status != null
                    && model.Descripton != null)
                {

                    string rootFolder = Server.MapPath("~/UploadImg/News/");
                    string pathFile = rootFolder + fileImage.FileName;
                    fileImage.SaveAs(pathFile);
                    news.Image = "~/UploadImg/News/" + fileImage.FileName;


                    var findName = service.FindName(news.NewsName);
                    if (news.NewsName != findName)
                    {
                        var rs = service.Create(news);
                        if (rs != null)
                        {
                            TempData["CREATE"] = "Thêm tin tức thành công";
                            TempData["ALEART"] = "success";
                        }
                        else
                        {
                            TempData["CREATE"] = "Thêm thất bại";
                            TempData["ALEART"] = "danger";
                        }
                    }
                    else
                    {
                        TempData["CREATE"] = "Tin tức được thêm đã tồn tại";
                        TempData["ALEART"] = "warning";
                    }
                }

            }
            catch
            {
               
            }
            return View();
           
        }

        public ActionResult Edit(int id)
        {
            var news = new NewsService().GetById(id);
            TempData["IMAGE"] = news.Image;
            SharedData.Image = news.Image;
            ViewBag.Detail = news.Detail;
            return View(news);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(tbl_News news, HttpPostedFileBase fileImage, NewsModel model)
        {
            var service = new NewsService();
            try
            {
                if ( model.NewsName != null && model.Detail != null && model.Status != null
                    && model.Descripton != null)
                {

                  
                    if (fileImage != null)
                    {
                        string rootFolder = Server.MapPath("~/UploadImg/News/");
                        string pathFile = rootFolder + fileImage.FileName;
                        fileImage.SaveAs(pathFile);
                        news.Image = "~/UploadImg/News/" + fileImage.FileName;
                    }
                    else
                    {
                        news.Image = SharedData.Image;
                    }
                    var rs = service.Update(news);
                    if (rs)
                    {
                        TempData["IMAGE"] = news.Image;
                        ViewBag.Detail = news.Detail;
                        TempData["CREATE"] = "Cập nhật tin tức thành công";
                        TempData["ALEART"] = "success";
                    }
                    else
                    {
                        TempData["IMAGE"] = SharedData.Image;
                        TempData["CREATE"] = "Cập nhật thất bại";
                        TempData["ALEART"] = "danger";
                    }
                }

            }
            catch
            {
                ViewBag.Detail = news.Detail;
                TempData["IMAGE"] = SharedData.Image;
            }
            return View();
        }

        [HttpPost]
        public JsonResult ChangeStatus(int id)
        {
            var rs = new NewsService().ChangeStatus(id);
            return Json(new
            {
                status = rs
            });
        }

        public ActionResult Delete(int id)
        {
            var service = new NewsService();
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