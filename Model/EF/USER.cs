namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("USER")]
    public partial class USER
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public USER()
        {
            INVOICE = new HashSet<INVOICE>();
        }

        public int Id { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập vào tên!")]
        [StringLength(255)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập vào email!")]
        [StringLength(255)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập vào mật khẩu!")]
        [StringLength(255)]
        public string Password { get; set; }

        [StringLength(255)]
        public string Address { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập vào số điện thoại!")]
        [StringLength(20)]
        public string Phone { get; set; }

        public string Avatar { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? Participation_Time { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<INVOICE> INVOICE { get; set; }
    }
}
