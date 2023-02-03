namespace WebBookShop.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_Role
    {
        public int Id { get; set; }

        [StringLength(200)]
        public string RoleName { get; set; }
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{dd/MM/yyyy hh:mm tt}", ApplyFormatInEditMode = true)]
        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdateDate { get; set; }
    }
}
