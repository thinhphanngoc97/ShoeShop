using Model.DAO;
using ShoeShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShoeShop.Controllers
{
    public class CartController : Controller
    {
        // Cart Area
        public ActionResult CartDetail()
        {
            var sesionCart = Session[Common.CommonConstants.CART_SESSION];
            var cartModel = new CartModel();

            if (sesionCart != null)
            {
                cartModel = (CartModel)sesionCart;
            }

            return View(cartModel);
        }

        public ActionResult AddItem(int productID, int quantity)
        {
            var product = new ProductDAO().GetProductById(productID);
            var cart = Session[Common.CommonConstants.CART_SESSION];

            if (cart != null)
            {
                var cartModel = (CartModel)cart;

                if (cartModel.ListCartItem.Exists(x => x.Product.Id == productID))
                {
                    foreach (var item in cartModel.ListCartItem)
                    {
                        if (item.Product.Id == productID)
                        {
                            item.Quantity += quantity;
                        }
                    }
                    cartModel.Total = GetCartTotal(cartModel);
                }
                else
                {
                    var item = new CartItemModel();
                    item.Product = product;
                    item.Quantity = quantity;

                    cartModel.ListCartItem.Add(item);
                    cartModel.Total = GetCartTotal(cartModel);

                    Session[Common.CommonConstants.CART_SESSION] = cartModel;
                }
            }
            else
            {
                var item = new CartItemModel();
                item.Product = product;
                item.Quantity = quantity;

                var cartModel = new CartModel();
                cartModel.ListCartItem.Add(item);
                cartModel.Total = GetCartTotal(cartModel);

                Session[Common.CommonConstants.CART_SESSION] = cartModel;
            }

            return RedirectToAction("CartDetail");
        }

        // Supportive  methods
        private int GetCartTotal(CartModel cartModel)
        {
            return (int)cartModel.ListCartItem.Sum(x => x.Quantity * (x.Product.Discount_Amount > 0 ? x.Product.Price * (100 - x.Product.Discount_Amount) / 100 : x.Product.Price));
        }

        private void ResetDiscountCode(CartModel cartModel)
        {
            cartModel.DiscountCode = null;
            cartModel.DiscountAmount = 0;
        }
    }
}