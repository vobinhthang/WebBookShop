using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebBookShop.Commons;
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
        public IEnumerable<OrderModel> GetAll(int page, int pageSize)
        {
            var qr = from o in dbcontext.tbl_Order   
                     
                     orderby o.OrderDate descending
                     select new OrderModel
                     {
                         Id = o.Id,
                         OrderDate = o.OrderDate,
                         Status = o.Status,
                         DeliveredDate = o.DeliveredDate,
                         Delivered = o.Delivered,
                         CustomerName = o.CustomerName,
                         UserID = o.UserID,
                         Payments = o.Payments,
                         Discount = o.Discount,
                         TotalPrice = o.TotalPrice,
                         Address = o.Address,
                         Phone = o.Phone,
                         Email = o.Email,

                     };
            foreach(var o in qr.ToList())
            {
                var orderdetail = new OrderService().GetDetail((int)o.Id);
                var order = dbcontext.tbl_Order.Find(o.Id);
                if (order.Discount != null)
                {
                    order.TotalPrice = orderdetail.Sum(od => od.Price * od.Quantity) * (100 - order.Discount) / 100;
                }
                else
                {
                    order.TotalPrice = orderdetail.Sum(od => od.Price * od.Quantity);
                }
            }
            dbcontext.SaveChanges();

            return qr.ToPagedList(page,pageSize);
        }

       

        public IEnumerable<OrderModel> Search(string keyword, int page, int pageSize)
        {
            if (keyword!=null)
            {
                var qr = from o in dbcontext.tbl_Order
                         orderby o.OrderDate descending
                         where o.OrderDate.Value.ToString().Contains(keyword) ||  
                                o.Id.ToString().Contains(keyword) ||
                                o.Phone.Contains(keyword) 
                         select new OrderModel
                         {
                             Id = o.Id,
                             OrderDate = o.OrderDate,
                             Status = o.Status,
                             DeliveredDate = o.DeliveredDate,
                             Delivered = o.Delivered,
                             CustomerName = o.CustomerName,
                             UserID = o.UserID,
                             Payments = o.Payments,
                             Discount = o.Discount,
                             TotalPrice = o.TotalPrice,
                             Address = o.Address,
                             Phone = o.Phone,
                             Email = o.Email,

                         };
                return qr.ToPagedList(page, pageSize);
            }
            else
            {
                var qr = from o in dbcontext.tbl_Order

                         orderby o.OrderDate descending
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
                foreach (var o in qr.ToList())
                {
                    var orderdetail = new OrderService().GetDetail((int)o.Id);
                    var order = dbcontext.tbl_Order.Find(o.Id);
                    if (order.Discount != null)
                    {
                        order.TotalPrice = orderdetail.Sum(od => od.Price * od.Quantity) * (100 - order.Discount) / 100;
                    }
                    else
                    {
                        order.TotalPrice = orderdetail.Sum(od => od.Price * od.Quantity);
                    }
                }
                dbcontext.SaveChanges();

                return qr.ToPagedList(page, pageSize);
            }

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
                         DeliveredDate=o.DeliveredDate,
                         Delivered=o.Delivered,
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
                         DeliveredDate=o.DeliveredDate
                     };
            return qr.ToList();
        }

        public List<OrderDetailModel> GetTopSelling()
        {

            var qr = from od in dbcontext.tbl_OrderDetail
                     join o in dbcontext.tbl_Order on od.OrderID equals o.Id
                     join p in dbcontext.tbl_Product on od.ProductID equals p.Id
                     group od by od.ProductID into x
                     orderby x.Sum(p=>p.Quantity) descending
                    
                     select new OrderDetailModel
                     {
                         ProductId= x.Key,         
                         
                     };
            var topselling = qr.Take(10).ToList();
            return topselling;
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
            var rs = dbcontext.tbl_OrderDetail.SingleOrDefault(od => od.ProductID == productId && od.OrderID== orderid);
            
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

        public bool Update(tbl_Order order)
        {
            try
            {
                var _order = dbcontext.tbl_Order.Find(order.Id);
                _order.CustomerName = order.CustomerName;
                _order.Phone = order.Phone;
                _order.Address = order.Address;
                _order.Email = order.Email;
                _order.Discount = order.Discount;
                _order.Status = order.Status;
                _order.Delivered = order.Delivered;
                if (_order.Delivered == true && _order.DeliveredDate==null)
                {
                    _order.DeliveredDate = DateTime.Now;
                }
                dbcontext.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public OrderModel GetOrderById(int id)
        {
            var order = dbcontext.tbl_Order.Find(id);
            return new OrderModel
            {
                Id= order.Id,
                CustomerName= order.CustomerName,
                Phone= order.Phone,
                Email= order.Email,
                Address= order.Address,
                Discount= order.Discount,
                Status= order.Status,
                Delivered= order.Delivered,
            };
        }

        public IEnumerable<OrderModel> MyOrder(int userId, int page, int pageSize)
        {
            var qr = from o in dbcontext.tbl_Order

                     orderby o.OrderDate descending
                     where o.UserID == userId
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
            foreach (var o in qr.ToList())
            {
                var orderdetail = new OrderService().GetDetail((int)o.Id);
                var order = dbcontext.tbl_Order.Find(o.Id);
                if (order.Discount != null)
                {
                    order.TotalPrice = orderdetail.Sum(od => od.Price * od.Quantity) * (100 - order.Discount) / 100;
                }
                else
                {
                    order.TotalPrice = orderdetail.Sum(od => od.Price * od.Quantity);
                }
            }
            dbcontext.SaveChanges();

            return qr.ToPagedList(page,pageSize);
        }
        public bool Delete(int id)
        {
            try
            {
                var order = dbcontext.tbl_Order.Find(id);
                dbcontext.tbl_Order.Remove(order);
                dbcontext.SaveChanges();
                
                return true;
            }
            catch
            {
                return false;
            }
        }

        public OrderModel Create(tbl_Order order)
        {
            order.OrderDate= DateTime.Now;
            dbcontext.tbl_Order.Add(order);
            dbcontext.SaveChanges();

            return new OrderModel
            {
                CustomerName = order.CustomerName,
                Phone = order.Phone,
                Email = order.Email,
                Address = order.Address,
                Discount = order.Discount,
                Status = order.Status,
                Delivered = order.Delivered,
            };
        }

        public int CountOrderMonth()
        {
            DateTime startOfMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            DateTime endOfMonth = startOfMonth.AddMonths(1);

            var qr = from o in dbcontext.tbl_Order
                     where o.OrderDate >= startOfMonth && o.OrderDate < endOfMonth && o.Delivered==true
                     select o;

            return qr.ToList().Count();
        }

        public double TotalPriceMonth()
        {
            try
            {
                DateTime startOfMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                DateTime endOfMonth = startOfMonth.AddMonths(1);

                var qr = from o in dbcontext.tbl_Order
                         where o.OrderDate >= startOfMonth && o.OrderDate < endOfMonth && o.Delivered == true
                         select o;

                return (double)qr.Sum(o => o.TotalPrice);
            }
            catch
            {
                return 0;
            }
        }

        public int ConfirmPayment(List<CartItem> cartItem, CustomerAddress customerAddress,int? userID, bool? login,int payments)
        {
            tbl_Order order = new tbl_Order();
            if (login == false)
            {
                order.OrderDate = DateTime.Now;
                order.Address = customerAddress._address;
                order.Phone = customerAddress._phone;
                order.CustomerName = customerAddress._name;
                order.Email = customerAddress._email;
                order.Discount = 0;
                order.Delivered = false;
                order.Status = true;
                if (payments == 1)
                {
                    order.Payments = "COD";
                }
                else
                {
                    order.Payments ="Chuyển khoản";
                }
            }
            else
            {
                order.OrderDate = DateTime.Now;
                order.UserID = userID;
                order.Address = customerAddress._address;
                order.Phone = customerAddress._phone;
                order.CustomerName = customerAddress._name;
                order.Email = customerAddress._email;
                order.Discount = 0;
                order.Delivered = false;
                order.Status = true;
                if (payments == 1)
                {
                    order.Payments = "COD";
                }
                else
                {
                    order.Payments = "Chuyển khoản";
                }
            }
            dbcontext.tbl_Order.Add(order);
            dbcontext.SaveChanges();
            
            
            

            foreach (var item in cartItem)
            {
                tbl_OrderDetail orderDetail = new tbl_OrderDetail();
                orderDetail.OrderID = order.Id;
                orderDetail.Price = item.ProductModel.Price;
                orderDetail.ProductID = item.ProductModel.Id;
                orderDetail.Quantity = item.Quantity;

                dbcontext.tbl_OrderDetail.Add(orderDetail);
                var product = dbcontext.tbl_Product.Find(orderDetail.ProductID);
                product.Quantity -= orderDetail.Quantity;
            }
            
            dbcontext.SaveChanges();

            return order.Id;
        }
        
        public double UpdateTotalPrice(int orderId)
        {
            var order = dbcontext.tbl_Order.Find(orderId);
            var detail = new OrderService().GetDetail(orderId);

            if (order.Discount != 0)
            {
                order.TotalPrice = detail.Sum(od => od.Price * od.Quantity) * (100 - order.Discount) / 100;
            }
            else
            {
                order.TotalPrice = detail.Sum(od => od.Price * od.Quantity);
            }
            dbcontext.SaveChanges();

            return (double)order.TotalPrice;
        }
    
        //public List<OrderDetailModel> SoldProductsChart()
        //{


        //    //var qr = from od in dbcontext.tbl_OrderDetail
        //    //         join p in dbcontext.tbl_Product on od.ProductID equals p.Id
        //    //         group od by od.ProductID into x
        //    //         orderby x.Sum(p => p.Quantity) descending
                     
        //    //         select new OrderDetailModel
        //    //         {
        //    //             ProductName = x.SingleOrDefault(x=>x.ProductID=x.ProductID),
        //    //             Quantity =  x.(p => p.Quantity),
        //    //             Delivered=true
        //    //         };
           
        //    //return qr.ToList();
        //}
    }
}