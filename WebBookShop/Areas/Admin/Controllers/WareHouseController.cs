using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBookShop.Areas.Admin.Models;
using WebBookShop.Models;
using WebBookShop.Services;

namespace WebBookShop.Areas.Admin.Controllers
{
    public class WareHouseController : BaseController
    {
        // GET: Admin/WareHouse
        public ActionResult Index(string keyword , int page =1,int pageSize = 10)
        {
            var service = new ProductService();
            ViewBag.PageSize = pageSize;
            ViewBag.Page = page;

            PageListModel.pageSize = pageSize;
            PageListModel.page = page;
            //ShowOption();
            IEnumerable<ProductModel> wh;
            if (keyword != null)
            {
                ViewBag.Search = keyword;
                PageListModel.keyword = keyword;

                wh = service.Search(keyword, page, pageSize);
            }
            else
            {

                wh = service.GetAll(page, pageSize);
            }
            return View(wh);
        }
    }
}