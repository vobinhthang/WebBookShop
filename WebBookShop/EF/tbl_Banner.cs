namespace WebBookShop.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_Banner
    {
        public int Id { get; set; }

        [StringLength(200)]
        public string BannerName { get; set; }

        public string Image { get; set; }

        [StringLength(200)]
        public string Link { get; set; }

        public bool? Status { get; set; }

        public int? Sort { get; set; }
    }
}
