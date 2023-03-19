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
    public class InvoiceController : Controller
    {
        // GET: Admin/Invoice
        public ActionResult Index(string keyword, int page = 1, int pageSize = 10)
        {
            var service = new InvoiceService();

            ViewBag.PageSize = pageSize;
            ViewBag.Page = page;


            PageListModel.pageSize = pageSize;
            PageListModel.page = page;
            //ShowOption();
            IEnumerable<InvoiceModel> invoices;
            if (keyword != null)
            {
                ViewBag.Search = keyword;
                PageListModel.keyword = keyword;

                invoices = service.Search(keyword, page, pageSize);
            }
            else
            {

                invoices = service.GetAll(page, pageSize);
            }
            return View(invoices);
        }

        [HttpPost]
        public ActionResult Index(string keyword)
        {
            var service = new CommentService();
            var invoices = service.Search(keyword, PageListModel.page, PageListModel.pageSize);
            ViewBag.Search = keyword;

            PageListModel.keyword = keyword;
            //ShowOption();
            return View(invoices);
        }

        public ActionResult Delete(int id)
        {
            var service = new InvoiceService();
            TempData["INVOICE_ID"] = id;
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
            var rs = new InvoiceService().ChangeStatus(id);
            return Json(new
            {
                status = rs
            });
        }

        public ActionResult Detail(int id)
        {
            TempData["INVOICE_ID"] = id;
            SharedData.InvoiceId = id;
            var detail = new InvoiceService().GetDetail(id);
            if (detail.Count > 0)
            {
                TempData["CREATE_DATE"] = detail.Take(1).SingleOrDefault(d=>d.InvoiceId==id).CreateDate;
                return View(detail);
            }
            else
            {
                var notProducs = new InvoiceService().GetDetailNotPr(id);
                TempData["CREATE_DATE"] = notProducs.SingleOrDefault(d => d.Id == id).CreateDate;
                notProducs.RemoveAt(0);
                return View(notProducs);
            }
    
        }

        public ActionResult CreateDetail(int id, int? cateId)
        {
            TempData["INVOICE_ID"] = id;
            SharedData.InvoiceId = id;
            ListProductId(null, cateId);
            ListCateId(cateId);
            return View();
        }
        public void ListCateId(int? selectId = null)
        {
            var service = new CateService();
            var cates = service.GetAll();

            ViewBag.CateList = new SelectList(cates, "Id", "CategoryName", selectId);
        }
        public void ListProductId(int? selectId = null, int? cateid = null)
        {
            var service = new ProductService();
            List<ProductModel> products;
            if (cateid != null)
            {
                products = service.SearchByCate((int)cateid);
            }
            else
            {
                products = service.GetAll();
            }
            ViewBag.ProductList = new SelectList(products, "Id", "ProductName", selectId);
        }

        [HttpPost]
        public ActionResult CreateDetail(tbl_InvoiceDetail detail , InvoiceDetailModel model)
        {
            ListCateId();
            ListProductId();
            TempData["INVOICE_ID"] = SharedData.InvoiceId;
            detail.InvoiceId = SharedData.InvoiceId;

            var service = new InvoiceService();
            var findProductId = service.FindProductId((int)detail.ProductId, (int)detail.InvoiceId);

            if (model.ProductId == findProductId && model.Quantity != null)
            {
                TempData["CREATE"] = "Sản phẩm đã tồn tại trong hóa đơn nhập";
                TempData["ALEART"] = "warning";
            }

            if (model.Quantity != null && model.Quantity > 0 && model.ProductId != findProductId)
            {

                var rs = service.CreateDetail(detail);
                if (rs != null)
                {
                    TempData["CREATE"] = "Thêm sản phẩm thành công";
                    TempData["ALEART"] = "success";
                }
                else
                {
                    TempData["CREATE"] = "Thêm thất bại";
                    TempData["ALEART"] = "danger";
                }
            }
            return View();
        }

        public ActionResult DeleteDetail(int id)
        {
            var service = new InvoiceService();
            var rs = service.DeleteDetail(id);
            if (rs)
            {
                return RedirectPermanent("/admin/invoice/detail/" + SharedData.InvoiceId.ToString());
            }
            else
            {
                return View();
            }
        }

    }
}