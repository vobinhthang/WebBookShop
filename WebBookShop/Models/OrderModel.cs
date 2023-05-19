using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebBookShop.Models
{
    public class OrderModel
    {
        public int Id { get; set; }
        
        public DateTime? OrderDate { get; set; }
        public string Payments { get; set; }
        public bool? Status { get; set; }
        public DateTime? DeliveredDate { get; set; }
        public bool? Delivered { get; set; }
        public int? UserID { get; set; }
        public string CustomerName { get; set; }
        public string UserName { get; set; }
        [Phone(ErrorMessage ="Số điện thoại không hợp lệ")]
        [Required(ErrorMessage ="Xin mời nhập số điện thoại")]
        [StringLength(15,ErrorMessage ="Độ dài số điện thoại phải >=10 và <=15",MinimumLength =10)]
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