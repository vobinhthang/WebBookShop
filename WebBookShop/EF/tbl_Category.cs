namespace WebBookShop.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_Category
    {
        public int Id { get; set; }

        [StringLength(200)]
        public string CategoryName { get; set; }

        public bool? Status { get; set; }

        public int? Sort { get; set; }

        public int? ParentID { get; set; }

        public DateTime? CreateDate { get; set; }

        public DateTime? UpdateDate { get; set; }
    }
}
