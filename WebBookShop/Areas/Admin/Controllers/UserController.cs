using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using WebBookShop.Areas.Admin.Models;
using WebBookShop.EF;
using WebBookShop.Models;
using WebBookShop.Services;

namespace WebBookShop.Areas.Admin.Controllers
{
    public class UserController : BaseController
    {
        // GET: Admin/User
        public ActionResult Index(string sort, string sortBy, string keyword, int page = 1, int pageSize = 5)
        {
            var service = new UserService();

            ViewBag.SortUser = sort == "DES" ? "ASC" : "DES";
            ViewBag.PageSizeUser = pageSize;
            ViewBag.PageUser = page;
            ViewBag.SortByPageUser = sortBy;

            PageListModel.pageSize = pageSize;
            PageListModel.page = page;
            PageListModel.sortBy = sortBy;
            PageListModel.sort = sort;

            ShowOption();

            IEnumerable<UserModel> users;
            if (keyword != null)
            {
                ViewBag.SearchRole = keyword;
                PageListModel.keyword = keyword;

                users = service.Search(keyword, page, pageSize);
            }
            else
            {
                users = service.GetAll(page, pageSize);
                
            }
            if (users.ToList().Count==0)
            {
                 users = service.GetAllNotRole(page, pageSize);
            }
               
            if (TempData["IconSortEmail"] == null && TempData["IconSortUserUpdate"] == null && TempData["IconSortUserCreate"] == null)
            {
                TempData["IconSortEmail"] = "up";
                TempData["IconSortUserUpdate"] = "up";
                TempData["IconSortUserCreate"] = "up";
            }

            switch (sortBy)
            {
                case "email":
                    switch (sort)
                    {
                        case "ASC":
                            users = service.Sorting("ASC", "", "", page, pageSize);
                            ViewBag.PageSizeUser = pageSize;
                            PageListModel.pageSize = pageSize;
                            ViewBag.SortPageUser = "ASC";
                            PageListModel.sort = sort;
                            TempData["IconSortEmail"] = "up";
                            TempData["IconSortUserUpdate"] = "up";
                            TempData["IconSortUserCreate"] = "up";
                            break;
                        case "DES":
                            users = service.Sorting("DES", "", "", page, pageSize);
                            ViewBag.SortPageUser = "DES";
                            ViewBag.PageSizeUser = pageSize;
                            PageListModel.pageSize = pageSize;
                            PageListModel.sort = sort;
                            TempData["IconSortEmail"] = "down";
                            TempData["IconSortUserUpdate"] = "up";
                            TempData["IconSortUserCreate"] = "up";
                            break;
                        default:
                            users = service.Sorting("ASC", "", "", page, pageSize);
                            ViewBag.SortPageUser = "ASC";
                            ViewBag.PageSizeUser = pageSize;
                            PageListModel.pageSize = pageSize;
                            PageListModel.sort = sort;
                            TempData["IconSortEmail"] = "up";
                            TempData["IconSortUserUpdate"] = "up";
                            TempData["IconSortUserCreate"] = "up";
                            break;
                    }
                    break;
                case "updatedate":
                    switch (sort)
                    {
                        case "ASC":
                            users = service.Sorting("", "ASC", "", page, pageSize);
                            ViewBag.SortPageUser = "ASC";
                            ViewBag.PageSizeUser = pageSize;
                            PageListModel.pageSize = pageSize;
                            PageListModel.sort = sort;
                            TempData["IconSortUserUpdate"] = "up";
                            TempData["IconSortEmail"] = "up";
                            TempData["IconSortUserCreate"] = "up";
                            break;
                        case "DES":
                            users = service.Sorting("", "DES", "", page, pageSize);
                            ViewBag.SortPageUser = "DES";
                            ViewBag.PageSizeUser = pageSize;
                            PageListModel.pageSize = pageSize;
                            PageListModel.sort = sort;
                            TempData["IconSortUserUpdate"] = "down";
                            TempData["IconSortEmail"] = "up";
                            TempData["IconSortUserCreate"] = "up";
                            break;
                        default:
                            users = service.Sorting("", "ASC", "", page, pageSize);
                            ViewBag.SortPageUser = "ASC";
                            ViewBag.PageSizeUser = pageSize;
                            PageListModel.pageSize = pageSize;
                            PageListModel.sort = sort;
                            TempData["IconSortUserUpdate"] = "up";
                            TempData["IconSortEmail"] = "up";
                            TempData["IconSortUserCreate"] = "up";
                            break;
                    }
                    break;
                case "createdate":
                    switch (sort)
                    {
                        case "ASC":
                            users = service.Sorting("", "", "ASC", page, pageSize);
                            ViewBag.SortPageUser = "ASC";
                            ViewBag.PageSizeUser = pageSize;
                            PageListModel.pageSize = pageSize;
                            PageListModel.sort = sort;
                            TempData["IconSortUserUpdate"] = "up";
                            TempData["IconSortEmail"] = "up";
                            TempData["IconSortUserCreate"] = "up";
                            break;
                        case "DES":
                            users = service.Sorting("", "", "DES", page, pageSize);
                            ViewBag.SortPageUser = "DES";
                            ViewBag.PageSizeUser = pageSize;
                            PageListModel.pageSize = pageSize;
                            PageListModel.sort = sort;
                            TempData["IconSortUserUpdate"] = "up";
                            TempData["IconSortEmail"] = "up";
                            TempData["IconSortUserCreate"] = "down";
                            break;
                        default:
                            users = service.Sorting("", "", "ASC", page, pageSize);
                            ViewBag.SortPageUser = "ASC";
                            ViewBag.PageSizeUser = pageSize;
                            PageListModel.pageSize = pageSize;
                            PageListModel.sort = sort;
                            TempData["IconSortUserUpdate"] = "up";
                            TempData["IconSortEmail"] = "up";
                            TempData["IconSortUserCreate"] = "up";
                            break;
                    }
                    break;
                default:
                    break;
            }

            return View(users);
        }

        [HttpPost]
        public ActionResult Index(string keyword)
        {
            var service = new UserService();
            var users = service.Search(keyword, PageListModel.page, PageListModel.pageSize);
            ViewBag.SearchUser = keyword;

            PageListModel.keyword = keyword;
            ShowOption();
            return View(users);
        }
        public void ShowOption()
        {
            
            var options = SharedData.OptionPageNumber(PageListModel.sort, PageListModel.sortBy, PageListModel.page, PageListModel.pageSize, PageListModel.keyword);
            TempData["showpagesize"] = options;
            TempData["IconSortEmail"] = "up";
            TempData["IconSortUserUpdate"] = "up";
            TempData["IconSortUserCreate"] = "up";
        }

        [HttpGet]
        public ActionResult Create()
        {
            ListRoleId();
            return View();
        }

        [HttpPost]
        public ActionResult Create(tbl_User user,UserModel model)
        {
            var service= new UserService();
            ListRoleId();

            var email = service.FindEmail(model.Email);
            //model.Phone != null &&
            //   model.Phone.Length >= 10 && model.Phone.Length <= 15
            if (model.Email == email && model.Email!=null && model.Password!=null)
            {
                TempData["CREATEUSER"] = "Email đã tồn tại";
                TempData["ALEART"] = "warning";
            }

            if(model.Email!=null && !model.Email.StartsWith(" ") && model.Email!=email && model.Password!=null && !model.Password.StartsWith(" ")
                && model.Password.Length>=6)
            {
                var _user = service.Create(user);
                if (_user != null)
                {
                    TempData["CREATEUSER"] = "Thêm mới người dùng thành công";
                    TempData["ALEART"] = "success";
                }
                else
                {
                    TempData["CREATEUSER"] = "Thêm thất bại";
                    TempData["ALEART"] = "danger";
                }
            }

            return View();
        }

        [HttpGet]
        public ActionResult Detail(int id)
        {
            var service = new UserService();
            var user = service.GetById(id);
            //ViewBag.GetIdUser = id;
            
            return View(user);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var userService = new UserService();
            var user = userService.GetById(id);
          
            ListRoleId(user.RoleID);
            return View(user);
        }

        [HttpPost]
        public ActionResult Edit(tbl_User user,UserModel model)
        {
            var service = new UserService();
            ListRoleId(user.RoleId);

            if (model.Email != null && !model.Email.StartsWith(" ") && model.Password != null && !model.Password.StartsWith(" ")
                 && model.Password.Length >= 6)
            {
                var rs = service.Update(user);
                if (rs)
                {
                    TempData["UPDATEUSER"] = "Cập nhập người dùng thành công.";
                    TempData["ALEART"] = "success";
                }
                else
                {
                    TempData["UPDATEUSER"] = "Cập nhập thất bại";
                    TempData["ALEART"] = "danger";
                }
            }
            return View();
        }

    
        public ActionResult Delete(int id)
        {
            var service = new UserService();
            var rs = service.Delete(id);
            if (rs)
            {
                return RedirectToAction("index");
            }
            else
            {
                return View();
            }
        }

        public void ListRoleId(int? selectId=null)
        {
            var service = new RoleService();
            var roles = service.GetAll();
            ViewBag.RoleList = new SelectList(roles, "Id", "RoleName", selectId);
        }
    }
}