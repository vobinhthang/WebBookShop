using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebBookShop.EF;
using WebBookShop.Models;

namespace WebBookShop.Services
{
    public class CateService
    {
        private readonly MyDbContext dbcontext;

        public CateService()
        {
            dbcontext = new MyDbContext();
        }
        

        public List<CateModel> GetAll()
        {

            var qr = from c in dbcontext.tbl_Category
                     orderby c.CreateDate descending
                     select new CateModel
                     {
                         Id = c.Id,
                         CategoryName = c.CategoryName,
                         Status = c.Status,
                         Sort = c.Sort,
                         ParentID = c.ParentID,
                         CreatedDate = c.CreateDate,
                         UpdateDate = c.UpdateDate,
                     };

            return qr.ToList();
        }
        public IEnumerable<CateModel> GetAll(int page, int pageSize)
        {

            var qr = from c in dbcontext.tbl_Category
                     orderby c.CreateDate descending
                     select new CateModel
                     {
                         Id=c.Id,
                         CategoryName=c.CategoryName,
                         Status=c.Status,
                         Sort=c.Sort,
                         ParentID=c.ParentID,
                         CreatedDate=c.CreateDate,
                         UpdateDate=c.UpdateDate,
                     };
            
            return qr.ToPagedList(page,pageSize);        
        }
        public List<CateModel> GetCateChildId(int id)
        {
            var cate = dbcontext.tbl_Category.Find(id);
            
            if (cate.ParentID != null)
            {
                 var qr = from c in dbcontext.tbl_Category
                         where c.ParentID == cate.ParentID
                         select new CateModel
                         {
                             Id = c.Id,
                             CategoryName = c.CategoryName,
                             Status = c.Status,
                             Sort = c.Sort,
                             ParentID = c.ParentID,
                             CreatedDate = c.CreateDate,
                             UpdateDate = c.UpdateDate,
                         };
                return qr.Where(c => c.Status == true).ToList();
            }
            else
            {

                 var qr = from c in dbcontext.tbl_Category
                         where c.ParentID == cate.Id
                         select new CateModel
                         {
                             Id = c.Id,
                             CategoryName = c.CategoryName,
                             Status = c.Status,
                             Sort = c.Sort,
                             ParentID = c.ParentID,
                             CreatedDate = c.CreateDate,
                             UpdateDate = c.UpdateDate,
                         };
                return qr.Where(c => c.Status == true).ToList();
            }

        }
        public CateModel GetCateParentId(int id)
        {
            var childcate = dbcontext.tbl_Category.Find(id);
            if (childcate.ParentID != null)
            {
                var parentcate = dbcontext.tbl_Category.SingleOrDefault(c => c.Id == childcate.ParentID && c.Status == true);
                if (parentcate != null)
                {
                    return new CateModel()
                    {
                        Id = parentcate.Id,
                        CategoryName = parentcate.CategoryName,
                        Status = parentcate.Status,
                    };
                }
            }
            else
            {
                return new CateModel()
                {
                    Id = childcate.Id,
                    CategoryName = childcate.CategoryName,
                    Status = childcate.Status,
                };
            }

            return null;
        }

        public bool ChangeStatus(int id)
        {
            var cate = dbcontext.tbl_Category.Find(id);
            cate.Status = !cate.Status;
            dbcontext.SaveChanges();

            return (bool)cate.Status;
        }

        public CateModel Create(tbl_Category cate)
        {
            cate.CreateDate = DateTime.Now;
            dbcontext.tbl_Category.Add(cate);
            dbcontext.SaveChanges();
            var _cate = new CateModel
            {
                Id = cate.Id,
                CategoryName =cate.CategoryName,
                Status = cate.Status,
                Sort = cate.Sort,
                ParentID = cate.ParentID,
                CreatedDate = cate.CreateDate,
            };
            return _cate;
        }

        public string FindCateName(string catename)
        {
            var rs = dbcontext.tbl_Category.SingleOrDefault(c => c.CategoryName == catename);
            if (rs!=null)
            {
                return catename;
            }
            else
            {
                return null;
            }
        }

        public bool Update(tbl_Category cate)
        {
            try
            {
                var _cate = dbcontext.tbl_Category.Find(cate.Id);
                cate.UpdateDate = DateTime.Now;

                _cate.CategoryName = cate.CategoryName.Trim();
                _cate.Sort = cate.Sort;
                _cate.Status = cate.Status;
                _cate.ParentID = cate.ParentID;
                _cate.UpdateDate = cate.UpdateDate;
                dbcontext.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public CateModel GetById(int id)
        {
            var cate = dbcontext.tbl_Category.Find(id);
            return new CateModel
            {
                Id=cate.Id,
                CategoryName=cate.CategoryName,
                ParentID=cate.ParentID,
                Status=cate.Status,
                Sort=cate.Sort
            };
        }
        public bool Delete(int id)
        {
            try
            {
                var cate = dbcontext.tbl_Category.Find(id);
                dbcontext.tbl_Category.Remove(cate);
                dbcontext.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public IEnumerable<CateModel> Search(string keyword, int page, int pageSize)
        {
            if (!string.IsNullOrEmpty(keyword))
            {
                var qr = from c in dbcontext.tbl_Category
                         orderby c.CreateDate
                         where c.CategoryName.Contains(keyword)
                         select new CateModel
                         {
                             Id = c.Id,
                             CategoryName = c.CategoryName,
                             Status = c.Status,
                             Sort = c.Sort,
                             ParentID = c.ParentID,
                             CreatedDate = c.CreateDate,
                             UpdateDate = c.UpdateDate,
                         };
                return qr.ToPagedList(page, pageSize);
            }
            else
            {
                var qr = from c in dbcontext.tbl_Category
                         orderby c.CreateDate
                         select new CateModel
                         {
                             Id = c.Id,
                             CategoryName = c.CategoryName,
                             Status = c.Status,
                             Sort = c.Sort,
                             ParentID = c.ParentID,
                             CreatedDate = c.CreateDate,
                             UpdateDate = c.UpdateDate,
                         };
                return qr.ToPagedList(page, pageSize);
            }

        }

        public IEnumerable<CateModel> Sorting(string sortcatename, string sortupdate, string sortcreate, int page, int pageSize)
        {
            if (sortcatename == "ASC" && sortupdate == "" && sortcreate == "")
            {
                var qr = from c in dbcontext.tbl_Category
                         orderby c.CategoryName ascending
                         select new CateModel
                         {
                             Id = c.Id,
                             CategoryName = c.CategoryName,
                             Status = c.Status,
                             Sort = c.Sort,
                             ParentID = c.ParentID,
                             CreatedDate = c.CreateDate,
                             UpdateDate = c.UpdateDate,
                         };
                return qr.ToPagedList(page, pageSize);
            }
            else if (sortcatename == "DES" && sortupdate == "" && sortcreate == "")
            {
                var qr = from c in dbcontext.tbl_Category
                         orderby c.CategoryName descending
                         select new CateModel
                         {
                             Id = c.Id,
                             CategoryName = c.CategoryName,
                             Status = c.Status,
                             Sort = c.Sort,
                             ParentID = c.ParentID,
                             CreatedDate = c.CreateDate,
                             UpdateDate = c.UpdateDate,
                         };
                return qr.ToPagedList(page, pageSize);

            }

            if (sortupdate == "ASC" && sortcatename == "" && sortcreate == "")
            {
                var qr = from c in dbcontext.tbl_Category
                         orderby c.UpdateDate ascending
                         select new CateModel
                         {
                             Id = c.Id,
                             CategoryName = c.CategoryName,
                             Status = c.Status,
                             Sort = c.Sort,
                             ParentID = c.ParentID,
                             CreatedDate = c.CreateDate,
                             UpdateDate = c.UpdateDate,
                         };
                return qr.ToPagedList(page, pageSize);

            }
            else if (sortupdate == "DES" && sortcatename == "" && sortcreate == "")
            {
                var qr = from c in dbcontext.tbl_Category
                         orderby c.UpdateDate descending
                         select new CateModel
                         {
                             Id = c.Id,
                             CategoryName = c.CategoryName,
                             Status = c.Status,
                             Sort = c.Sort,
                             ParentID = c.ParentID,
                             CreatedDate = c.CreateDate,
                             UpdateDate = c.UpdateDate,
                         };
                return qr.ToPagedList(page, pageSize);
            }

            if (sortcreate == "ASC" && sortcatename == "" && sortupdate == "")
            {
                var qr = from c in dbcontext.tbl_Category
                         orderby c.CreateDate ascending
                         select new CateModel
                         {
                             Id = c.Id,
                             CategoryName = c.CategoryName,
                             Status = c.Status,
                             Sort = c.Sort,
                             ParentID = c.ParentID,
                             CreatedDate = c.CreateDate,
                             UpdateDate = c.UpdateDate,
                         };
                return qr.ToPagedList(page, pageSize);
            }
            else if (sortcreate == "DES" && sortcatename == "" && sortupdate == "")
            {
                var  qr = from c in dbcontext.tbl_Category
                                  orderby c.CreateDate descending
                                  select new CateModel
                                  {
                                      Id = c.Id,
                                      CategoryName = c.CategoryName,
                                      Status = c.Status,
                                      Sort = c.Sort,
                                      ParentID = c.ParentID,
                                      CreatedDate = c.CreateDate,
                                      UpdateDate = c.UpdateDate,
                                  };
                return qr.ToPagedList(page, pageSize);
            }
            return null;
        }
    }
}