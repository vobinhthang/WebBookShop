using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebBookShop.Models
{
    public class OrderModel
    {
        public int Id { get; set; }
        
        public DateTime? OrderDate { get; set; }
        public bool? Status { get; set; }
        public DateTime? DeliveredDate { get; set; }
        public bool? Delivered { get; set; }
        public int? UserID { get; set; }
        public string CustomerName { get; set; }
        public string UserName { get; set; }
        public string Phone { get; set; }
        public string UserPhone { get; set; }
        public string Email { get; set; }
        public string UserEmail { get; set; }
        public string Address { get; set; }
        public string UserAddress { get; set; }
        public double? Discount { get; set; }
        public double? TotalPrice { get; set; }

    }
}