namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PRODUCT")]
    public partial class PRODUCT
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PRODUCT()
        {
            INVOICE_DETAIL = new HashSet<INVOICE_DETAIL>();
            PRODUCT_SIZE = new HashSet<PRODUCT_SIZE>();
            PRODUCT_IMAGE = new HashSet<PRODUCT_IMAGE>();
            RATE = new HashSet<RATE>();
        }

        public int Id { get; set; }

        [StringLength(255)]
        public string Name { get; set; }

        [Column(TypeName = "money")]
        public decimal? Price { get; set; }

        public int? Discount_Amount { get; set; }

        public double? Average_Rate { get; set; }

        public string Image_Thumbnail { get; set; }

        [StringLength(255)]
        public string Status { get; set; }

        [StringLength(255)]
        public string Model_Number { get; set; }

        [StringLength(1255)]
        public string Description { get; set; }

        [StringLength(255)]
        public string Style { get; set; }

        [StringLength(255)]
        public string Material { get; set; }

        [StringLength(255)]
        public string Warranty_Period { get; set; }

        [StringLength(255)]
        public string String_Material { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? Created_Time { get; set; }

        public int? Id_Category { get; set; }

        public int? Id_Manufacturer { get; set; }

        [StringLength(255)]
        public string Metadata { get; set; }

        public virtual CATEGORY CATEGORY { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<INVOICE_DETAIL> INVOICE_DETAIL { get; set; }

        public virtual MANUFACTURER MANUFACTURER { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PRODUCT_IMAGE> PRODUCT_IMAGE { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RATE> RATE { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PRODUCT_SIZE> PRODUCT_SIZE { get; set; }

    }
}
