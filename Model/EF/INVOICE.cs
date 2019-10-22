namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("INVOICE")]
    public partial class INVOICE
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public INVOICE()
        {
            INVOICE_DETAIL = new HashSet<INVOICE_DETAIL>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Customer_Name { get; set; }

        [StringLength(255)]
        public string Customer_Email { get; set; }

        [Required]
        [StringLength(20)]
        public string Customer_Phone { get; set; }

        [Required]
        [StringLength(1255)]
        public string Customer_Address { get; set; }

        [StringLength(1255)]
        public string Customer_Message { get; set; }

        [Column(TypeName = "money")]
        public decimal? Total { get; set; }

        [StringLength(255)]
        public string Payment_Method { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? Created_Time { get; set; }

        public int? Id_User { get; set; }

        public int? Id_Discount_Code { get; set; }

        public int? Status { get; set; }

        public virtual DISCOUNT_CODE DISCOUNT_CODE { get; set; }

        public virtual USER USER { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<INVOICE_DETAIL> INVOICE_DETAIL { get; set; }
    }
}
