using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoeShop.Models
{
    public class CartItemModel
    {
        public PRODUCT Product { get; set; }
        public int Quantity { get; set; }
    }
}