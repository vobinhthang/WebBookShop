using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebBookShop.Areas.Admin.Models
{
    public static class SharedData
    {
        public static int? RoleId { get; set; }
        public static int? OptionRoleId { get; set; }

        public static List<SelectListItem> OptionPageNumber(string sort, string sortBy, int page, int pageSize, string keyword)
        {

            List<SelectListItem> options = new List<SelectListItem>();
            for (int i = 5; i <= 20; i += 5)
            {
                if (pageSize == i)
                {
                    options.Add(new SelectListItem { Text = Convert.ToString(i), Value = Convert.ToString("?sort=" + sort + "&sortBy=" + sortBy + "&page=" + page + "&pageSize=" + i + "&keyword=" + keyword), Selected = true });
                }
                else
                {
                    options.Add(new SelectListItem { Text = Convert.ToString(i), Value = Convert.ToString("?sort=" + sort + "&sortBy=" + sortBy + "&page=" + page + "&pageSize=" + i + "&keyword=" + keyword) });
                }
            }

            return options;
        }
    }
}