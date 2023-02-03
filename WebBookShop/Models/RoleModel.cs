using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace WebBookShop.Models
{
    public class RoleModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="Xin mời nhập thông tin")]
        public string RoleName { get; set; }
        //[DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy hh:mm tt}", ApplyFormatInEditMode = true)]
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdateDate { get; set; }
    }
}