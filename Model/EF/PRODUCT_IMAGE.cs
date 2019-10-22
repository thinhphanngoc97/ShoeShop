namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class PRODUCT_IMAGE
    {
        public int Id { get; set; }

        public string Data { get; set; }

        public int? Id_Product { get; set; }

        public virtual PRODUCT PRODUCT { get; set; }
    }
}
