using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebBookShop.EF;
using WebBookShop.Models;

namespace WebBookShop.Services
{
    public class OrderService
    {
        private readonly MyDbContext dbcontext;
        public OrderService()
        {
            dbcontext = new MyDbContext();
        }
        public List<OrderModel> GetAll()
        {
            var qr = from o in dbcontext.tbl_Order   
                     
                     orderby o.Id
                     select new OrderModel
                     {
                         Id = o.Id,
                         OrderDate = o.OrderDate,
                         Status = o.Status,
                         DeliveredDate = o.DeliveredDate,
                         Delivered = o.Delivered,
                         CustomerName = o.CustomerName,
                         UserID = o.UserID,

                         Discount = o.Discount,
                         TotalPrice = o.TotalPrice,
                         Address = o.Address,
                         Phone = o.Phone,
                         Email = o.Email,

                     };
            return qr.ToList();
        }

        public List<OrderDetailModel> GetDetail(int orderId)
        {
            var qr = from od in dbcontext.tbl_OrderDetail
                     join o in dbcontext.tbl_Order on od.OrderID equals o.Id
                     join p in dbcontext.tbl_Product on od.ProductID equals p.Id
                     where od.OrderID == orderId
                     select new OrderDetailModel
                     {
                         Id=od.Id,
                         ProductId=od.ProductID,
                         OrderId=od.OrderID,
                         Price=od.Price,
                         Quantity=od.Quantity,
                         ProductName=p.ProductName,
                         Phone = o.Phone,
                         Address = o.Address,
                         Email=o.Email,
                         CustomerName=o.CustomerName,
                         TotalPrice=o.TotalPrice,
                         OrderDate=o.OrderDate,
                         
                     };
            return qr.ToList();
        }
        public List<OrderDetailModel> GetDetailNotPr(int orderId)
        {
            var qr = from o in dbcontext.tbl_Order
                     where o.Id == orderId
                     select new OrderDetailModel
                     {
                         Id= o.Id,
                         OrderId = orderId,
                         Phone = o.Phone,
                         Address = o.Address,
                         Email = o.Email,
                         CustomerName = o.CustomerName,
                         TotalPrice = o.TotalPrice,
                         OrderDate = o.OrderDate,
                     };
            return qr.ToList();
        }

        public OrderDetailModel CreateDetail(tbl_OrderDetail detail)
        {
            var findPrice = dbcontext.tbl_Product.Find(detail.ProductID);
            detail.Price = findPrice.Price;
            dbcontext.tbl_OrderDetail.Add(detail);
            dbcontext.SaveChanges();

            var orderdetail = new OrderService().GetDetail((int)detail.OrderID);
            var order = dbcontext.tbl_Order.Find(detail.OrderID);
            if (order.Discount != null)
            {
                order.TotalPrice = orderdetail.Sum(od => od.Price * od.Quantity) * (100 - order.Discount) / 100;
            }
            else
            {
                order.TotalPrice = orderdetail.Sum(od => od.Price * od.Quantity);
            }
            dbcontext.SaveChanges();
            return new OrderDetailModel
            {
                Id = detail.Id,
                ProductId=detail.ProductID,
                OrderId = detail.OrderID,
                Quantity=detail.Quantity,
                Price=detail.Price,
            };

        }

        public int FindProductId(int productId, int orderid)
        {
            var rs = dbcontext.tbl_OrderDetail.Take(1).SingleOrDefault(od => od.ProductID == productId && od.OrderID== orderid);
            if (rs != null)
                return productId;
            else
                return 0;
        }
        
        public bool UpdateQuantity(tbl_OrderDetail detail)
        {
            try
            {
                var _detail = dbcontext.tbl_OrderDetail.SingleOrDefault(od => od.ProductID == detail.ProductID);
                _detail.Quantity += detail.Quantity;
                dbcontext.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool UpdateDetail(tbl_OrderDetail detail)
        {
            try
            {
                //var product = dbcontext.tbl_Product.Find(detail.ProductID);

                var _detail = dbcontext.tbl_OrderDetail.Find(detail.Id);
                _detail.Id = detail.Id;
                _detail.ProductID = detail.ProductID;
                _detail.Quantity = detail.Quantity;
                dbcontext.SaveChanges();

                var orderdetail = new OrderService().GetDetail((int)detail.OrderID);
                var order = dbcontext.tbl_Order.Find(detail.OrderID);
                if (order.Discount != null)
                {
                    order.TotalPrice = orderdetail.Sum(od => od.Price * od.Quantity) * (100 - order.Discount) / 100;
                }
                else
                {
                    order.TotalPrice = orderdetail.Sum(od => od.Price * od.Quantity);
                }
                dbcontext.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public OrderDetailModel GetDetailById(int id)
        {
            var detail = dbcontext.tbl_OrderDetail.Find(id);
            return new OrderDetailModel
            {
                Id = detail.Id,
                ProductId = detail.ProductID,
                OrderId = detail.OrderID,
                Quantity= detail.Quantity,
                Price =detail.Price,
            };
        }

        public bool DeleteDetail(int id)
        {
            try
            {
                var detail = dbcontext.tbl_OrderDetail.Find(id);
                dbcontext.tbl_OrderDetail.Remove(detail);
                dbcontext.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}