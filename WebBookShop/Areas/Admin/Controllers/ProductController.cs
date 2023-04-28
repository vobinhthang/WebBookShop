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
    public class ProductController : BaseController
    {
        // GET: Admin/Product
        public ActionResult Index(string sort, string sortBy, string keyword, int page = 1, int pageSize = 10)
        {
            var service = new ProductService();

            ViewBag.Sort = sort == "DES" ? "ASC" : "DES";
            ViewBag.PageSize = pageSize;
            ViewBag.Page = page;
            ViewBag.SortByPage = sortBy;

            PageListModel.pageSize = pageSize;
            PageListModel.page = page;
            PageListModel.sortBy = sortBy;
            PageListModel.sort = sort;

            ShowOption();

            IEnumerable<ProductModel> products;
            if (keyword != null)
            {
                ViewBag.Search = keyword;
                PageListModel.keyword = keyword;

                products = service.Search(keyword, page, pageSize);
            }
            else
            {
                products = service.GetAll(page, pageSize);
            }
            if (TempData["IconSortName"] == null && TempData["IconSortPrice"] == null && TempData["IconSortUpdate"] == null && TempData["IconSortCreate"] == null)
            { 
                TempData["IconSortName"] = "up";
                TempData["IconSortUpdate"] = "up";
                TempData["IconSortCreate"] = "up";
            }

            switch (sortBy)
            {
                case "productname":
                    switch (sort)
                    {
                        case "ASC":
                            products = service.Sorting("ASC","", "", "", page, pageSize);
                            ViewBag.PageSizeCate = pageSize;
                            PageListModel.pageSize = pageSize;
                            ViewBag.SortPageCate = "ASC";
                            PageListModel.sort = sort;
                            TempData["IconSortName"] = "up";
                            TempData["IconSortPrice"] = "up";
                            TempData["IconSortUpdate"] = "up";
                            TempData["IconSortCreate"] = "up";
                            break;
                        case "DES":
                            products = service.Sorting("DES", "", "", "", page, pageSize);
                            ViewBag.SortPageCate = "DES";
                            ViewBag.PageSizeCate = pageSize;
                            PageListModel.pageSize = pageSize;
                            PageListModel.sort = sort;
                            TempData["IconSortName"] = "down";
                            TempData["IconSortPrice"] = "up";
                            TempData["IconSortCateUpdate"] = "up";
                            TempData["IconSortCateCreate"] = "up";
                            break;
                        default:
                            products = service.Sorting("ASC", "", "", "", page, pageSize);
                            ViewBag.SortPageCate = "ASC";
                            ViewBag.PageSizeCate = pageSize;
                            PageListModel.pageSize = pageSize;
                            PageListModel.sort = sort;
                            TempData["IconSortPrice"] = "up";
                            TempData["IconSortName"] = "up";
                            TempData["IconSortCateUpdate"] = "up";
                            TempData["IconSortCateCreate"] = "up";
                            break;
                    }
                    break;
                case "updatedate":
                    switch (sort)
                    {
                        case "ASC":
                            products = service.Sorting("","", "ASC", "", page, pageSize);
                            ViewBag.SortPageCate = "ASC";
                            ViewBag.PageSizeCate = pageSize;
                            PageListModel.pageSize = pageSize;
                            PageListModel.sort = sort;
                            TempData["IconSortUpdate"] = "up";
                            TempData["IconSortName"] = "up";
                            TempData["IconSortPrice"] = "up";
                            TempData["IconSortCreate"] = "up";
                            break;
                        case "DES":
                            products = service.Sorting("","", "DES", "", page, pageSize);
                            ViewBag.SortPageCate = "DES";
                            ViewBag.PageSizeCate = pageSize;
                            PageListModel.pageSize = pageSize;
                            PageListModel.sort = sort;
                            TempData["IconSortUpdate"] = "down";
                            TempData["IconSortName"] = "up";
                            TempData["IconSortPrice"] = "up";
                            TempData["IconSortCreate"] = "up";
                            break;
                        default:
                            products = service.Sorting("", "", "ASC", "", page, pageSize);
                            ViewBag.SortPageCate = "ASC";
                            ViewBag.PageSizeCate = pageSize;
                            PageListModel.pageSize = pageSize;
                            PageListModel.sort = sort;
                            TempData["IconSortUpdate"] = "up";
                            TempData["IconSortName"] = "up";
                            TempData["IconSortPrice"] = "up";
                            TempData["IconSortCreate"] = "up";
                            break;
                    }
                    break;
                case "createdate":
                    switch (sort)
                    {
                        case "ASC":
                            products = service.Sorting("", "", "", "ASC", page, pageSize);
                            ViewBag.SortPageCate = "ASC";
                            ViewBag.PageSizeCate = pageSize;
                            PageListModel.pageSize = pageSize;
                            PageListModel.sort = sort;
                            TempData["IconSortUpdate"] = "up";
                            TempData["IconSortName"] = "up";
                            TempData["IconSortPrice"] = "up";
                            TempData["IconSortCreate"] = "up";
                            break;
                        case "DES":
                            products = service.Sorting("", "", "", "DES", page, pageSize);
                            ViewBag.SortPageCate = "DES";
                            ViewBag.PageSizeCate = pageSize;
                            PageListModel.pageSize = pageSize;
                            PageListModel.sort = sort;
                            TempData["IconSortUpdate"] = "up";
                            TempData["IconSortPrice"] = "up";
                            TempData["IconSortName"] = "up";
                            TempData["IconSortCreate"] = "down";
                            break;
                        default:
                            products = service.Sorting("", "", "", "ASC", page, pageSize);
                            ViewBag.SortPageCate = "ASC";
                            ViewBag.PageSizeCate = pageSize;
                            PageListModel.pageSize = pageSize;
                            PageListModel.sort = sort;
                            TempData["IconSortUpdate"] = "up";
                            TempData["IconSortName"] = "up";
                            TempData["IconSortPrice"] = "up";
                            TempData["IconSortCreate"] = "up";
                            break;
                    }
                    break;
                case "price":
                    switch (sort)
                    {
                        case "ASC":
                            products = service.Sorting("", "ASC", "", "", page, pageSize);
                            ViewBag.SortPageCate = "ASC";
                            ViewBag.PageSizeCate = pageSize;
                            PageListModel.pageSize = pageSize;
                            PageListModel.sort = sort;
                            TempData["IconSortUpdate"] = "up";
                            TempData["IconSortName"] = "up";
                            TempData["IconSortPrice"] = "up";
                            TempData["IconSortCreate"] = "up";
                            break;
                        case "DES":
                            products = service.Sorting("", "DES", "", "", page, pageSize);
                            ViewBag.SortPageCate = "DES";
                            ViewBag.PageSizeCate = pageSize;
                            PageListModel.pageSize = pageSize;
                            PageListModel.sort = sort;
                            TempData["IconSortUpdate"] = "up";
                            TempData["IconSortPrice"] = "down";
                            TempData["IconSortName"] = "up";
                            TempData["IconSortCreate"] = "up";
                            break;
                        default:
                            products = service.Sorting("", "ASC", "", "", page, pageSize);
                            ViewBag.SortPageCate = "ASC";
                            ViewBag.PageSizeCate = pageSize;
                            PageListModel.pageSize = pageSize;
                            PageListModel.sort = sort;
                            TempData["IconSortUpdate"] = "up";
                            TempData["IconSortName"] = "up";
                            TempData["IconSortPrice"] = "up";
                            TempData["IconSortCreate"] = "up";
                            break;
                    }
                    break;
                default:
                    break;
            }

            return View(products);
        }
        [HttpPost]
        public ActionResult Index(string keyword)
        {
            var service = new ProductService();
            var products = service.Search(keyword, PageListModel.page, PageListModel.pageSize);
            ViewBag.Search = keyword;

            PageListModel.keyword = keyword;
            ShowOption();
            return View(products);
        }
        [HttpGet]
        public ActionResult Create()
        {
            ListCateId();
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(tbl_Product product,ProductModel model)
        {
            ListCateId();
            var service = new ProductService();
            var productname = service.FindProductName(model.ProductName);

            if (model.ProductName == productname && model.ProductName != null)
            {
                TempData["CREATE_PRODUCT"] = "Tên sản phẩm đã tồn tại";
                TempData["ALEART"] = "warning";
            }
            if(model.ProductName != null && !model.ProductName.StartsWith(" ") && model.ProductName!=productname
                && model.Status!=null && model.Price!=null && model.Hot!=null && model.AuthorName!=null
                && model.PublishCompany != null && model.NumberPage != null && model.Description != null)
            {
                var rs = service.Create(product);
                if (rs != null)
                {
                    TempData["CREATE_PRODUCT"] = "Thêm mới sản phẩm thành công";
                    TempData["ALEART"] = "success";
                }
                else
                {
                    TempData["CREATE_PRODUCT"] = "Thêm thất bại";
                    TempData["ALEART"] = "danger";
                }
            }
            return View();
        }

        [HttpGet]
        public ActionResult Detail(int id)
        {
            var product = new ProductService().GetById(id);
            return View(product);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var product = new ProductService().GetById(id);
            
            ListCateId(product.CategoryId);
            ViewBag.Description = product.Description;
            return View(product);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(tbl_Product product, ProductModel model)
        {
            ListCateId(product.CategoryID);
            var service = new ProductService();

            if (model.ProductName != null && !model.ProductName.StartsWith(" ")
                && model.Price != null  && model.AuthorName != null
                 && model.NumberPage != null && model.Description != null)
            {
                var rs = service.Update(product);
                if (rs)
                {
                    ViewBag.Description = product.Description;
                    TempData["UPDATE"] = "Cập nhật sản phẩm thành công";
                    TempData["ALEART"] = "success";
                }
                else
                {
                    TempData["UPDATE"] = "Cập nhật thất bại";
                    TempData["ALEART"] = "danger";
                }
            }
            return View();
        }

        

        public ActionResult Delete(int id)
        {
            var rs = new ProductService().Delete(id);
            if (rs)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }

        [HttpGet]
        public ActionResult Image(int id)
        {
            var image = new ImageProductService().GetByProductId(id);
            var product = new ProductService().GetById(id);
            TempData["PRODUCT_ID"] = id;
            
            if (product != null)
            {
                TempData["PRODUCT_NAME"] = product.ProductName;
                SharedData.ProductName = product.ProductName;
            }
            
             SharedData.ProductId = id;
            return View(image);
        }

        [HttpGet]
        public ActionResult ImageCreate(int id)
        {
            var image = new ImageProductService().GetByProductId(id);
            var product = new ProductService().GetById(id);
            TempData["PRODUCT_ID"] = id;
            if (product != null)
            {
                TempData["PRODUCT_NAME"] = product.ProductName;
                SharedData.ProductName = product.ProductName;
            }

            SharedData.ProductId = id;
            return View();
        }
        [HttpPost]
        public ActionResult ImageCreate(tbl_ImageProduct image , HttpPostedFileBase fileImage)
        {
            var service = new ImageProductService();
           
            try
            {

                if (fileImage.ContentLength > 0)
                {
                    string rootFolder = Server.MapPath("~/UploadImg/Product/");
                    string pathFile = rootFolder + fileImage.FileName;
                    fileImage.SaveAs(pathFile);
                    image.Image = "~/UploadImg/Product/" + fileImage.FileName;

                    image.ProductId = SharedData.ProductId;
                    image.Thumbnail = false;

                    var findImage = service.FindImage(image.Image);
                    if (image.Image != findImage)
                    {
                        var rs = service.Create(image);
                        if (rs != null)
                        {
                            TempData["CREATE_IMAGE"] = "Thêm ảnh thành công";
                            TempData["ALEART"] = "success";
                        }
                        else
                        {
                            TempData["CREATE_IMAGE"] = "Thêm thất bại";
                            TempData["ALEART"] = "danger";
                        }
                    }
                    else
                    {
                        TempData["CREATE_IMAGE"] = "Ảnh được thêm đã tồn tại";
                        TempData["ALEART"] = "warning";
                    }
                }   
            }
            catch
            {
              
            }

            TempData["PRODUCT_NAME"] = SharedData.ProductName;
            TempData["PRODUCT_ID"] = SharedData.ProductId;

            return View();
        }

        [HttpGet]
        public ActionResult ImageEdit(int id)
        {
            var img = new ImageProductService().GetById(id);
            SharedData.ProductId = img.ProductId;

            TempData["PRODUCT_ID"] = SharedData.ProductId;
            TempData["IMAGE"] =img.Image;
            SharedData.Image = img.Image;
            return View(img);
        }

        [HttpPost]
        public ActionResult ImageEdit(tbl_ImageProduct image, HttpPostedFileBase fileImage)
        {
            var service = new ImageProductService();
            try
            {
                if (fileImage != null)
                {
                    string rootFolder = Server.MapPath("~/UploadImg/Product/");
                    string pathFile = rootFolder + fileImage.FileName;
                    fileImage.SaveAs(pathFile);
                    image.Image = "~/UploadImg/Product/" + fileImage.FileName;
                }
                else
                {
                    image.Image = SharedData.Image;
                }
                var rs = service.Update(image);
                if (rs)
                {
                    TempData["IMAGE"] = image.Image;
                    TempData["CREATE_IMAGE"] = "Cập nhật ảnh thành công";
                    TempData["ALEART"] = "success";
                }
                else
                {
                    TempData["IMAGE"] = SharedData.Image;
                    TempData["CREATE_IMAGE"] = "Cập nhật thất bại";
                    TempData["ALEART"] = "danger";
                }
                
            }
            catch
            {
                TempData["IMAGE"]=SharedData.Image;
             
            }
            
            TempData["PRODUCT_ID"] = SharedData.ProductId;
            
            return View();
        }

        public void ListCateId(int? selectId = null)
        {
            var service = new CateService();
            var cates = service.GetAll();
            ViewBag.CateList = new SelectList(cates.Where(c=>c.ParentID!=null), "Id", "CategoryName", selectId);
        }

        [HttpPost]
        public JsonResult ChangeThumbnail(int id)
        {
            var rs = new ProductService().ChangeThumbnail(id, (int)SharedData.ProductId);
            return Json(new
            {
                thumbnail = rs
            });
        }

        
        public ActionResult ImageDelete(int id)
        {
            var rs = new ImageProductService().Delete(id);
            if (rs)
                return RedirectToActionPermanent("Image");
            else
                return View();
        }

        [HttpPost]
        public JsonResult ChangeStatus(int id)
        {
            var rs = new ProductService().ChangeStatusHot(id,"status");
            return Json(new
            {
                status = rs
            });
        }

        [HttpPost]
        public JsonResult ChangeHot(int id)
        {
            var rs = new ProductService().ChangeStatusHot(id, "hot");
            return Json(new
            {
                status = rs
            });
        }

        public void ShowOption()
        {

            var options = SharedData.OptionPageNumber(PageListModel.sort, PageListModel.sortBy, PageListModel.page, PageListModel.pageSize, PageListModel.keyword);
            TempData["showpagesize"] = options;
            TempData["IconSortName"] = "up";
            TempData["IconSortPrice"] = "up";
            TempData["IconSortUpdate"] = "up";
            TempData["IconSortCreate"] = "up";
        }
    }
}