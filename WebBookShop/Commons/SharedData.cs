using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebBookShop.Models;

namespace WebBookShop.Commons
{
    public static class SharedData
    {
        public static string Email { get; set; }
        public static string Password { get; set; }
        public static string Keyword { get; set; }
        public static int OrderId { get; set; }
        public static CustomerAddress customerAddress { get; set; }
        public static List<ProductModel> SearchProduct { get; set; }
    }
}