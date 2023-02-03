using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebBookShop.EF;
using WebBookShop.Models;

namespace WebBookShop.Services
{
    public class ProductService
    {
        private readonly MyDbContext dbcontext;
        public ProductService()
        {
            dbcontext = new MyDbContext();
        }

        public List<ProductModel> GetAll()
        {
            var qr = from p in dbcontext.tbl_Product
                     join c in dbcontext.tbl_Category on p.CategoryID equals c.Id
                     orderby p.CreateDate
                     select new ProductModel
                     {
                         Id=p.Id,
                         ProductName=p.ProductName,
                         Image=p.Image,
                         Status=p.Status,
                         Hot=p.Hot,
                         CategoryId=p.CategoryID,
                         CategoryName=c.CategoryName,
                         Price=p.Price,
                         ListImage=p.ListImage,
                         Quantity=p.Quantity,
                         Description=p.Description,
                         AuthorName=p.AuthorName,
                         PublishingCompany=p.PublishCompany,
                         NumberPage=p.NumberPage,
                         CreatedDate=p.CreateDate,
                         UpdateDate=p.UpdateDate,
                     };
            return qr.ToList();
        }

        public ProductModel Create(tbl_Product product)
        {
            product.CreateDate = DateTime.Now;
            dbcontext.tbl_Product.Add(product);
            dbcontext.SaveChanges();
            return new ProductModel
            {
                Id = product.Id,
                ProductName = product.ProductName,
                Image = product.Image,
                Status = product.Status,
                Hot = product.Hot,
                CategoryId = product.CategoryID,
                Price = product.Price,
                ListImage = product.ListImage,
                Quantity = product.Quantity,
                Description = product.Description,
                AuthorName = product.AuthorName,
                PublishingCompany = product.PublishCompany,
                NumberPage = product.NumberPage,
                CreatedDate = product.CreateDate,
                UpdateDate = product.UpdateDate,
            };
        }
    }
}