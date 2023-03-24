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
    public class OrderController : Controller
    {
        // GET: Admin/Order
        public ActionResult Index(string keyword,int page = 1, int pageSize=10 )
        {
            var service = new OrderService();
     
            ViewBag.PageSize = pageSize;
            ViewBag.Page = page;

            
            PageListModel.pageSize = pageSize;
            PageListModel.page = page;
            ShowOption();
            IEnumerable<OrderModel> orders;
            if (keyword!=null)
            {
                ViewBag.Search = keyword;
                PageListModel.keyword =keyword;

                orders = service.Search(keyword, page, pageSize);
            }
            else
            {
                
                orders = service.GetAll(page, pageSize);
            }
            return View(orders);
        }
        [HttpPost]
        public ActionResult Index(string keyword)
        {
            var service = new OrderService();
            var orders = service.Search(keyword, PageListModel.page, PageListModel.pageSize);
            ViewBag.Search = keyword;

            PageListModel.keyword = keyword;
            ShowOption();
            return View(orders);
        }
        public ActionResult Detail(int id)
        {
            SharedData.OrderId = id;
            var orderdetail = new OrderService().GetDetail(id);
            if(orderdetail.Count >0)
            {
                var list = orderdetail.Take(1).SingleOrDefault(od => od.OrderId == id);
                
                TempData["ORDERID"] = list.OrderId;
                TempData["ORDER_DATE"] = list.OrderDate;
                TempData["NAME"] = list.CustomerName;
                TempData["PHONE"] = list.Phone;
                TempData["ADDRESS"] = list.Address;
                TempData["EMAIL"] = list.Email;
                TempData["DeliveredDate"] = list.DeliveredDate;
                return View(orderdetail);
            }
            else
            {
                var orderdetail2 = new OrderService().GetDetailNotPr(id);
                var list = orderdetail2.SingleOrDefault(o => o.OrderId == id);
                TempData["ORDERID"] = id;
                TempData["ORDER_DATE"] = list.OrderDate;
                TempData["NAME"] = list.CustomerName;
                TempData["PHONE"] = list.Phone;
                TempData["ADDRESS"] = list.Address;
                TempData["EMAIL"] = list.Email;
                TempData["DeliveredDate"] = list.DeliveredDate;
                orderdetail2.RemoveAt(0);
                return View(orderdetail2);
            }
            
        }

        public void ShowOption()
        {
            var options = SharedData.Option(PageListModel.page, PageListModel.pageSize, PageListModel.keyword);
            TempData["showpagesize"] = options;

        }
        [HttpGet]
        public ActionResult CreateDetail(int id, int? cateid)
        {
            TempData["ORDERID"] = id;
            SharedData.OrderId = id;
            ListProductId(null,cateid);
            ListCateId(cateid);
            return View();
        }
        public void ListCateId(int? selectId = null)
        {
            var service = new CateService();
            var cates = service.GetAll();
        
            ViewBag.CateList = new SelectList(cates, "Id", "CategoryName", selectId);
        }
        public void ListProductId(int? selectId = null, int? cateid=null)
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
        public ActionResult CreateDetail(tbl_OrderDetail detail, OrderDetailModel model)
        {
            ListCateId();
            ListProductId();
            TempData["ORDERID"] = SharedData.OrderId;
            var service = new OrderService();
            detail.OrderID = SharedData.OrderId;
            var findProductId = service.FindProductId((int)detail.ProductID,(int)detail.OrderID);

            if(model.ProductId == findProductId && model.Quantity != null)
            {
                TempData["CREATE"] = "Sản phẩm đã tồn tại trong đơn hàng";
                TempData["ALEART"] = "warning";
            }

            if (model.Quantity != null && model.Quantity>0 && model.ProductId != findProductId)
            {
                
                var rs = service.CreateDetail(detail);
                if (rs != null)
                {
                    TempData["CREATE"] = "Thêm sản phẩm đơn hàng thành công";
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
        
        [HttpGet]    
        public ActionResult EditDetail(int id, int? cateid, int detailid)
        {
            TempData["ORDERID"] = id;
            SharedData.OrderId = id;
            SharedData.DetailId = detailid;
            TempData["DETAIL_ID"] = detailid;
            ListProductId(null, cateid);
            ListCateId(cateid);
            var detail = new OrderService().GetDetailById(detailid);
            return View(detail);   
        }

        [HttpPost]
        public ActionResult EditDetail(tbl_OrderDetail detail, OrderDetailModel model)
        {
            ListCateId();
            ListProductId();
            TempData["ORDERID"] = SharedData.OrderId;
            TempData["DETAIL_ID"] = SharedData.DetailId;
            var service = new OrderService();

            if (model.Quantity != null && model.Quantity > 0 )
            {
                detail.Id = (int)SharedData.DetailId;
                detail.OrderID = SharedData.OrderId;
                var rs = service.UpdateDetail(detail);
                if (rs )
                {
                    TempData["CREATE"] = "Cập nhập sản phẩm đơn hàng thành công";
                    TempData["ALEART"] = "success";
                }
                else
                {
                    TempData["CREATE"] = "Cập nhập thất bại";
                    TempData["ALEART"] = "danger";
                }
            }

            return View();
        }

        
        public ActionResult DeleteDetail(int id)
        {

            var service = new OrderService();
            var rs = service.DeleteDetail(id);
            if (rs)
            {
                return RedirectPermanent("/admin/order/detail/"+SharedData.OrderId.ToString());
            }
            else
            {
                return View();
            }
        }

        public ActionResult Edit(int id)
        {
            var order = new OrderService().GetOrderById(id);
            return View(order);
        }

        [HttpPost]
        public ActionResult Edit(tbl_Order order, OrderModel model)
        {
            var service = new OrderService();

            if (model.CustomerName != null && !model.CustomerName.StartsWith(" ") && model.Phone != null && 
                model.Phone.Length>= 10 && model.Phone.Length<=15 && model.Address != null && model.Status != null && 
                model.Delivered != null)
            {
                var rs = service.Update(order);
                if (rs)
                {

                    TempData["CREATE"] = "Cập nhập thông tin đơn hàng thành công";
                    TempData["ALEART"] = "success";
                }
                else
                {
                    TempData["CREATE"] = "Cập nhập thất bại";
                    TempData["ALEART"] = "danger";
                }
            }

            return View();
        }

        
        public ActionResult Delete(int id)
        {
            var service = new OrderService();
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

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(tbl_Order order , OrderModel model)
        {
            var service = new OrderService();

            if (model.CustomerName != null && !model.CustomerName.StartsWith(" ") && model.Phone != null &&
               model.Phone.Length >= 10 && model.Phone.Length <= 15 && model.Address != null && model.Status != null &&
               model.Delivered != null)
            {
                var rs = service.Create(order);
                if (rs!=null)
                {

                    TempData["CREATE"] = "Thêm mới đơn hàng thành công";
                    TempData["ALEART"] = "success";
                }
                else
                {
                    TempData["CREATE"] = "Thêm mới thất bại";
                    TempData["ALEART"] = "danger";
                }
            }


            return View();
        }
    }
}