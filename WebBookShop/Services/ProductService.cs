using PagedList;
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
        public IEnumerable<ProductModel> GetAll(int page, int pageSize)
        {

            var qr = from p in dbcontext.tbl_Product
                     join c in dbcontext.tbl_Category on p.CategoryID equals c.Id
                     orderby p.CreateDate descending
                     select new ProductModel
                     {

                         Id = p.Id,
                         ProductName = p.ProductName,
                         Status = p.Status,
                         Hot = p.Hot,
                         CategoryId = p.CategoryID,
                         CategoryName = c.CategoryName,
                         Price = p.Price,
                         Quantity = p.Quantity,
                         Description = p.Description,
                         AuthorName = p.AuthorName,
                         PublishCompany = p.PublishCompany,
                         NumberPage = p.NumberPage,
                         CreatedDate = p.CreateDate,
                         UpdateDate = p.UpdateDate,
                     };
            return qr.ToPagedList(page, pageSize);
        }
        public List<ProductModel> GetAll()
        {
            var qr = from p in dbcontext.tbl_Product
                     join c in dbcontext.tbl_Category on p.CategoryID equals c.Id
                     orderby p.ProductName 
                     select new ProductModel
                     {
                         
                         Id=p.Id,
                         ProductName=p.ProductName,
                         Status=p.Status,
                         Hot=p.Hot,
                         CategoryId=p.CategoryID,
                         CategoryName=c.CategoryName,
                         Price=p.Price,
                         Quantity=p.Quantity,
                         Description=p.Description,
                         AuthorName=p.AuthorName,
                         PublishCompany = p.PublishCompany,
                         NumberPage=p.NumberPage,
                         CreatedDate=p.CreateDate,
                         UpdateDate=p.UpdateDate,
                     };
            

            return qr.ToList();
        }
        public List<ProductModel> GetNews()
        {
            var qr = from p in dbcontext.tbl_Product
                     join c in dbcontext.tbl_Category on p.CategoryID equals c.Id
                     orderby p.Id descending
                     where p.Status == true
                     select new ProductModel
                     {

                         Id = p.Id,
                         ProductName = p.ProductName,
                         Status = p.Status,
                         Hot = p.Hot,
                         CategoryId = p.CategoryID,
                         CategoryName = c.CategoryName,
                         Price = p.Price,
                         Quantity = p.Quantity,
                         Description = p.Description,
                         AuthorName = p.AuthorName,
                         PublishCompany = p.PublishCompany,
                         NumberPage = p.NumberPage,
                         CreatedDate = p.CreateDate,
                         UpdateDate = p.UpdateDate,
                     };


            return qr.Take(10).ToList();
        }
        public IEnumerable<ProductModel> GetByName(string keyword,string sort, int page, int pageSize)
        {
            if (sort == "default")
            {
                var qr = from p in dbcontext.tbl_Product
                         join c in dbcontext.tbl_Category on p.CategoryID equals c.Id
                         where p.ProductName.Contains(keyword)
                         orderby p.Id descending
                         select new ProductModel
                         {

                             Id = p.Id,
                             ProductName = p.ProductName,
                             Status = p.Status,
                             Hot = p.Hot,
                             CategoryId = p.CategoryID,
                             CategoryName = c.CategoryName,
                             Price = p.Price,
                             Quantity = p.Quantity,
                             Description = p.Description,
                             AuthorName = p.AuthorName,
                             PublishCompany = p.PublishCompany,
                             NumberPage = p.NumberPage,
                             CreatedDate = p.CreateDate,
                             UpdateDate = p.UpdateDate,
                         };


                return qr.ToPagedList(page, pageSize);
            }
            else if (sort == "asc")
            {

                var qr = from p in dbcontext.tbl_Product
                         join c in dbcontext.tbl_Category on p.CategoryID equals c.Id
                         where p.ProductName.Contains(keyword)
                         orderby p.Price
                         select new ProductModel
                         {

                             Id = p.Id,
                             ProductName = p.ProductName,
                             Status = p.Status,
                             Hot = p.Hot,
                             CategoryId = p.CategoryID,
                             CategoryName = c.CategoryName,
                             Price = p.Price,
                             Quantity = p.Quantity,
                             Description = p.Description,
                             AuthorName = p.AuthorName,
                             PublishCompany = p.PublishCompany,
                             NumberPage = p.NumberPage,
                             CreatedDate = p.CreateDate,
                             UpdateDate = p.UpdateDate,
                         };


                return qr.ToPagedList(page, pageSize);
            }
            else if (sort == "des")
            {

                var qr = from p in dbcontext.tbl_Product
                         join c in dbcontext.tbl_Category on p.CategoryID equals c.Id
                         where p.ProductName.Contains(keyword)
                         orderby p.Price descending
                         select new ProductModel
                         {

                             Id = p.Id,
                             ProductName = p.ProductName,
                             Status = p.Status,
                             Hot = p.Hot,
                             CategoryId = p.CategoryID,
                             CategoryName = c.CategoryName,
                             Price = p.Price,
                             Quantity = p.Quantity,
                             Description = p.Description,
                             AuthorName = p.AuthorName,
                             PublishCompany = p.PublishCompany,
                             NumberPage = p.NumberPage,
                             CreatedDate = p.CreateDate,
                             UpdateDate = p.UpdateDate,
                         };


                return qr.ToPagedList(page, pageSize);
            }
            else
            {

                var qr = from p in dbcontext.tbl_Product
                         join c in dbcontext.tbl_Category on p.CategoryID equals c.Id
                         where p.ProductName.Contains(keyword)
                         orderby p.Id descending
                         select new ProductModel
                         {

                             Id = p.Id,
                             ProductName = p.ProductName,
                             Status = p.Status,
                             Hot = p.Hot,
                             CategoryId = p.CategoryID,
                             CategoryName = c.CategoryName,
                             Price = p.Price,
                             Quantity = p.Quantity,
                             Description = p.Description,
                             AuthorName = p.AuthorName,
                             PublishCompany = p.PublishCompany,
                             NumberPage = p.NumberPage,
                             CreatedDate = p.CreateDate,
                             UpdateDate = p.UpdateDate,
                         };


                return qr.ToPagedList(page, pageSize);
            }

        }
        public IEnumerable<ProductModel> GetByCate(int cateId,string sort, int page,int pageSize)
        {
            var cate = dbcontext.tbl_Category.Find(cateId);

            if (cate.ParentID != null)
            {
                if (sort == "default")
                {
                    var qr = from p in dbcontext.tbl_Product
                             join c in dbcontext.tbl_Category on p.CategoryID equals c.Id
                             where p.CategoryID == cateId
                             orderby p.Id descending
                             select new ProductModel
                             {

                                 Id = p.Id,
                                 ProductName = p.ProductName,
                                 Status = p.Status,
                                 Hot = p.Hot,
                                 CategoryId = p.CategoryID,
                                 Price = p.Price,
                             };
                    return qr.ToPagedList(page,pageSize);
                }
                else if (sort == "asc")
                {
                    var qr = from p in dbcontext.tbl_Product
                             join c in dbcontext.tbl_Category on p.CategoryID equals c.Id
                             where p.CategoryID == cateId
                             orderby p.Price
                             select new ProductModel
                             {

                                 Id = p.Id,
                                 ProductName = p.ProductName,
                                 Status = p.Status,
                                 Hot = p.Hot,
                                 CategoryId = p.CategoryID,
                                 Price = p.Price,
                             };
                    return qr.ToPagedList(page, pageSize);
                }
                else if (sort == "des")
                {
                    var qr = from p in dbcontext.tbl_Product
                             join c in dbcontext.tbl_Category on p.CategoryID equals c.Id
                             where p.CategoryID == cateId
                             orderby p.Price descending
                             select new ProductModel
                             {

                                 Id = p.Id,
                                 ProductName = p.ProductName,
                                 Status = p.Status,
                                 Hot = p.Hot,
                                 CategoryId = p.CategoryID,
                                 Price = p.Price,
                             };
                    return qr.ToPagedList(page, pageSize);
                }
                else
                {
                    var qr = from p in dbcontext.tbl_Product
                             join c in dbcontext.tbl_Category on p.CategoryID equals c.Id
                             where p.CategoryID == cateId
                             orderby p.Id descending
                             select new ProductModel
                             {

                                 Id = p.Id,
                                 ProductName = p.ProductName,
                                 Status = p.Status,
                                 Hot = p.Hot,
                                 CategoryId = p.CategoryID,
                                 Price = p.Price,
                             };
                    return qr.ToPagedList(page, pageSize);
                }
            }
            else
            {
                if (sort == "default")
                {
                    var qr = from p in dbcontext.tbl_Product
                             join c in dbcontext.tbl_Category on p.CategoryID equals c.Id
                             where c.ParentID == cate.Id
                             orderby p.Id descending
                             select new ProductModel
                             {

                                 Id = p.Id,
                                 ProductName = p.ProductName,
                                 Status = p.Status,
                                 Hot = p.Hot,
                                 CategoryId = p.CategoryID,
                                 Price = p.Price,
                             };

                    return qr.ToPagedList(page, pageSize);
                }
                else if (sort == "asc")
                {
                    var qr = from p in dbcontext.tbl_Product
                             join c in dbcontext.tbl_Category on p.CategoryID equals c.Id
                             where c.ParentID == cate.Id
                             orderby p.Price
                             select new ProductModel
                             {

                                 Id = p.Id,
                                 ProductName = p.ProductName,
                                 Status = p.Status,
                                 Hot = p.Hot,
                                 CategoryId = p.CategoryID,
                                 Price = p.Price,
                             };

                    return qr.ToPagedList(page, pageSize);
                }
                else if (sort == "des")
                {
                    var qr = from p in dbcontext.tbl_Product
                             join c in dbcontext.tbl_Category on p.CategoryID equals c.Id
                             where c.ParentID == cate.Id
                             orderby p.Price descending
                             select new ProductModel
                             {

                                 Id = p.Id,
                                 ProductName = p.ProductName,
                                 Status = p.Status,
                                 Hot = p.Hot,
                                 CategoryId = p.CategoryID,
                                 Price = p.Price,
                             };

                    return qr.ToPagedList(page, pageSize);
                }
                else
                {
                    var qr = from p in dbcontext.tbl_Product
                             join c in dbcontext.tbl_Category on p.CategoryID equals c.Id
                             where c.ParentID == cate.Id
                             orderby p.Id descending
                             select new ProductModel
                             {

                                 Id = p.Id,
                                 ProductName = p.ProductName,
                                 Status = p.Status,
                                 Hot = p.Hot,
                                 CategoryId = p.CategoryID,
                                 Price = p.Price,
                             };

                    return qr.ToPagedList(page, pageSize);
                }
            }
        }
        public List<ProductModel> GetByCate(int cateId)
        {
            var qr = from p in dbcontext.tbl_Product
                     join c in dbcontext.tbl_Category on p.CategoryID equals c.Id
                     where p.CategoryID == cateId
                     orderby p.ProductName
                     select new ProductModel
                     {

                         Id = p.Id,
                         ProductName = p.ProductName,
                         Status = p.Status,
                         Hot = p.Hot,
                         CategoryId = p.CategoryID,
                         CategoryName = c.CategoryName,
                         Price = p.Price,

                     };

            return qr.ToList();
        }
        public string FindProductName(string productname)
        {
            var rs = dbcontext.tbl_Product.SingleOrDefault(p=>p.ProductName==productname);
            if (rs != null)
            {
                return productname;
            }
            else
            {
                return null;
            }
        }

        public ProductModel Create(tbl_Product product)
        {
            product.CreateDate = DateTime.Now;

            dbcontext.tbl_Product.Add(product);
            dbcontext.SaveChanges();

            //tbl_ImageProduct imageProduct = new tbl_ImageProduct
            //{
            //    ProductId = product.Id,
                
            //};
            //dbcontext.tbl_ImageProduct.Add(imageProduct);
            //dbcontext.SaveChanges();
            return new ProductModel
            {
                Id = product.Id,
                ProductName = product.ProductName,
                Status = product.Status,
                Hot = product.Hot,
                CategoryId = product.CategoryID,
                Price = product.Price,
                Quantity = product.Quantity,
                Description = product.Description,
                AuthorName = product.AuthorName,
                PublishCompany = product.PublishCompany,
                NumberPage = product.NumberPage,
                CreatedDate = product.CreateDate,
                UpdateDate = product.UpdateDate,
            };
        }

        public ProductModel GetById(int id)
        {
            try
            {
                var qr = from p in dbcontext.tbl_Product
                         join c in dbcontext.tbl_Category on p.CategoryID equals c.Id
                         join i in dbcontext.tbl_ImageProduct on p.Id equals i.ProductId
                         where p.Id == id
                         select new ProductModel
                         {
                             Id = p.Id,
                             ProductName = p.ProductName,
                             Status = p.Status,
                             Image = i.Image,
                             Hot = p.Hot,
                             CategoryId = p.CategoryID,
                             CategoryName = c.CategoryName,
                             Price = p.Price,
                             Quantity = p.Quantity,
                             Description = p.Description,
                             AuthorName = p.AuthorName,
                             PublishCompany = p.PublishCompany,
                             NumberPage = p.NumberPage,
                             CreatedDate = p.CreateDate,
                             UpdateDate = p.UpdateDate,
                         };
                ProductModel product = qr.First();
                return product;
            }
            catch
            {
                var qr = from p in dbcontext.tbl_Product
                         join c in dbcontext.tbl_Category on p.CategoryID equals c.Id
                         where p.Id == id
                         select new ProductModel
                         {
                             Id = p.Id,
                             ProductName = p.ProductName,
                             Status = p.Status,
                             Hot = p.Hot,
                             CategoryId = p.CategoryID,
                             CategoryName = c.CategoryName,
                             Price = p.Price,
                             Quantity = p.Quantity,
                             Description = p.Description,
                             AuthorName = p.AuthorName,
                             PublishCompany = p.PublishCompany,
                             NumberPage = p.NumberPage,
                             CreatedDate = p.CreateDate,
                             UpdateDate = p.UpdateDate,
                         };
                ProductModel product = qr.First();
                return product;
            }
        }
        
        public bool ChangeThumbnail(int id , int productId)
        {
            var img = dbcontext.tbl_ImageProduct.SingleOrDefault(i => i.Id == id);
            
            img.Thumbnail = !img.Thumbnail;
            dbcontext.SaveChanges();
            var img_false = dbcontext.tbl_ImageProduct.Where(i => i.Id != id&& i.ProductId==productId);
            if (img_false != null)
            {
                foreach (var i in img_false)
                {
                    i.Thumbnail = !img.Thumbnail;
                    
                }
            }
            dbcontext.SaveChanges();
            return (bool)img.Thumbnail;
        }

        public bool Update(tbl_Product product)
        {
            
            try
            {
                var _product = dbcontext.tbl_Product.Find(product.Id);
                product.UpdateDate = DateTime.Now;

                _product.ProductName = product.ProductName.Trim();
                _product.CategoryID = product.CategoryID;
                _product.Price = product.Price;

                if (_product.NumberPage != null)
                {
                    _product.NumberPage = product.NumberPage;
                }
                else
                {
                    _product.NumberPage = 0;
                }
                if (product.AuthorName != null)
                {
                    _product.AuthorName = product.AuthorName;
                }
                else
                {
                    _product.AuthorName = string.Empty;
                }
                if (product.PublishCompany != null)
                {
                    _product.PublishCompany = product.PublishCompany;
                }
                else
                {
                    _product.PublishCompany = string.Empty;
                }
                if (product.Description != null)
                {
                    _product.Description = product.Description;
                }
                else
                {
                    _product.Description = string.Empty;
                }
                _product.Quantity = product.Quantity;
                _product.UpdateDate = product.UpdateDate;
                dbcontext.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Delete(int id)
        {
            try
            {
                var product = dbcontext.tbl_Product.Find(id);
                dbcontext.tbl_Product.Remove(product);
                dbcontext.SaveChanges();
                var imageproduct = dbcontext.tbl_ImageProduct.Where(i=>i.ProductId==id).ToList();
                foreach (var i in imageproduct)
                {
                    var model = dbcontext.tbl_ImageProduct.Single(img => img.ProductId == i.ProductId);
                    dbcontext.tbl_ImageProduct.Remove(model);
                   
                }
                dbcontext.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool ChangeStatusHot(int id, string str)
        {
            if (str == "status")
            {
                var product = dbcontext.tbl_Product.Find(id);
                product.Status = !product.Status;
                dbcontext.SaveChanges();

                return (bool)product.Status;
            }
            else if(str == "hot")
            {
                var product = dbcontext.tbl_Product.Find(id);
                product.Hot = !product.Hot;
                dbcontext.SaveChanges();

                return (bool)product.Hot;
            }
            else
            {
                return false;
            }
        }

        public IEnumerable<ProductModel> Search(string keyword, int page, int pageSize)
        {
            if (!string.IsNullOrEmpty(keyword))
            {
                var qr = from p in dbcontext.tbl_Product
                         join c in dbcontext.tbl_Category on p.CategoryID equals c.Id
                         orderby p.CreateDate
                         where p.ProductName.Contains(keyword) || c.CategoryName.Contains(keyword)
                         select new ProductModel
                         {

                             Id = p.Id,
                             ProductName = p.ProductName,
                             Status = p.Status,
                             Hot = p.Hot,
                             CategoryId = p.CategoryID,
                             CategoryName = c.CategoryName,
                             Price = p.Price,
                             Quantity = p.Quantity,
                             Description = p.Description,
                             AuthorName = p.AuthorName,
                             PublishCompany = p.PublishCompany,
                             NumberPage = p.NumberPage,
                             CreatedDate = p.CreateDate,
                             UpdateDate = p.UpdateDate,
                         };
                return qr.ToPagedList(page, pageSize);
            }
            else
            {
                var qr = from p in dbcontext.tbl_Product
                         join c in dbcontext.tbl_Category on p.CategoryID equals c.Id
                         orderby p.CreateDate
                         select new ProductModel
                         {

                             Id = p.Id,
                             ProductName = p.ProductName,
                             Status = p.Status,
                             Hot = p.Hot,
                             CategoryId = p.CategoryID,
                             CategoryName = c.CategoryName,
                             Price = p.Price,
                             Quantity = p.Quantity,
                             Description = p.Description,
                             AuthorName = p.AuthorName,
                             PublishCompany = p.PublishCompany,
                             NumberPage = p.NumberPage,
                             CreatedDate = p.CreateDate,
                             UpdateDate = p.UpdateDate,
                         };
                return qr.ToPagedList(page, pageSize);
            }

        }

        public IEnumerable<ProductModel> Sorting(string sortproductname,string prrice, string sortupdate, string sortcreate, int page, int pageSize)
        {
            if (sortproductname == "ASC" && prrice=="" && sortupdate == "" && sortcreate == "")
            {
                var qr = from p in dbcontext.tbl_Product
                         join c in dbcontext.tbl_Category on p.CategoryID equals c.Id
                         orderby p.ProductName
                         select new ProductModel
                         {

                             Id = p.Id,
                             ProductName = p.ProductName,
                             Status = p.Status,
                             Hot = p.Hot,
                             CategoryId = p.CategoryID,
                             CategoryName = c.CategoryName,
                             Price = p.Price,
                             Quantity = p.Quantity,
                             Description = p.Description,
                             AuthorName = p.AuthorName,
                             PublishCompany = p.PublishCompany,
                             NumberPage = p.NumberPage,
                             CreatedDate = p.CreateDate,
                             UpdateDate = p.UpdateDate,
                         };

                return qr.ToPagedList(page, pageSize);
            }
            else if (sortproductname == "DES" && prrice == "" && sortupdate == "" && sortcreate == "")
            {
                var qr = from p in dbcontext.tbl_Product
                         join c in dbcontext.tbl_Category on p.CategoryID equals c.Id
                         orderby p.ProductName descending
                         select new ProductModel
                         {

                             Id = p.Id,
                             ProductName = p.ProductName,
                             Status = p.Status,
                             Hot = p.Hot,
                             CategoryId = p.CategoryID,
                             CategoryName = c.CategoryName,
                             Price = p.Price,
                             Quantity = p.Quantity,
                             Description = p.Description,
                             AuthorName = p.AuthorName,
                             PublishCompany = p.PublishCompany,
                             NumberPage = p.NumberPage,
                             CreatedDate = p.CreateDate,
                             UpdateDate = p.UpdateDate,
                         };
                return qr.ToPagedList(page, pageSize);

            }

            if (sortupdate == "ASC" && prrice == "" && sortproductname == "" && sortcreate == "")
            {
                var qr = from p in dbcontext.tbl_Product
                         join c in dbcontext.tbl_Category on p.CategoryID equals c.Id
                         orderby p.UpdateDate
                         select new ProductModel
                         {

                             Id = p.Id,
                             ProductName = p.ProductName,
                             Status = p.Status,
                             Hot = p.Hot,
                             CategoryId = p.CategoryID,
                             CategoryName = c.CategoryName,
                             Price = p.Price,
                             Quantity = p.Quantity,
                             Description = p.Description,
                             AuthorName = p.AuthorName,
                             PublishCompany = p.PublishCompany,
                             NumberPage = p.NumberPage,
                             CreatedDate = p.CreateDate,
                             UpdateDate = p.UpdateDate,
                         };
                return qr.ToPagedList(page, pageSize);

            }
            else if (sortupdate == "DES" && prrice == "" && sortproductname == "" && sortcreate == "")
            {
                var qr = from p in dbcontext.tbl_Product
                         join c in dbcontext.tbl_Category on p.CategoryID equals c.Id
                         orderby p.UpdateDate descending
                         select new ProductModel
                         {

                             Id = p.Id,
                             ProductName = p.ProductName,
                             Status = p.Status,
                             Hot = p.Hot,
                             CategoryId = p.CategoryID,
                             CategoryName = c.CategoryName,
                             Price = p.Price,
                             Quantity = p.Quantity,
                             Description = p.Description,
                             AuthorName = p.AuthorName,
                             PublishCompany = p.PublishCompany,
                             NumberPage = p.NumberPage,
                             CreatedDate = p.CreateDate,
                             UpdateDate = p.UpdateDate,
                         };
                return qr.ToPagedList(page, pageSize);
            }

            if (sortcreate == "ASC" && prrice == "" && sortproductname == "" && sortupdate == "")
            {
                var qr = from p in dbcontext.tbl_Product
                         join c in dbcontext.tbl_Category on p.CategoryID equals c.Id
                         orderby p.CreateDate
                         select new ProductModel
                         {

                             Id = p.Id,
                             ProductName = p.ProductName,
                             Status = p.Status,
                             Hot = p.Hot,
                             CategoryId = p.CategoryID,
                             CategoryName = c.CategoryName,
                             Price = p.Price,
                             Quantity = p.Quantity,
                             Description = p.Description,
                             AuthorName = p.AuthorName,
                             PublishCompany = p.PublishCompany,
                             NumberPage = p.NumberPage,
                             CreatedDate = p.CreateDate,
                             UpdateDate = p.UpdateDate,
                         };
                return qr.ToPagedList(page, pageSize);
            }
            else if (sortcreate == "DES" && prrice == "" && sortproductname == "" && sortupdate == "")
            {
                var qr = from p in dbcontext.tbl_Product
                         join c in dbcontext.tbl_Category on p.CategoryID equals c.Id
                         orderby p.CreateDate descending 
                         select new ProductModel
                         {

                             Id = p.Id,
                             ProductName = p.ProductName,
                             Status = p.Status,
                             Hot = p.Hot,
                             CategoryId = p.CategoryID,
                             CategoryName = c.CategoryName,
                             Price = p.Price,
                             Quantity = p.Quantity,
                             Description = p.Description,
                             AuthorName = p.AuthorName,
                             PublishCompany = p.PublishCompany,
                             NumberPage = p.NumberPage,
                             CreatedDate = p.CreateDate,
                             UpdateDate = p.UpdateDate,
                         };
                return qr.ToPagedList(page, pageSize);
            }

            if (sortcreate == "" && prrice == "ASC" && sortproductname == "" && sortupdate == "")
            {
                var qr = from p in dbcontext.tbl_Product
                         join c in dbcontext.tbl_Category on p.CategoryID equals c.Id
                         orderby p.Price 
                         select new ProductModel
                         {

                             Id = p.Id,
                             ProductName = p.ProductName,
                             Status = p.Status,
                             Hot = p.Hot,
                             CategoryId = p.CategoryID,
                             CategoryName = c.CategoryName,
                             Price = p.Price,
                             Quantity = p.Quantity,
                             Description = p.Description,
                             AuthorName = p.AuthorName,
                             PublishCompany = p.PublishCompany,
                             NumberPage = p.NumberPage,
                             CreatedDate = p.CreateDate,
                             UpdateDate = p.UpdateDate,
                         };
                return qr.ToPagedList(page, pageSize);
            }
            else if (sortcreate == "" && prrice == "DES" && sortproductname == "" && sortupdate == "")
            {
                var qr = from p in dbcontext.tbl_Product
                         join c in dbcontext.tbl_Category on p.CategoryID equals c.Id
                         orderby p.Price descending
                         select new ProductModel
                         {

                             Id = p.Id,
                             ProductName = p.ProductName,
                             Status = p.Status,
                             Hot = p.Hot,
                             CategoryId = p.CategoryID,
                             CategoryName = c.CategoryName,
                             Price = p.Price,
                             Quantity = p.Quantity,
                             Description = p.Description,
                             AuthorName = p.AuthorName,
                             PublishCompany = p.PublishCompany,
                             NumberPage = p.NumberPage,
                             CreatedDate = p.CreateDate,
                             UpdateDate = p.UpdateDate,
                         };
                return qr.ToPagedList(page, pageSize);
            }
            return null;
        }
        
        public List<ProductModel> SearchByCate(int cateid)
        {
            var qr = from p in dbcontext.tbl_Product
                     join c in dbcontext.tbl_Category on p.CategoryID equals c.Id
                     where p.CategoryID == cateid
                     orderby p.ProductName
                     select new ProductModel
                     {
                         Id = p.Id,
                         ProductName = p.ProductName,
                         Status = p.Status,
                         Hot = p.Hot,
                         CategoryId = p.CategoryID,
                         CategoryName = c.CategoryName,
                         Price = p.Price,
                         Quantity = p.Quantity,
                         Description = p.Description,
                         AuthorName = p.AuthorName,
                         PublishCompany = p.PublishCompany,
                         NumberPage = p.NumberPage,
                         CreatedDate = p.CreateDate,
                         UpdateDate = p.UpdateDate,
                     };
            return qr.ToList();
        }


        public void UpdateQuantity(int orderid)
        {
            var details = from d in dbcontext.tbl_OrderDetail
                     where d.OrderID == orderid
                     select new OrderDetailModel
                     {
                         Id =d.Id,
                         ProductId =d.ProductID,
                         Quantity=d.Quantity
                     };          

            foreach(var item in details.ToList())
            {
                var orderDetail = dbcontext.tbl_OrderDetail.Find(item.Id);

                var product = dbcontext.tbl_Product.Find(item.ProductId);
                product.Quantity+= orderDetail.Quantity;
            }
            dbcontext.SaveChanges();
        }
    }
}