namespace WebBookShop.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_Order
    {
        public int Id { get; set; }

        public DateTime? OrderDate { get; set; }

        public bool? Status { get; set; }

        public bool? Delivered { get; set; }

        public DateTime? DeliveredDate { get; set; }

        public int? UserID { get; set; }

        [StringLength(200)]
        public string CustomerName { get; set; }

        [StringLength(15)]
        public string Phone { get; set; }

        [StringLength(200)]
        public string Email { get; set; }

        [StringLength(250)]
        public string Address { get; set; }

        public double? Discount { get; set; }

        public double? TotalPrice { get; set; }
    }
}
