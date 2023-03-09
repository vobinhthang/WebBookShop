using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebBookShop.Models
{
    public class OrderDetailModel
    {
        public int Id { get; set; }
        public int? OrderId { get; set; }
        public int? ProductId { get; set; }
        public double? Price { get; set; }

        [Required(ErrorMessage = "Xin mời nhập số lượng")]
        public int? Quantity { get; set; }
        public string ProductName { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string CustomerName { get; set; }
        public double? TotalPrice { get; set; }
        public DateTime? OrderDate { get; set; }

    }
}