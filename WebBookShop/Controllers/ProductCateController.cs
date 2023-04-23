using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBookShop.Commons;
using WebBookShop.Models;
using WebBookShop.Services;

namespace WebBookShop.Controllers
{
    public class ProductCateController : Controller
    {
        // GET: ProductCate
        public ActionResult Index(int id,string sort,int page=1, int pageSize=20)
        {
            IEnumerable<ProductModel> products;
            var childCates = new CateService().GetCateChildId(id);
            var parentCates = new CateService().GetCateParentId(id);
            var catename = new CateService().GetById(id);

            

            ViewBag.CateName = catename.CategoryName;

            ViewBag.ChildIdCates = childCates;
            ViewBag.CountCate = childCates.Count;

            ViewBag.ParentIdCatesName = parentCates.CategoryName;
            ViewBag.ParentIdCates = parentCates.Id;

            TempData["CateId"] = id;

            ViewBag.Sort = sort;
            switch (sort)
            {
                case "default":
                    products = new ProductService().GetByCate(id, "default",page,pageSize);
                    TempData["COLOR_DF"] = "#0ABAB5";
                    break;
                case "asc":
                    products = new ProductService().GetByCate(id, "asc", page, pageSize);
                    TempData["COLOR_ASC"] = "#0ABAB5";
                    break;
                case "des":
                    products = new ProductService().GetByCate(id, "des", page, pageSize);
                    TempData["COLOR_DES"] = "#0ABAB5";
                    break;
                default:
                    products = new ProductService().GetByCate(id, "", page, pageSize);
                    TempData["COLOR_DF"] = "#0ABAB5";
                    break;

            }
    
            return View(products);
        }


        public ActionResult Search(string keyword,string sort,int page=1,int pageSize=20)
        {
           
            var service = new ProductService();
            ViewBag.Sort = sort;
            @ViewBag.Keyword = keyword;
            IEnumerable<ProductModel> products;
           
            switch (sort)
            {
                case "default":
                    products = new ProductService().GetByName(keyword, "default", page, pageSize);
                    TempData["COLOR_DF"] = "#0ABAB5";
                    break;
                case "asc":
                    products = new ProductService().GetByName(keyword, "asc", page, pageSize);
                    TempData["COLOR_ASC"] = "#0ABAB5";
                    break;
                case "des":
                    products = new ProductService().GetByName(keyword, "des", page, pageSize);
                    TempData["COLOR_DES"] = "#0ABAB5";
                    break;
                default:
                    products = new ProductService().GetByName(keyword, "", page, pageSize);
                    TempData["COLOR_DF"] = "#0ABAB5";
                    break;

            }

            //SharedData.SearchProduct = products;
            return View(products);
        }
    }
}