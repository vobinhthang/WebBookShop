using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebBookShop.Models
{
    public class CateModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Xin mời nhập thông tin")]
        public string CategoryName { get; set; }

        [Required(ErrorMessage = "Xin mời nhập thông tin")]
        public bool? Status { get; set; }

        [Required(ErrorMessage = "Xin mời nhập thông tin")]
        public int? Sort { get;set; }
        public int? ParentID { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdateDate { get; set; }
    }
}