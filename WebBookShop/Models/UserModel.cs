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
        public string Password { get; set; }
        public string Fullname { get;set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy hh:mm tt}", ApplyFormatInEditMode = true)]
        public DateTime? CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public int? RoleID { get; set; }

        public string RoleName { get; set; }
    }
}