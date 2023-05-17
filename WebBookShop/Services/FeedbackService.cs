using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebBookShop.EF;
using WebBookShop.Models;

namespace WebBookShop.Services
{
    public class FeedbackService
    {
        private readonly MyDbContext dbcontext;

        public FeedbackService()
        {
            dbcontext = new MyDbContext();
        }

        public IEnumerable<FeedbackModel> GetAll(int page, int pageSize)
        {
            var qr = from f in dbcontext.tbl_Feedback
                     orderby f.Id descending
                     select new FeedbackModel
                     {
                         Id = f.Id,
                         Address = f.Address,
                         Detail = f.Detail,
                         Email= f.Email,
                         Name = f.Name,
                         Phone=f.Phone
                     };
            return qr.ToPagedList(page, pageSize);
        }

        public IEnumerable<FeedbackModel> Search(string keyword, int page, int pageSize)
        {
            if (!string.IsNullOrEmpty(keyword))
            {
                var qr = from f in dbcontext.tbl_Feedback
                         orderby f.Id descending
                         where f.Phone == keyword || f.Name.Contains(keyword) || f.Email == keyword
                         select new FeedbackModel
                         {
                             Id = f.Id,
                             Address = f.Address,
                             Detail = f.Detail,
                             Email = f.Email,
                             Name = f.Name,
                             Phone = f.Phone
                         };
                return qr.ToPagedList(page, pageSize);
            }
            else
            {
                var qr = from f in dbcontext.tbl_Feedback
                         orderby f.Id descending
                         select new FeedbackModel
                         {
                             Id = f.Id,
                             Address = f.Address,
                             Detail = f.Detail,
                             Email = f.Email,
                             Name = f.Name,
                             Phone = f.Phone
                         };
                return qr.ToPagedList(page, pageSize);
            }

        }

        public bool Delete(int id)
        {
            try
            {
                var fb = dbcontext.tbl_Feedback.Find(id);
                dbcontext.tbl_Feedback.Remove(fb);
                dbcontext.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }


        public FeedbackModel Create(tbl_Feedback feedback)
        {
            dbcontext.tbl_Feedback.Add(feedback);
            dbcontext.SaveChanges();
            var model = new FeedbackModel
            {
                Id = feedback.Id,
                Address =feedback.Address,
                Phone =feedback.Phone,
                Email =feedback.Email,
                Name =feedback.Name,
                Detail =feedback.Detail,
            };
            return model;
        }
    }
}