namespace WebBookShop.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_ImageProduct
    {
        public int Id { get; set; }

        public int? ProductId { get; set; }

        public string Image { get; set; }

        public bool? Thumbnail { get; set; }
    }
}
