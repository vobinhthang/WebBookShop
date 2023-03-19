using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebBookShop.Models
{
    public class ProductCommentModel
    {
        public int Id { get; set; }

        public int? UserId { get; set; }

        public string Fullname { get; set; }

        public string Detail { get; set; }

        public bool? Status { get; set; }

        public int? ProductId { get; set; }

        public string ProductName { get; set; }

        public DateTime? CreateDate { get; set; }

        public DateTime? UpdateDate { get; set; }
    }
}