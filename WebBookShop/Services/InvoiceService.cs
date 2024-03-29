﻿using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebBookShop.EF;
using WebBookShop.Models;

namespace WebBookShop.Services
{
    public class InvoiceService
    {
        private readonly MyDbContext dbcontext;
        public InvoiceService()
        {
            dbcontext = new MyDbContext();
        }
        public IEnumerable<InvoiceModel> GetAll(int page, int pageSize)
        {
            var qr = from i in dbcontext.tbl_Invoice

                     orderby i.CreateDate descending
                     select new InvoiceModel
                     {
                         Id = i.Id,
                        CreateDate = i.CreateDate,
                        Status = i.Status,
                        UpdateDate = i.UpdateDate,
                        
                     };
            
            return qr.ToPagedList(page, pageSize);
        }
        public IEnumerable<InvoiceModel> Search(string keyword, int page, int pageSize)
        {
            if (!string.IsNullOrEmpty(keyword))
            {

                var qr = from i in dbcontext.tbl_Invoice
                         orderby i.CreateDate descending
                         where i.Id.ToString().Equals(keyword)
                         select new InvoiceModel
                         {
                             Id = i.Id,
                             CreateDate = i.CreateDate,
                             Status = i.Status,
                             UpdateDate = i.UpdateDate
                         };

                return qr.ToPagedList(page, pageSize);
            }
            else
            {
                var qr = from i in dbcontext.tbl_Invoice

                         orderby i.CreateDate descending
                         select new InvoiceModel
                         {
                             Id = i.Id,
                             CreateDate = i.CreateDate,
                             Status = i.Status,
                             UpdateDate = i.UpdateDate
                         };

                return qr.ToPagedList(page, pageSize);
            }

        }

        public bool Delete(int id)
        {
            try
            {
                var invoice = dbcontext.tbl_Invoice.Find(id);
                dbcontext.tbl_Invoice.Remove(invoice);
                dbcontext.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool ChangeStatus(int id)
        {
            var invoice = dbcontext.tbl_Invoice.Find(id);
            invoice.Status = !invoice.Status;
            dbcontext.SaveChanges();
            
            return (bool)invoice.Status;
        }

        public List<InvoiceDetailModel> GetDetail(int invoiceId)
        {
            var qr = from d in dbcontext.tbl_InvoiceDetail
                     join i in dbcontext.tbl_Invoice on d.InvoiceId equals i.Id
                     join p in dbcontext.tbl_Product on d.ProductId equals p.Id
                     orderby d.Id
                     where d.InvoiceId ==invoiceId
                     select new InvoiceDetailModel
                     {
                         Id = d.Id,
                         InvoiceId = d.InvoiceId,
                         Price = d.Price,
                         ProductId = d.ProductId,
                         Productname = p.ProductName,
                         Quantity = d.Quantity,
                         CreateDate = i.CreateDate,
                     };


            return qr.ToList();
        }
        public List<InvoiceDetailModel> GetDetailNotPr(int id)
        {
            var qr = from i in dbcontext.tbl_Invoice
                     where i.Id == id
                     select new InvoiceDetailModel
                     {
                         Id = i.Id,
                         CreateDate = i.CreateDate,
                         
                     };
            return qr.ToList();
        }
        public int FindProductId(int productId, int invoiceid)
        {
            var rs = dbcontext.tbl_InvoiceDetail.SingleOrDefault(od => od.ProductId == productId && od.InvoiceId == invoiceid);

            if (rs != null)
                return productId;
            else
                return 0;
        }
        public InvoiceDetailModel CreateDetail(tbl_InvoiceDetail detail)
        {
            //var findPrice = dbcontext.tbl_Product.Find(detail.ProductId);
            //detail.Price = findPrice.Price;
            dbcontext.tbl_InvoiceDetail.Add(detail);
            dbcontext.SaveChanges();
            var _invoice = dbcontext.tbl_Invoice.Find(detail.InvoiceId);
            _invoice.UpdateDate = DateTime.Now;
            dbcontext.SaveChanges();
            return new InvoiceDetailModel
            {
                Id = detail.Id,
                ProductId = detail.ProductId,
                InvoiceId = detail.InvoiceId,
                Quantity = detail.Quantity,
                Price = detail.Price,
            };

        }

        public bool DeleteDetail(int id)
        {
            try
            {
                var detail = dbcontext.tbl_InvoiceDetail.Find(id);
                dbcontext.tbl_InvoiceDetail.Remove(detail);
                dbcontext.SaveChanges();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public InvoiceDetailModel GetDetailById(int id)
        {
            var detail = dbcontext.tbl_InvoiceDetail.Find(id);
            return new InvoiceDetailModel
            {
                Id = detail.Id,
                InvoiceId = detail.InvoiceId,
                ProductId = detail.ProductId,
                Quantity = detail.Quantity,
                Price = detail.Price
            };
        }

        public bool UpdateDetail(tbl_InvoiceDetail detail)
        {
            try
            {
                var _detail = dbcontext.tbl_InvoiceDetail.Find(detail.Id);
                _detail.ProductId = detail.ProductId;
                _detail.Quantity = detail.Quantity;
                _detail.Price = detail.Price;

                dbcontext.SaveChanges();
                var _invoice = dbcontext.tbl_Invoice.Find(detail.InvoiceId);
                _invoice.UpdateDate = DateTime.Now;
                dbcontext.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Create()
        {
            try
            {
                var detail = new tbl_Invoice
                {
                    CreateDate = DateTime.Now,
                    Status = false
                };
                dbcontext.tbl_Invoice.Add(detail);
                dbcontext.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public double TotalPriceMonth()
        {
            try
            {
                DateTime startOfMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                DateTime endOfMonth = startOfMonth.AddMonths(1);
                var qr = from d in dbcontext.tbl_InvoiceDetail
                         join i in dbcontext.tbl_Invoice on d.InvoiceId equals i.Id
                         where i.CreateDate >= startOfMonth && i.CreateDate < endOfMonth && i.Status == true
                         select d;

                return (double)qr.Sum(d => d.Quantity * d.Price);
            }
            catch
            {
                return 0;
            }
        }
    }
}