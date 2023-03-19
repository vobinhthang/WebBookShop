namespace WebBookShop.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_InvoiceDetail
    {
        public int Id { get; set; }

        public int? InvoiceId { get; set; }

        public int? ProductId { get; set; }

        public int? Quantity { get; set; }

        public double? Price { get; set; }
    }
}
