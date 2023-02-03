using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBookShop.Areas.Admin.Models;
using WebBookShop.EF;
using WebBookShop.Models;
using WebBookShop.Services;

namespace WebBookShop.Areas.Admin.Controllers
{
    public class RoleController : Controller
    {
        // GET: Admin/Role
        [HttpGet]
        public ActionResult Index(string sort,string sortBy, string keyword, int page=1,int pageSize=5)
        {
            var service = new RoleService();

            ViewBag.SortRole = sort == "DES" ? "ASC" : "DES";
            ViewBag.PageSizeRole = pageSize;
            ViewBag.PageRole = page;
            ViewBag.SortByPageRole = sortBy;
            //TempData["ShowPageSize"] = pageSize;
            PageListModel.pageSize = pageSize;
            PageListModel.page = page;
            PageListModel.sortBy = sortBy;
            PageListModel.sort = sort;
            //
            ShowOption();
            //
            IEnumerable<RoleModel> roles;     
            if (keyword != null)
            {
                ViewBag.SearchRole= keyword;
                PageListModel.keyword= keyword;
           
                roles = service.Search( keyword,page,pageSize);
            }
            else
            {
                roles = service.GetAll(page,pageSize);
            }

            if(TempData["IconSortRoleName"] == null && TempData["IconSortRoleUpdate"] == null && TempData["IconSortRoleCreate"] == null)
            {
                TempData["IconSortRoleName"] = "up";
                TempData["IconSortRoleUpdate"] = "up";
                TempData["IconSortRoleCreate"] = "up";
            }
           
            switch (sortBy)
            {
                    case "rolename":
                        switch (sort)
                        {
                            case "ASC":

                                roles = service.Sorting("ASC", "", "", page, pageSize);
                                ViewBag.PageSizeRole = pageSize;
                                PageListModel.pageSize = pageSize;
                                ViewBag.SortPageRole = "ASC";
                                PageListModel.sort = sort;
                                TempData["IconSortRoleName"] = "up";
                                TempData["IconSortRoleUpdate"] = "up";
                                TempData["IconSortRoleCreate"] = "up";
                            break;
                            case "DES":
                                roles = service.Sorting("DES", "", "", page, pageSize);
                                ViewBag.SortPageRole = "DES";
                                ViewBag.PageSizeRole = pageSize;
                                PageListModel.pageSize = pageSize;
                                PageListModel.sort = sort;
                                TempData["IconSortRoleName"] = "down";
                                TempData["IconSortRoleUpdate"] = "up";
                                TempData["IconSortRoleCreate"] = "up";
                            break;
                            default:
                                roles = service.Sorting("ASC", "", "", page, pageSize);
                                ViewBag.SortPageRole = "ASC";
                                ViewBag.PageSizeRole = pageSize;
                                PageListModel.pageSize = pageSize;
                                PageListModel.sort = sort;
                                TempData["IconSortRoleName"] = "up";
                                TempData["IconSortRoleUpdate"] = "up";
                                TempData["IconSortRoleCreate"] = "up";
                            break;
                        }
                        break;
                    case "updatedate":
                        switch (sort)
                        {
                            case "ASC":
                                roles = service.Sorting("", "ASC", "", page, pageSize);
                                ViewBag.SortPageRole = "ASC";
                                ViewBag.PageSizeRole = pageSize;
                                PageListModel.pageSize = pageSize;
                                PageListModel.sort = sort;
                                TempData["IconSortRoleUpdate"] = "up";
                                TempData["IconSortRoleName"] = "up";
                                TempData["IconSortRoleCreate"] = "up";
                            break;
                            case "DES":
                                roles = service.Sorting("", "DES", "", page, pageSize);
                                ViewBag.SortPageRole = "DES";
                                ViewBag.PageSizeRole = pageSize;
                                PageListModel.pageSize = pageSize;
                                PageListModel.sort = sort;
                                TempData["IconSortRoleUpdate"] = "down";
                                TempData["IconSortRoleName"] = "up";
                                TempData["IconSortRoleCreate"] = "up";
                            break;
                            default:
                                roles = service.Sorting("", "ASC", "", page, pageSize);
                                ViewBag.SortPageRole = "ASC";
                                ViewBag.PageSizeRole = pageSize;
                                PageListModel.pageSize = pageSize;
                                PageListModel.sort = sort;
                                TempData["IconSortRoleUpdate"] = "up";
                                TempData["IconSortRoleName"] = "up";
                                TempData["IconSortRoleCreate"] = "up";
                            break;
                        }
                        break;
                    case "createdate":
                        switch (sort)
                        {
                            case "ASC":
                                roles = service.Sorting("", "", "ASC", page, pageSize);
                                ViewBag.SortPageRole = "ASC";
                                ViewBag.PageSizeRole = pageSize;
                                PageListModel.pageSize = pageSize;
                                PageListModel.sort = sort;
                                TempData["IconSortRoleUpdate"] = "up";
                                TempData["IconSortRoleName"] = "up";
                                TempData["IconSortRoleCreate"] = "up";
                            break;
                            case "DES":
                                roles = service.Sorting("", "", "DES", page, pageSize);
                                ViewBag.SortPageRole = "DES";
                                ViewBag.PageSizeRole = pageSize;
                                PageListModel.pageSize = pageSize;
                                PageListModel.sort = sort;
                                TempData["IconSortRoleUpdate"] = "up";
                                TempData["IconSortRoleName"] = "up";
                                TempData["IconSortRoleCreate"] = "down";
                            break;
                            default:
                                roles = service.Sorting("", "", "ASC", page, pageSize);
                                ViewBag.SortPageRole = "ASC";
                                ViewBag.PageSizeRole = pageSize;
                                PageListModel.pageSize = pageSize;
                                PageListModel.sort = sort;
                                TempData["IconSortRoleUpdate"] = "up";
                                TempData["IconSortRoleName"] = "up";
                                TempData["IconSortRoleCreate"] = "up";
                            break;
                        }
                     break;
                default:
                    break;
            }
            return View(roles);
        }

        [HttpPost]
        public ActionResult Index(string keyword)
        {
            var service = new RoleService();
            var roles = service.Search(keyword, PageListModel.page,PageListModel.pageSize);
            ViewBag.SearchRole = keyword;

            PageListModel.keyword = keyword;
            ShowOption();
            return View(roles);
        }

        public void ShowOption()
        {
           
            var options = SharedData.OptionPageNumber(PageListModel.sort, PageListModel.sortBy,PageListModel.page, PageListModel.pageSize,PageListModel.keyword);
            TempData["showpagesize"] = options;
            TempData["IconSortRoleName"] = "up";
            TempData["IconSortRoleUpdate"] = "up";
            TempData["IconSortRoleCreate"] = "up";
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(tbl_Role _role,RoleModel model)
        {
            var service = new RoleService();
            var rolename = service.FindRoleName(model.RoleName);
            if (model.RoleName == rolename && model.RoleName!= null)
            {
                TempData["CREATEROLE"] = "Tên chức vụ đã tồn tại";
                TempData["ALEART"] = "warning";
            }
            if (model.RoleName != null && !model.RoleName.StartsWith(" ") && model.RoleName!=rolename)
            {
                var role = service.Create(_role);
                if (role != null)
                {
                    TempData["CREATEROLE"] = "Thêm mới quyền người dùng thành công";
                    TempData["ALEART"] = "success";
                  
                }
                else
                {
                    TempData["CREATEROLE"] = "Thêm mới thất bại";
                    TempData["ALEART"] = "danger";

                }
            }
            
           
            return View();
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var service = new RoleService();
            var role = service.GetById(id);
            return View(role);
        }

        [HttpPost]
        public ActionResult Edit(tbl_Role role,RoleModel model)
        {
            var service = new RoleService();
            var rolename = service.FindRoleName(model.RoleName);
            if (model.RoleName == rolename && model.RoleName != null)
            {
                TempData["UPDATEROLE"] = "Tên chức vụ đã tồn tại";
                TempData["ALEART"] = "warning";
            }
            if (model.RoleName!=null && !model.RoleName.StartsWith(" ") && model.RoleName != rolename)
            {
                var result = service.Update(role);
                if (result)
                {
                    TempData["UPDATEROLE"] = "Cập nhập quyền người dùng có ID: " + role.Id + " thành công.";
                    TempData["ALEART"] = "success";
                }
                else
                {
                    TempData["UPDATEROLE"] = "Cập nhập thất bại";
                    TempData["ALEART"] = "danger";
                }
            }

            return View();
        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            var service = new RoleService();
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
    }
}