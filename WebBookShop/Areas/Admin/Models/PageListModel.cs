using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebBookShop.Areas.Admin.Models
{
    public static class PageListModel
    {
        public static int page { get; set; }
        public static int pageSize { get; set; }
        public static string sort { get; set; }
        public static string sortBy { get; set; }
        public static string keyword { get; set; }
        public static DateTime KeywordDate { get; set; }

        public static List<SelectListItem> listItems { get; set; }
    }
}