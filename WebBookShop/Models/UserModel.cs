using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebBookShop.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Xin mời nhập email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Xin mời nhập mật khẩu")]
        [MinLength(6,ErrorMessage ="Độ dài mật khẩu phải >=6 ký tự")]
        public string Password { get; set; }
        public string Fullname { get;set; }
        [Phone(ErrorMessage = "Số điện thoại không hợp lệ")]
        [Required(ErrorMessage = "Xin mời nhập số điện thoại")]
        [StringLength(15, ErrorMessage = "Độ dài số điện thoại phải >=10 và <=15", MinimumLength = 10)]
        public string Phone { get; set; }
        public string Address { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public int? RoleID { get; set; }

        public string RoleName { get; set; }
    }
}