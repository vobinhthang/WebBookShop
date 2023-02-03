using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBookShop.Areas.Admin.Models;
using WebBookShop.EF;
using WebBookShop.Models;

namespace WebBookShop.Services
{
    public class RoleService
    {
        private readonly MyDbContext dbcontext;

        public RoleService()
        {
            dbcontext = new MyDbContext();
        }
        public List<RoleModel> GetAll()
        {
            var query = from r in dbcontext.tbl_Role
                        orderby r.RoleName
                        select new RoleModel
                        {
                            Id = r.Id,
                            RoleName = r.RoleName,
                            CreatedDate = r.CreatedDate,
                            UpdateDate = r.UpdateDate,
                        };
            return query.ToList();
        }

        public IEnumerable<RoleModel> GetAll(int page , int pageSize)
        {
            var query = from r in dbcontext.tbl_Role
                        orderby r.CreatedDate ascending
                        select new RoleModel
                        {
                            Id = r.Id,
                            RoleName = r.RoleName,
                            CreatedDate = r.CreatedDate,
                            UpdateDate= r.UpdateDate,
                        };
            return query.ToPagedList(page, pageSize);         
        }

        public RoleModel Create(tbl_Role _role)
        {   
           
            _role.CreatedDate = DateTime.UtcNow;
            dbcontext.tbl_Role.Add(_role);
            dbcontext.SaveChanges();
            var role = new RoleModel {
                Id=_role.Id,
                RoleName = _role.RoleName,
                CreatedDate = _role.CreatedDate,
            };

            return role;
        }

        public bool Update(tbl_Role _role)
        {
            try
            {
                var role = dbcontext.tbl_Role.Find(_role.Id);
               
                _role.UpdateDate = DateTime.Now;
                role.UpdateDate = _role.UpdateDate;
                role.RoleName = _role.RoleName.Trim();
                dbcontext.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        
        public RoleModel GetById(int _id)
        {
            var role = dbcontext.tbl_Role.Find(_id);
            var model = new RoleModel
            {
                Id=role.Id,
                RoleName = role.RoleName,
            };
            return model;
        }

        public bool Delete(int id)
        {
            try
            {
                var role = dbcontext.tbl_Role.Find(id);
                dbcontext.tbl_Role.Remove(role);
                dbcontext.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public string FindRoleName(string _rolename)
        {
            var rs = dbcontext.tbl_Role.SingleOrDefault(r => r.RoleName.Equals(_rolename));
            
            if (rs != null)
            {
                return _rolename;
            }
            else
            {
                return null;
            }
        }

        public IEnumerable<RoleModel> Search(string keyword,int page,int pageSize)
        {
            if (!string.IsNullOrEmpty(keyword))
            {
                var qr = from r in dbcontext.tbl_Role
                         orderby r.RoleName ascending
                         where r.RoleName.Contains(keyword)     
                         select new RoleModel
                         {
                             Id = r.Id,
                             RoleName = r.RoleName,
                             CreatedDate = r.CreatedDate,
                             UpdateDate = r.UpdateDate,
                         };
                return qr.ToPagedList(page, pageSize);
            }
            else
            {
                var qr = from r in dbcontext.tbl_Role
                         orderby r.RoleName ascending
                         select new RoleModel
                            {
                                Id = r.Id,
                                RoleName = r.RoleName,
                                CreatedDate = r.CreatedDate,
                                UpdateDate = r.UpdateDate,
                            };
                return qr.ToPagedList(page, pageSize);
            }
            
        }

        public IEnumerable<RoleModel> Sorting(string sortname, string sortupdate, string sortcreate,int page ,int pageSize)
        {
            if (sortname == "ASC" && sortupdate=="" && sortcreate == "")
            {
                var qr = from r in dbcontext.tbl_Role
                         orderby r.RoleName ascending
                         select new RoleModel
                         {
                             Id = r.Id,
                             RoleName = r.RoleName,
                             CreatedDate = r.CreatedDate,
                             UpdateDate = r.UpdateDate,
                         };
                return qr.ToPagedList(page, pageSize);
            }
            else if(sortname == "DES" && sortupdate == "" && sortcreate == "")
            {
                var qr = from r in dbcontext.tbl_Role
                         orderby r.RoleName descending
                         select new RoleModel
                         {
                             Id = r.Id,
                             RoleName = r.RoleName,
                             CreatedDate = r.CreatedDate,
                             UpdateDate = r.UpdateDate,
                         };
                return qr.ToPagedList(page, pageSize);
            }

            if(sortupdate== "ASC" && sortname == "" && sortcreate == "")
            {
                var qr = from r in dbcontext.tbl_Role
                         orderby r.UpdateDate ascending
                         select new RoleModel
                         {
                             Id = r.Id,
                             RoleName = r.RoleName,
                             CreatedDate = r.CreatedDate,
                             UpdateDate = r.UpdateDate,
                         };
                return qr.ToPagedList(page, pageSize);
            }
            else if (sortupdate == "DES" && sortname == "" && sortcreate == "")
            {
                var qr = from r in dbcontext.tbl_Role
                         orderby r.UpdateDate descending
                         select new RoleModel
                         {
                             Id = r.Id,
                             RoleName = r.RoleName,
                             CreatedDate = r.CreatedDate,
                             UpdateDate = r.UpdateDate,
                         };
                return qr.ToPagedList(page, pageSize);
            }

            if (sortcreate == "ASC" && sortname == "" && sortupdate == "")
            {
                var qr = from r in dbcontext.tbl_Role
                         orderby r.CreatedDate ascending
                         select new RoleModel
                         {
                             Id = r.Id,
                             RoleName = r.RoleName,
                             CreatedDate = r.CreatedDate,
                             UpdateDate = r.UpdateDate,
                         };
                return qr.ToPagedList(page, pageSize);
            }
            else if (sortcreate == "DES" && sortname == "" && sortupdate == "")
            {
                var qr = from r in dbcontext.tbl_Role
                         orderby r.CreatedDate descending
                         select new RoleModel
                         {
                             Id = r.Id,
                             RoleName = r.RoleName,
                             CreatedDate = r.CreatedDate,
                             UpdateDate = r.UpdateDate,
                         };
                return qr.ToPagedList(page, pageSize);
            }
            return null;
        }

        
    }
}