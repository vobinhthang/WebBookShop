using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebBookShop.EF;
using WebBookShop.Models;

namespace WebBookShop.Services
{
    public class CommentService
    {
        private readonly MyDbContext dbcontext;

        public CommentService()
        {
            dbcontext = new MyDbContext();
        }

        public IEnumerable<ProductCommentModel> GetAll(int page, int pageSize)
        {
            var qr = from c in dbcontext.tbl_ProductComment
                     join u in dbcontext.tbl_User on c.UserId equals u.Id
                     join p in dbcontext.tbl_Product on c.ProductId equals p.Id
                     orderby c.CreateDate descending
                     select new ProductCommentModel
                     {
                         Id = c.Id,
                         CreateDate = c.CreateDate,
                         UpdateDate = c.UpdateDate,
                         Detail = c.Detail,
                         Fullname = u.FullName,
                         ProductId = p.Id,
                         ProductName = p.ProductName,
                         Status = c.Status,
                         UserId = u.Id,
                     };
            return qr.ToPagedList(page, pageSize);
        }

        public IEnumerable<ProductCommentModel> Search(string keyword, int page, int pageSize)
        {
            if (!string.IsNullOrEmpty(keyword))
            {

                var qr = from c in dbcontext.tbl_ProductComment
                         join u in dbcontext.tbl_User on c.UserId equals u.Id
                         join p in dbcontext.tbl_Product on c.ProductId equals p.Id
                         orderby c.CreateDate descending
                         where c.CreateDate.ToString().Contains(keyword) || p.ProductName.Contains(keyword) || u.FullName.Contains(keyword)
                         select new ProductCommentModel
                         {
                             Id = c.Id,
                             CreateDate = c.CreateDate,
                             UpdateDate = c.UpdateDate,
                             Detail = c.Detail,
                             Fullname = u.FullName,
                             ProductId = p.Id,
                             ProductName = p.ProductName,
                             Status = c.Status,
                             UserId = u.Id,
                         };
                return qr.ToPagedList(page, pageSize);
            }
            else
            {

                var qr = from c in dbcontext.tbl_ProductComment
                         join u in dbcontext.tbl_User on c.UserId equals u.Id
                         join p in dbcontext.tbl_Product on c.ProductId equals p.Id
                         orderby c.CreateDate descending
                         select new ProductCommentModel
                         {
                             Id = c.Id,
                             CreateDate = c.CreateDate,
                             UpdateDate = c.UpdateDate,
                             Detail = c.Detail,
                             Fullname = u.FullName,
                             ProductId = p.Id,
                             ProductName = p.ProductName,
                             Status = c.Status,
                             UserId = u.Id,
                         };
                return qr.ToPagedList(page, pageSize);
            }

        }

        public bool Delete(int id)
        {
            try
            {
                var comment = dbcontext.tbl_ProductComment.Find(id);
                dbcontext.tbl_ProductComment.Remove(comment);
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
            var comment = dbcontext.tbl_ProductComment.Find(id);
            comment.Status = !comment.Status;
            dbcontext.SaveChanges();

            return (bool)comment.Status;
        }
    }
}