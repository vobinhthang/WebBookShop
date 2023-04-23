using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebBookShop.EF;
using WebBookShop.Models;

namespace WebBookShop.Services
{
    public class BannerService
    {
        private readonly MyDbContext dbcontext;
        public BannerService()
        {
            dbcontext = new MyDbContext();
        }

        public IEnumerable<BannerModel> GetAll(int page, int pageSize)
        {
            var qr = from b in dbcontext.tbl_Banner
                     orderby b.Id descending
                     select new BannerModel
                     {
                         Id = b.Id,
                         BannerName = b.BannerName,
                         Link = b.Link,
                         Sort = b.Sort,
                         Image = b.Image,
                         Status = b.Status,
                     };
            return qr.ToPagedList(page, pageSize);
        }
        public List<BannerModel> GetSlide()
        {
            var qr = from b in dbcontext.tbl_Banner
                     where b.Status == true && b.BannerName.StartsWith("slider")
                     select new BannerModel
                     {
                         Id = b.Id,
                         BannerName = b.BannerName,
                         Link = b.Link,
                         Sort = b.Sort,
                         Image = b.Image,
                         Status = b.Status,
                     };
            return qr.ToList();
        }
        public List<BannerModel> GetBanner()
        {
            var qr = from b in dbcontext.tbl_Banner
                     where b.Status == true && b.BannerName.StartsWith("banner")
                     select new BannerModel
                     {
                         Id = b.Id,
                         BannerName = b.BannerName,
                         Link = b.Link,
                         Sort = b.Sort,
                         Image = b.Image,
                         Status = b.Status,
                     };
            return qr.ToList();
        }
        public IEnumerable<BannerModel> Search(string keyword, int page, int pageSize)
        {
            if (!string.IsNullOrEmpty(keyword))
            {
                var qr = from b in dbcontext.tbl_Banner
                         orderby b.Id descending
                         where b.BannerName.Contains(keyword) || b.Link.Contains(keyword)
                         select new BannerModel
                         {
                             Id = b.Id,
                             BannerName = b.BannerName,
                             Link = b.Link,
                             Sort = b.Sort,
                             Image = b.Image,
                             Status = b.Status,
                         };
                return qr.ToPagedList(page, pageSize);
            }
            else
            {
                var qr = from b in dbcontext.tbl_Banner
                         orderby b.Id descending
                         select new BannerModel
                         {
                             Id = b.Id,
                             BannerName = b.BannerName,
                             Link = b.Link,
                             Sort = b.Sort,
                             Image = b.Image,
                             Status = b.Status,
                         };
                return qr.ToPagedList(page, pageSize);
            }

        }


        public BannerModel Create(tbl_Banner banner)
        {
            dbcontext.tbl_Banner.Add(banner);
            dbcontext.SaveChanges();
            return new BannerModel
            {
                BannerName= banner.BannerName,
                Image = banner.Image,
                Link = banner.Link,
                Sort=banner.Sort,
                Status = banner.Status,
            };
        }
        public bool ChangeStatus(int id)
        {
            var banner = dbcontext.tbl_Banner.Find(id);
            banner.Status = !banner.Status;
            dbcontext.SaveChanges();

            return (bool)banner.Status;
        }
        public bool Delete(int id)
        {
            try
            {
                var banner = dbcontext.tbl_Banner.Find(id);
                dbcontext.tbl_Banner.Remove(banner);
                dbcontext.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Update(tbl_Banner banner)
        {
            try
            {
                var _banner = dbcontext.tbl_Banner.Find(banner.Id);
                _banner.BannerName = banner.BannerName;
                _banner.Image = banner.Image;
                _banner.Link = banner.Link;
                _banner.Sort = banner.Sort;
                _banner.Status = banner.Status;
                dbcontext.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public BannerModel GetById(int id)
        {
            var banner = dbcontext.tbl_Banner.Find(id);
            return new BannerModel
            {
                Id = banner.Id,
                BannerName=banner.BannerName,
                Image=banner.Image,
                Link=banner.Link,
                Sort=banner.Sort,
                Status=banner.Status
                
            };
        }
        public string FindImage(string image)
        {
            var rs = dbcontext.tbl_Banner.SingleOrDefault(b => b.Image == image);
            if (rs != null)
            {
                return image;
            }
            else
            {
                return null;
            }
        }
    }
}