using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebBookShop.Models
{
    public class ProductModel
    {
        public int Id { get; set; }
        public int? CategoryId { get; set; }
        public string CategoryName { get; set; }

        [Required(ErrorMessage = "Xin mời nhập thông tin")]
        public string ProductName { get; set; }
        public string Image { get; set; }
        [Required(ErrorMessage = "Xin mời nhập thông tin")]
        public bool? Status { get; set; }
        [Required(ErrorMessage = "Xin mời nhập thông tin")]
        public double? Price { get; set; }
        public int? Quantity { get; set; }
        [Required(ErrorMessage = "Xin mời nhập thông tin")]
        public bool? Hot { get; set; }
        [Required(ErrorMessage = "Xin mời nhập thông tin")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Xin mời nhập thông tin")]
        public string AuthorName { get; set; }
        [Required(ErrorMessage = "Xin mời nhập thông tin")]
        public string PublishCompany { get; set; }
        [Required(ErrorMessage = "Xin mời nhập thông tin")]
        public int? NumberPage { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdateDate { get; set; }
    }
}