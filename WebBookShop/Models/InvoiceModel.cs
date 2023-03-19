using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebBookShop.Models
{
    public class InvoiceModel
    {
        public int Id { get; set; }

        public bool? Status { get; set; }

        public double? TotalPrice { get; set; }
        public DateTime? CreateDate { get; set; }

        public DateTime? UpdateDate { get; set; }
    }
}