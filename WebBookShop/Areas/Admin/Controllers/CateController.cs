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
    public class CateController : Controller
    {
        // GET: Admin/Cate
        public ActionResult Index(string sort, string sortBy, string keyword, int page = 1, int pageSize = 5)
        {
            var service = new CateService();

            ViewBag.SortCate = sort == "DES" ? "ASC" : "DES";
            ViewBag.PageSizeCate = pageSize;
            ViewBag.PageCate = page;
            ViewBag.SortByPageCate = sortBy;

            PageListModel.pageSize = pageSize;
            PageListModel.page = page;
            PageListModel.sortBy = sortBy;
            PageListModel.sort = sort;

            ShowOption();
            IEnumerable<CateModel> cates;
            if (keyword != null)
            {
                ViewBag.SearchRole = keyword;
                PageListModel.keyword = keyword;

                cates = service.Search(keyword, page, pageSize);
            }
            else
            {
                cates = service.GetAll(page, pageSize);
            }
            if (TempData["IconSortCateName"] == null && TempData["IconSortUserUpdate"] == null && TempData["IconSortUserCreate"] == null)
            {
                TempData["IconSortCateName"] = "up";
                TempData["IconSortCateUpdate"] = "up";
                TempData["IconSortCateCreate"] = "up";
            }

            switch (sortBy)
            {
                case "catename":
                    switch (sort)
                    {
                        case "ASC":
                            cates = service.Sorting("ASC", "", "", page, pageSize);
                            ViewBag.PageSizeCate = pageSize;
                            PageListModel.pageSize = pageSize;
                            ViewBag.SortPageCate = "ASC";
                            PageListModel.sort = sort;
                            TempData["IconSortCateName"] = "up";
                            TempData["IconSortCateUpdate"] = "up";
                            TempData["IconSortCateCreate"] = "up";
                            break;
                        case "DES":
                            cates = service.Sorting("DES", "", "", page, pageSize);
                            ViewBag.SortPageCate = "DES";
                            ViewBag.PageSizeCate = pageSize;
                            PageListModel.pageSize = pageSize;
                            PageListModel.sort = sort;
                            TempData["IconSortCateName"] = "down";
                            TempData["IconSortCateUpdate"] = "up";
                            TempData["IconSortCateCreate"] = "up";
                            break;
                        default:
                            cates = service.Sorting("ASC", "", "", page, pageSize);
                            ViewBag.SortPageCate = "ASC";
                            ViewBag.PageSizeCate = pageSize;
                            PageListModel.pageSize = pageSize;
                            PageListModel.sort = sort;
                            TempData["IconSortCateName"] = "up";
                            TempData["IconSortCateUpdate"] = "up";
                            TempData["IconSortCateCreate"] = "up";
                            break;
                    }
                    break;
                case "updatedate":
                    switch (sort)
                    {
                        case "ASC":
                            cates = service.Sorting("", "ASC", "", page, pageSize);
                            ViewBag.SortPageCate = "ASC";
                            ViewBag.PageSizeCate = pageSize;
                            PageListModel.pageSize = pageSize;
                            PageListModel.sort = sort;
                            TempData["IconSortCateUpdate"] = "up";
                            TempData["IconSortCateName"] = "up";
                            TempData["IconSortCateCreate"] = "up";
                            break;
                        case "DES":
                            cates = service.Sorting("", "DES", "", page, pageSize);
                            ViewBag.SortPageCate = "DES";
                            ViewBag.PageSizeCate = pageSize;
                            PageListModel.pageSize = pageSize;
                            PageListModel.sort = sort;
                            TempData["IconSortCateUpdate"] = "down";
                            TempData["IconSortCateName"] = "up";
                            TempData["IconSortCateCreate"] = "up";
                            break;
                        default:
                            cates = service.Sorting("", "ASC", "", page, pageSize);
                            ViewBag.SortPageCate = "ASC";
                            ViewBag.PageSizeCate = pageSize;
                            PageListModel.pageSize = pageSize;
                            PageListModel.sort = sort;
                            TempData["IconSortCateUpdate"] = "up";
                            TempData["IconSortCateName"] = "up";
                            TempData["IconSortCateCreate"] = "up";
                            break;
                    }
                    break;
                case "createdate":
                    switch (sort)
                    {
                        case "ASC":
                            cates = service.Sorting("", "", "ASC", page, pageSize);
                            ViewBag.SortPageCate = "ASC";
                            ViewBag.PageSizeCate = pageSize;
                            PageListModel.pageSize = pageSize;
                            PageListModel.sort = sort;
                            TempData["IconSortCateUpdate"] = "up";
                            TempData["IconSortCateName"] = "up";
                            TempData["IconSortCateCreate"] = "up";
                            break;
                        case "DES":
                            cates = service.Sorting("", "", "DES", page, pageSize);
                            ViewBag.SortPageCate = "DES";
                            ViewBag.PageSizeCate = pageSize;
                            PageListModel.pageSize = pageSize;
                            PageListModel.sort = sort;
                            TempData["IconSortCateUpdate"] = "up";
                            TempData["IconSortCateName"] = "up";
                            TempData["IconSortCateCreate"] = "down";
                            break;
                        default:
                            cates = service.Sorting("", "", "ASC", page, pageSize);
                            ViewBag.SortPageCate = "ASC";
                            ViewBag.PageSizeCate = pageSize;
                            PageListModel.pageSize = pageSize;
                            PageListModel.sort = sort;
                            TempData["IconSortCateUpdate"] = "up";
                            TempData["IconSortCateName"] = "up";
                            TempData["IconSortCateCreate"] = "up";
                            break;
                    }
                    break;
                default:
                    break;
            }


            return View(cates);
        }

        [HttpPost]
        public ActionResult Index(string keyword)
        {
            var service = new CateService();
            var cates = service.Search(keyword, PageListModel.page, PageListModel.pageSize);
            ViewBag.SearchCate = keyword;

            PageListModel.keyword = keyword;
            ShowOption();
            return View(cates);
        }
        public void ShowOption()
        {

            var options = SharedData.OptionPageNumber(PageListModel.sort, PageListModel.sortBy, PageListModel.page, PageListModel.pageSize, PageListModel.keyword);
            TempData["showpagesize"] = options;
            TempData["IconSortEmail"] = "up";
            TempData["IconSortCateUpdate"] = "up";
            TempData["IconSortCateCreate"] = "up";
        }
        [HttpPost]
        public JsonResult ChangeStatus(int id)
        {
            var rs = new CateService().ChangeStatus(id);
            return Json(new
            {
                status = rs
            });
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(tbl_Category cate, CateModel model)
        {
            var service = new CateService();

            var catename = service.FindCateName(model.CategoryName);

            if (model.CategoryName == catename && model.CategoryName!=null)
            {
                TempData["CREATE_CATE"] = "Tên thể loại đã tồn tại";
                TempData["ALEART"] = "warning";
            }
            if (model.CategoryName != null && !model.CategoryName.StartsWith(" ") 
                && model.Status!=null && model.Sort != null && model.CategoryName != catename)
            {
                var _cate = service.Create(cate);
                if (_cate != null)
                {
                    TempData["CREATE_CATE"] = "Thêm mới thể loại thành công";
                    TempData["ALEART"] = "success";
                }
                else
                {
                    TempData["CREATE_CATE"] = "Thêm mới thất bại";
                    TempData["ALEART"] = "danger";
                }
            }
                       
            return View();
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var cate = new CateService().GetById(id);
            return View(cate);
        }

        [HttpPost]
        public ActionResult Edit(tbl_Category cate, CateModel model)
        {
            var service = new CateService();

            if(model.CategoryName!=null && !model.CategoryName.StartsWith(" ") && model.Sort!=null && model.Status != null)
            {
                var rs = service.Update(cate);
                if (rs)
                {
                    TempData["UPDATE_CATE"] = "Cập nhập thể loại có ID: " + cate.Id + " thành công.";
                    TempData["ALEART"] = "success";
                }
                else
                {
                    TempData["UPDATE_CATE"] = "Cập nhập thất bại";
                    TempData["ALEART"] = "danger";
                }
            }

            return View();
        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            var service = new CateService();
            var rs = service.Delete(id);
            if (rs)
            {
                return RedirectToAction("index");
            }
            else
            {
                return View();
            }
        }
    }
}