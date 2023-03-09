using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebBookShop.EF;
using WebBookShop.Models;

namespace WebBookShop.Services
{
    public class ImageProductService
    {
        private readonly MyDbContext dbcontext;

        public ImageProductService()
        {
            dbcontext = new MyDbContext();
        }

        public List<ImageProductModel> GetByProductId(int id)
        {
            
            var qr = from i in dbcontext.tbl_ImageProduct
                     join p in dbcontext.tbl_Product on i.ProductId equals p.Id
                     where i.ProductId == id
                     select new ImageProductModel
                     {
                         Id = i.Id,
                         Image = i.Image,
                         ProductId = i.ProductId,
                         Thumbnail = i.Thumbnail,
                         ProductName = p.ProductName,
                     };
            return qr.ToList();
        }
        public string GetByThumbnail(int id)
        {
            var qr = dbcontext.tbl_ImageProduct.SingleOrDefault(i => i.ProductId == id && i.Thumbnail == true);
            if (qr != null)
            {
                return qr.Image;
            }
            else
            {
                return null;
            }
        }

        public ImageProductModel Create(tbl_ImageProduct image)
        {
            dbcontext.tbl_ImageProduct.Add(image);
            dbcontext.SaveChanges();
            return new ImageProductModel
            {
                Id = image.Id,
                Image = image.Image,
                ProductId = image.ProductId,
                
                Thumbnail = image.Thumbnail,
            };
        }

        public ImageProductModel GetById(int id)
        {
            var img = dbcontext.tbl_ImageProduct.Find(id);
            return new ImageProductModel
            {
                Id=img.Id,
                ProductId=img.ProductId,
                Image = img.Image,
            };
        }

        public bool Update(tbl_ImageProduct img)
        {
            try
            {
                var _img = dbcontext.tbl_ImageProduct.Find(img.Id);
                _img.Image = img.Image;
                dbcontext.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public string FindImage(string img)
        {
            var rs = dbcontext.tbl_ImageProduct.SingleOrDefault(i => i.Image == img);
            if (rs!=null)
            {
                return img;
            }
            return null;
        }

        public bool Delete(int id)
        {
            try
            {
                var img = dbcontext.tbl_ImageProduct.Find(id);
                dbcontext.tbl_ImageProduct.Remove(img);
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