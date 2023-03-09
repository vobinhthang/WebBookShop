using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebBookShop.Models
{
    public class ImageProductModel
    {
        public int Id { get; set; }
        public int? ProductId { get; set; }
        public string ProductName { get; set; }
        [Required(ErrorMessage = "Xin mời nhập thông tin ảnh")]
        public string Image { get; set; }
        public bool? Thumbnail { get; set; }
    }
}