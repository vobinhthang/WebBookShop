namespace WebBookShop.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_Product
    {
        public int Id { get; set; }

        public int? CategoryID { get; set; }

        [StringLength(200)]
        public string ProductName { get; set; }

        public double? Price { get; set; }

        public bool? Status { get; set; }

        public int? Quantity { get; set; }

        public bool? Hot { get; set; }

        public string Description { get; set; }

        [StringLength(200)]
        public string AuthorName { get; set; }

        [StringLength(200)]
        public string PublishCompany { get; set; }

        public int? NumberPage { get; set; }

        public DateTime? CreateDate { get; set; }

        public DateTime? UpdateDate { get; set; }
    }
}
