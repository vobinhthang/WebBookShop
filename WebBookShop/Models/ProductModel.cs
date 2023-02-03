using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebBookShop.Models
{
    public class ProductModel
    {
        public int Id { get; set; }
        public int? CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string ProductName { get; set; }
        public bool? Status { get; set; }
        public string Image { get; set; }
        public string ListImage { get; set; }
        public double? Price { get; set; }
        public int? Quantity { get; set; }
        public bool? Hot { get; set; }
        public string Description { get; set; }
        public string AuthorName { get; set; }
        public string PublishingCompany { get; set; }
        public int? NumberPage { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdateDate { get; set; }
    }
}