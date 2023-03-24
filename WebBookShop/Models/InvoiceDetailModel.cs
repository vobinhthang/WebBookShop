using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebBookShop.Models
{
    public class InvoiceDetailModel
    {
        public int Id { get; set; }

        public int? InvoiceId { get; set; }

        public int? ProductId { get; set; }
        public string Productname { get; set; }

        public DateTime? CreateDate { get; set; }
        [Required(ErrorMessage = "Xin mời nhập số lượng")]
        public int? Quantity { get; set; }
        [Required(ErrorMessage = "Xin mời nhập giá nhập")]

        public double? Price { get; set; }
    }
}