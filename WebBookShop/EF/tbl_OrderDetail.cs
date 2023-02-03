namespace WebBookShop.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_OrderDetail
    {
        public int Id { get; set; }

        public int? OrderID { get; set; }

        public int? ProductID { get; set; }

        public double? Price { get; set; }

        public int? Quantity { get; set; }
    }
}
