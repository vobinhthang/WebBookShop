using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebBookShop.EF;
using WebBookShop.Models;

namespace WebBookShop.Services
{
    public class NewsService
    {
        private readonly MyDbContext dbcontext;

        public NewsService()
        {
            dbcontext = new MyDbContext();
        }

        public IEnumerable<NewsModel> GetAll(int page, int pageSize)
        {
            var qr = from n in dbcontext.tbl_News
                     orderby n.Id descending
                     select new NewsModel
                     {
                         Id = n.Id,
                         CreateDate = n.CreateDate,
                         Descripton = n.Descripton,
                         Detail = n.Detail,
                         Image = n.Image,
                         NewsName = n.NewsName,
                         Status = n.Status,
                         UpdateDate = n.UpdateDate
                     };
            return qr.ToPagedList(page, pageSize);
        }

        public IEnumerable<NewsModel> Search(string keyword, int page, int pageSize)
        {
            if (!string.IsNullOrEmpty(keyword))
            {
                var qr = from n in dbcontext.tbl_News
                         orderby n.Id descending
                         where n.NewsName.Contains(keyword)
                         select new NewsModel
                         {
                             Id = n.Id,
                             CreateDate = n.CreateDate,
                             Descripton = n.Descripton,
                             Detail = n.Detail,
                             Image = n.Image,
                             NewsName = n.NewsName,
                             Status = n.Status,
                             UpdateDate = n.UpdateDate
                         };
                return qr.ToPagedList(page, pageSize);
            }
            else
            {
                var qr = from n in dbcontext.tbl_News
                         orderby n.Id descending
                         select new NewsModel
                         {
                             Id = n.Id,
                             CreateDate = n.CreateDate,
                             Descripton = n.Descripton,
                             Detail = n.Detail,
                             Image = n.Image,
                             NewsName = n.NewsName,
                             Status = n.Status,
                             UpdateDate = n.UpdateDate
                         };
                return qr.ToPagedList(page, pageSize);
            }

        }
        public NewsModel Create(tbl_News news)
        {
            news.CreateDate = DateTime.Now;
            dbcontext.tbl_News.Add(news);
            dbcontext.SaveChanges();
            return new NewsModel
            {
                CreateDate = news.CreateDate,
                Detail = news.Detail,
                Image = news.Image,
                NewsName = news.NewsName,
                Status = news.Status,
                Descripton=news.Descripton
            };
        }

        public string FindName(string name)
        {
            var rs = dbcontext.tbl_News.SingleOrDefault(n => n.NewsName == name);
            if (rs != null)
            {
                return name;
            }
            else
            {
                return null;
            }
        }

        public bool Update(tbl_News news)
        {
            try
            {
                news.UpdateDate = DateTime.Now;
                var _news = dbcontext.tbl_News.Find(news.Id);
                _news.NewsName = news.NewsName;
                _news.Image = news.Image;
                _news.UpdateDate = news.UpdateDate;
                _news.Status = news.Status;
                _news.Detail = news.Detail;
                _news.Descripton = news.Descripton;
                dbcontext.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public NewsModel GetById(int id)
        {
            var news = dbcontext.tbl_News.Find(id);
            return new NewsModel
            {
                Id = news.Id,
                Descripton = news.Descripton,
                Detail = news.Detail,
                Status = news.Status,
                Image = news.Image,
                NewsName = news.NewsName,
                CreateDate = news.CreateDate
            };
        }

        public bool ChangeStatus(int id)
        {
            var news = dbcontext.tbl_News.Find(id);
            news.Status = !news.Status;
            dbcontext.SaveChanges();

            return (bool)news.Status;
        }

        public bool Delete(int id)
        {
            try
            {
                var banner = dbcontext.tbl_News.Find(id);
                dbcontext.tbl_News.Remove(banner);
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