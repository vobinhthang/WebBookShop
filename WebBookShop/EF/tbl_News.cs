namespace WebBookShop.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_News
    {
        public int Id { get; set; }

        [StringLength(200)]
        public string NewsName { get; set; }

        public string Image { get; set; }

        public bool? Status { get; set; }

        public string Descripton { get; set; }

        public string Detail { get; set; }

        public DateTime? CreateDate { get; set; }

        public DateTime? UpdateDate { get; set; }
    }
}
