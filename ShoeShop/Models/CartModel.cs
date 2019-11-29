using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoeShop.Models
{
    [Serializable]
    public class CartModel
    {
        public List<CartItemModel> ListCartItem { get; set; }
        public string DiscountCode { get; set; }
        public int DiscountAmount { get; set; }
        public int Total { get; set; }

        public CartModel()
        {
            ListCartItem = new List<CartItemModel>();
        }
    }
}