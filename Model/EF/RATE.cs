namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("RATE")]
    public partial class RATE
    {
        public int Id { get; set; }

        public double? Number_Of_Stars { get; set; }

        public int? Id_Product { get; set; }

        public virtual PRODUCT PRODUCT { get; set; }
    }
}
