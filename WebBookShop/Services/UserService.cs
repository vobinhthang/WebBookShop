using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebBookShop.EF;
using WebBookShop.Models;

namespace WebBookShop.Services
{
    public class UserService
    {
        private readonly MyDbContext dbcontext;
        public UserService()
        {
            dbcontext = new MyDbContext();
        }

        public bool Login(string _email, string _password)
        {

            var query = from u in dbcontext.tbl_User
                        join r in dbcontext.tbl_Role on u.RoleId equals r.Id
                        where u.Email == _email && u.Password == _password && r.RoleName == "Admin"
                        select new UserModel { 
                            Email = u.Email,
                            Password = u.Password,
                        };

            if (query.ToList().Count>0)
            {
                return true;
            }
            return false;
        }

        public IEnumerable<UserModel> GetAll(int page, int pageSize)
        {
            var query = from u in dbcontext.tbl_User
                        join r in dbcontext.tbl_Role on u.RoleId equals r.Id
                        orderby u.CreateDate ascending
                        select new UserModel
                        {
                            Id = u.Id,
                            Email = u.Email,
                            Password = u.Password,
                            RoleName = r.RoleName,
                            RoleID = u.RoleId,
                            CreateDate=u.CreateDate,
                            UpdateDate=u.UpdateDate,
                        };
            return query.ToPagedList(page,pageSize);
        }

        public UserModel GetById(int id)
        {
            var query = from u in dbcontext.tbl_User
                        join r in dbcontext.tbl_Role on u.RoleId equals r.Id
                        where u.Id == id
                        select new UserModel
                        {
                            Id = u.Id,
                            Email = u.Email,
                            Password = u.Password,
                            Fullname = u.FullName,
                            Phone = u.Phone,
                            Address = u.Address,
                            CreateDate = u.CreateDate,
                            UpdateDate = u.UpdateDate,
                            RoleName = r.RoleName,
                            RoleID =u.RoleId,
                        };
            UserModel user = query.First();
            return user;
        }

        public bool Update(tbl_User _user)
        {
            try
            {
                var user = dbcontext.tbl_User.Find(_user.Id);
               

                _user.UpdateDate = DateTime.Now;

                user.UpdateDate = _user.UpdateDate;
                if (_user.FullName == null)
                {
                    user.FullName = string.Empty;
                }
                else
                {
                    user.FullName = _user.FullName;
                }
                if (_user.Address == null)
                {
                    user.Address = string.Empty;
                }
                else
                {
                    user.Address = _user.Address;
                }
                if (_user.Phone == null)
                {
                    user.Phone = string.Empty;
                }
                else
                {
                    user.Phone = _user.Phone;
                }
                user.Email = _user.Email;
                user.Password = _user.Password; 
                user.RoleId = _user.RoleId;
                dbcontext.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public string FindEmail(string _email)
        {
            var email = dbcontext.tbl_User.SingleOrDefault(u => u.Email == _email);
            if (email != null)
            {
                return _email;
            }
            else
            {
                return null;
            }
        }

        public UserModel Create(tbl_User _user)
        {
            _user.CreateDate = DateTime.Now;
            dbcontext.tbl_User.Add(_user);
            dbcontext.SaveChanges();
            var user = new UserModel
            {
                Id=_user.Id,
                Fullname=_user.FullName,
                Email= _user.Email,
                Password= _user.Password,
                RoleID =_user.RoleId,
                Address=_user.Address,
                Phone=_user.Phone,
                CreateDate=_user.CreateDate,
            };
            return user;
        }

        public bool Delete(int id)
        {
            try
            {
                var user = dbcontext.tbl_User.Find(id);
                dbcontext.tbl_User.Remove(user);
                dbcontext.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public IEnumerable<UserModel> Search(string keyword, int page, int pageSize)
        {
            if (!string.IsNullOrEmpty(keyword))
            {
                var qr = from u in dbcontext.tbl_User
                         join r in dbcontext.tbl_Role on u.RoleId equals r.Id
                         orderby u.Email ascending
                         where u.Email.Contains(keyword)
                         select new UserModel
                         {
                             Id=u.Id,
                             Email=u.Email,
                             Password=u.Password,
                             RoleID=u.RoleId,
                             RoleName=r.RoleName,
                             CreateDate=u.CreateDate,
                             UpdateDate=u.UpdateDate,
                         };
                return qr.ToPagedList(page, pageSize);
            }
            else
            {
                var qr = from u in dbcontext.tbl_User
                         join r in dbcontext.tbl_Role on u.RoleId equals r.Id
                         orderby u.Email ascending
                         select new UserModel
                         {
                             Id = u.Id,
                             Email = u.Email,
                             Password = u.Password,
                             RoleID = u.RoleId,
                             RoleName = r.RoleName,
                             CreateDate = u.CreateDate,
                             UpdateDate = u.UpdateDate,
                         };
                return qr.ToPagedList(page, pageSize);
            }

        }

        public IEnumerable<UserModel> Sorting(string sortemail, string sortupdate, string sortcreate, int page, int pageSize)
        {
            if (sortemail == "ASC" && sortupdate == "" && sortcreate == "")
            {
                var qr = from u in dbcontext.tbl_User
                         join r in dbcontext.tbl_Role on u.RoleId equals r.Id
                         orderby u.Email ascending
                         select new UserModel
                         {
                             Id = u.Id,
                             Email = u.Email,
                             Password = u.Password,
                             RoleID = u.RoleId,
                             RoleName = r.RoleName,
                             CreateDate = u.CreateDate,
                             UpdateDate = u.UpdateDate,
                         };
                return qr.ToPagedList(page, pageSize);
            }
            else if (sortemail == "DES" && sortupdate == "" && sortcreate == "")
            {
                var qr = from u in dbcontext.tbl_User
                         join r in dbcontext.tbl_Role on u.RoleId equals r.Id
                         orderby u.Email descending
                         select new UserModel
                         {
                             Id = u.Id,
                             Email = u.Email,
                             Password = u.Password,
                             RoleID = u.RoleId,
                             RoleName = r.RoleName,
                             CreateDate = u.CreateDate,
                             UpdateDate = u.UpdateDate,
                         };
                return qr.ToPagedList(page, pageSize);

            }

            if (sortupdate == "ASC" && sortemail == "" && sortcreate == "")
            {
                var qr = from u in dbcontext.tbl_User
                         join r in dbcontext.tbl_Role on u.RoleId equals r.Id
                         orderby u.UpdateDate ascending
                         select new UserModel
                         {
                             Id = u.Id,
                             Email = u.Email,
                             Password = u.Password,
                             RoleID = u.RoleId,
                             RoleName = r.RoleName,
                             CreateDate = u.CreateDate,
                             UpdateDate = u.UpdateDate,
                         };
                return qr.ToPagedList(page, pageSize);

            }
            else if (sortupdate == "DES" && sortemail == "" && sortcreate == "")
            {
                var qr = from u in dbcontext.tbl_User
                         join r in dbcontext.tbl_Role on u.RoleId equals r.Id
                         orderby u.UpdateDate descending
                         select new UserModel
                         {
                             Id = u.Id,
                             Email = u.Email,
                             Password = u.Password,
                             RoleID = u.RoleId,
                             RoleName = r.RoleName,
                             CreateDate = u.CreateDate,
                             UpdateDate = u.UpdateDate,
                         };
                return qr.ToPagedList(page, pageSize);
            }

            if (sortcreate == "ASC" && sortemail == "" && sortupdate == "")
            {
                var qr = from u in dbcontext.tbl_User
                         join r in dbcontext.tbl_Role on u.RoleId equals r.Id
                         orderby u.CreateDate ascending
                         select new UserModel
                         {
                             Id = u.Id,
                             Email = u.Email,
                             Password = u.Password,
                             RoleID = u.RoleId,
                             RoleName = r.RoleName,
                             CreateDate = u.CreateDate,
                             UpdateDate = u.UpdateDate,
                         };
                return qr.ToPagedList(page, pageSize);
            }
            else if (sortcreate == "DES" && sortemail == "" && sortupdate == "")
            {
                var qr = from u in dbcontext.tbl_User
                         join r in dbcontext.tbl_Role on u.RoleId equals r.Id
                         orderby u.CreateDate descending
                         select new UserModel
                         {
                             Id = u.Id,
                             Email = u.Email,
                             Password = u.Password,
                             RoleID = u.RoleId,
                             RoleName = r.RoleName,
                             CreateDate = u.CreateDate,
                             UpdateDate = u.UpdateDate,
                         };
                return qr.ToPagedList(page, pageSize);
            }
            return null;
        }
    }
}