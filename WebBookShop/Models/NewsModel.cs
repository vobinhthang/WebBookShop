using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebBookShop.Models
{
    public class NewsModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Xin mời nhập thông tin")]
        public string NewsName { get; set; }
        [Required(ErrorMessage = "Xin mời chọn ảnh")]
        public string Image { get; set; }
        [Required(ErrorMessage = "Xin mời nhập thông tin")]
        public bool? Status { get; set; }
        [Required(ErrorMessage = "Xin mời nhập thông tin")]
        public string Descripton { get; set; }
        [Required(ErrorMessage = "Xin mời nhập thông tin")]
        public string Detail { get; set; }

        public DateTime? CreateDate { get; set; }

        public DateTime? UpdateDate { get; set; }
    }
}