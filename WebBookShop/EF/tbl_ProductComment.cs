namespace WebBookShop.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_ProductComment
    {
        public int Id { get; set; }

        public int? UserId { get; set; }

        public string Detail { get; set; }

        public bool? Status { get; set; }

        public int? ProductId { get; set; }

        public DateTime? CreateDate { get; set; }

        public DateTime? UpdateDate { get; set; }
    }
}
