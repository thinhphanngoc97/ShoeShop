namespace Model.EF
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class ShoeShopDbContext : DbContext
    {
        public ShoeShopDbContext()
            : base("name=ShoeShopDbContext")
        {
        }

        public virtual DbSet<ADMIN> ADMIN { get; set; }
        public virtual DbSet<CATEGORY> CATEGORY { get; set; }
        public virtual DbSet<DISCOUNT_CODE> DISCOUNT_CODE { get; set; }
        public virtual DbSet<INVOICE> INVOICE { get; set; }
        public virtual DbSet<INVOICE_DETAIL> INVOICE_DETAIL { get; set; }
        public virtual DbSet<MANUFACTURER> MANUFACTURER { get; set; }
        public virtual DbSet<MESSAGE> MESSAGE { get; set; }
        public virtual DbSet<PRODUCT> PRODUCT { get; set; }
        public virtual DbSet<PRODUCT_IMAGE> PRODUCT_IMAGE { get; set; }
        public virtual DbSet<RATE> RATE { get; set; }
        public virtual DbSet<USER> USER { get; set; }
        public virtual DbSet<SIZE> SIZE { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ADMIN>()
                .Property(e => e.Username)
                .IsUnicode(false);

            modelBuilder.Entity<ADMIN>()
                .Property(e => e.Password)
                .IsUnicode(false);

            modelBuilder.Entity<ADMIN>()
                .Property(e => e.Avatar)
                .IsUnicode(false);

            modelBuilder.Entity<CATEGORY>()
                .HasMany(e => e.PRODUCT)
                .WithOptional(e => e.CATEGORY)
                .HasForeignKey(e => e.Id_Category);

            modelBuilder.Entity<DISCOUNT_CODE>()
                .Property(e => e.Code)
                .IsUnicode(false);

            modelBuilder.Entity<DISCOUNT_CODE>()
                .HasMany(e => e.INVOICE)
                .WithOptional(e => e.DISCOUNT_CODE)
                .HasForeignKey(e => e.Id_Discount_Code);

            modelBuilder.Entity<INVOICE>()
                .Property(e => e.Customer_Email)
                .IsUnicode(false);

            modelBuilder.Entity<INVOICE>()
                .Property(e => e.Customer_Phone)
                .IsUnicode(false);

            modelBuilder.Entity<INVOICE>()
                .Property(e => e.Total)
                .HasPrecision(19, 4);

            modelBuilder.Entity<INVOICE>()
                .HasMany(e => e.INVOICE_DETAIL)
                .WithRequired(e => e.INVOICE)
                .HasForeignKey(e => e.Id_Invoice)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<MANUFACTURER>()
                .HasMany(e => e.PRODUCT)
                .WithOptional(e => e.MANUFACTURER)
                .HasForeignKey(e => e.Id_Manufacturer);

            modelBuilder.Entity<PRODUCT>()
                .Property(e => e.Price)
                .HasPrecision(19, 4);

            modelBuilder.Entity<PRODUCT>()
                .Property(e => e.Image_Thumbnail)
                .IsUnicode(false);

            modelBuilder.Entity<PRODUCT>()
                .Property(e => e.Model_Number)
                .IsUnicode(false);

            modelBuilder.Entity<PRODUCT>()
                .HasMany(e => e.INVOICE_DETAIL)
                .WithRequired(e => e.PRODUCT)
                .HasForeignKey(e => e.Id_Product)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PRODUCT>()
                .HasMany(e => e.PRODUCT_IMAGE)
                .WithOptional(e => e.PRODUCT)
                .HasForeignKey(e => e.Id_Product);

            modelBuilder.Entity<PRODUCT>()
                .HasMany(e => e.RATE)
                .WithOptional(e => e.PRODUCT)
                .HasForeignKey(e => e.Id_Product);

            modelBuilder.Entity<PRODUCT_IMAGE>()
                .Property(e => e.Data)
                .IsUnicode(false);

            modelBuilder.Entity<USER>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<USER>()
                .Property(e => e.Password)
                .IsUnicode(false);

            modelBuilder.Entity<USER>()
                .Property(e => e.Phone)
                .IsUnicode(false);

            modelBuilder.Entity<USER>()
                .Property(e => e.Avatar)
                .IsUnicode(false);

            modelBuilder.Entity<USER>()
                .HasMany(e => e.INVOICE)
                .WithOptional(e => e.USER)
                .HasForeignKey(e => e.Id_User);
        }
    }
}
