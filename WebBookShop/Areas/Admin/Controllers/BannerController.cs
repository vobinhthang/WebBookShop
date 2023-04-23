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
    public class BannerController : Controller
    {
        // GET: Admin/Banner
        public ActionResult Index(string keyword,int  page=1, int pageSize=10)
        {
            var service = new BannerService();

            ViewBag.PageSize = pageSize;
            ViewBag.Page = page;


            PageListModel.pageSize = pageSize;
            PageListModel.page = page;
            ShowOption();
            IEnumerable<BannerModel> banners;
            if (keyword != null)
            {
                ViewBag.Search = keyword;
                PageListModel.keyword = keyword;

                banners = service.Search(keyword, page, pageSize);
            }
            else
            {

                banners = service.GetAll(page, pageSize);
            }
            return View(banners);
        }
        [HttpPost]
        public ActionResult Index(string keyword)
        {
            var service = new BannerService();
            var banners = service.Search(keyword, PageListModel.page, PageListModel.pageSize);
            ViewBag.Search = keyword;

            PageListModel.keyword = keyword;
            ShowOption();
            return View(banners);
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
        public ActionResult Create(tbl_Banner banner, BannerModel model, HttpPostedFileBase fileImage)
        {
            var service = new BannerService();
            try
            {
                if(fileImage.ContentLength>0 && model.BannerName !=null &&  model.Status!=null )
                {
                    if(model.BannerName.StartsWith("banner") || model.BannerName.StartsWith("slider"))
                    {
                        string rootFolder = Server.MapPath("~/UploadImg/Banner/");
                        string pathFile = rootFolder + fileImage.FileName;
                        fileImage.SaveAs(pathFile);
                        banner.Image = "~/UploadImg/Banner/" + fileImage.FileName;


                        var findImage = service.FindImage(banner.Image);
                        if (banner.Image != findImage)
                        {
                            var rs = service.Create(banner);
                            if (rs != null)
                            {
                                TempData["CREATE"] = "Thêm banner thành công";
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
                            TempData["CREATE"] = "Banner được thêm đã tồn tại";
                            TempData["ALEART"] = "warning";
                        }
                    }
                    else
                    {
                        TempData["MESSAGE"] = "Tên không hợp lệ!";
                    }
                }
                
            }
            catch
            {
                
            }
            return View();
        }

        [HttpPost]
        public JsonResult ChangeStatus(int id)
        {
            var rs = new BannerService().ChangeStatus(id);
            return Json(new
            {
                status = rs
            });
        }

       
        public ActionResult Delete(int id)
        {
            var service = new BannerService();
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

        public ActionResult Edit(int id)
        {
            var banner = new BannerService().GetById(id);
            TempData["IMAGE"] = banner.Image;
            SharedData.Image = banner.Image;
            return View(banner);
        }

        [HttpPost]
        public ActionResult Edit(tbl_Banner banner , BannerModel model, HttpPostedFileBase fileImage)
        {
            var service = new BannerService();

            try
            {
                if ( model.BannerName != null  && model.Status != null && (model.BannerName.StartsWith("banner") || model.BannerName.StartsWith("slider")))
                {
                   
                        if (fileImage != null)
                        {
                            string rootFolder = Server.MapPath("~/UploadImg/Product/");
                            string pathFile = rootFolder + fileImage.FileName;
                            fileImage.SaveAs(pathFile);
                            banner.Image = "~/UploadImg/Product/" + fileImage.FileName;
                        }
                        else
                        {
                            banner.Image = SharedData.Image;
                        }

                        var rs = service.Update(banner);
                        if (rs)
                        {
                            TempData["IMAGE"] = banner.Image;
                            TempData["CREATE"] = "Thêm banner thành công";
                            TempData["ALEART"] = "success";
                        }
                        else
                        {
                            TempData["IMAGE"] = SharedData.Image;
                            TempData["CREATE"] = "Thêm thất bại";
                            TempData["ALEART"] = "danger";
                        }

                }
                else
                {
                    TempData["IMAGE"] = SharedData.Image;
                    TempData["MESSAGE"] = "Tên không hợp lệ!";
                }
            }
            catch
            {
                TempData["IMAGE"] = SharedData.Image;          
            }

            return View();
        }
    }
}