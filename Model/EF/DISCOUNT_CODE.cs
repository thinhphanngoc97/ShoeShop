namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class DISCOUNT_CODE
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DISCOUNT_CODE()
        {
            INVOICE = new HashSet<INVOICE>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Code { get; set; }

        public int Discount_Amount { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime Start_Date { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime End_Date { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<INVOICE> INVOICE { get; set; }
    }
}
