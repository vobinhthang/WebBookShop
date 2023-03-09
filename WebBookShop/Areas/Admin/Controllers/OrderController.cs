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
        public ActionResult Index()
        {
            var orders = new OrderService().GetAll();
            
            return View(orders);
        }

        public ActionResult Detail(int id)
        {
            
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
                orderdetail2.RemoveAt(0);
                return View(orderdetail2);
            }
            
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

        [HttpDelete]
        public ActionResult DeleteDetail(int id)
        {
            var service = new OrderService();
            var rs = service.DeleteDetail(id);
            if (rs)
            {
                return RedirectToAction("detail");
            }
            else
            {
                return View();
            }
        }
    }
}