namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("MESSAGE")]
    public partial class MESSAGE
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập vào tên!")]
        [StringLength(50)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập vào email!")]
        [StringLength(50)]
        public string Email { get; set; }

        [StringLength(255)]
        public string Title { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập vào lời nhắn!")]
        [StringLength(1000)]
        public string Message_Content { get; set; }
    }
}
