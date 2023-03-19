using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace WebBookShop.EF
{
    public partial class MyDbContext : DbContext
    {
        public MyDbContext()
            : base("name=MyDbContext")
        {
        }

        public virtual DbSet<tbl_Banner> tbl_Banner { get; set; }
        public virtual DbSet<tbl_Category> tbl_Category { get; set; }
        public virtual DbSet<tbl_Feedback> tbl_Feedback { get; set; }
        public virtual DbSet<tbl_ImageProduct> tbl_ImageProduct { get; set; }
        public virtual DbSet<tbl_Invoice> tbl_Invoice { get; set; }
        public virtual DbSet<tbl_InvoiceDetail> tbl_InvoiceDetail { get; set; }
        public virtual DbSet<tbl_News> tbl_News { get; set; }
        public virtual DbSet<tbl_Order> tbl_Order { get; set; }
        public virtual DbSet<tbl_OrderDetail> tbl_OrderDetail { get; set; }
        public virtual DbSet<tbl_Product> tbl_Product { get; set; }
        public virtual DbSet<tbl_ProductComment> tbl_ProductComment { get; set; }
        public virtual DbSet<tbl_Role> tbl_Role { get; set; }
        public virtual DbSet<tbl_User> tbl_User { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<tbl_Feedback>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Feedback>()
                .Property(e => e.Phone)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Order>()
                .Property(e => e.Phone)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_User>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_User>()
                .Property(e => e.Password)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_User>()
                .Property(e => e.Phone)
                .IsUnicode(false);
        }
    }
}
