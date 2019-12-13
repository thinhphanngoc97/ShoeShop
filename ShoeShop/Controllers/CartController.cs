using Model.DAO;
using Model.EF;
using ShoeShop.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

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

        public JsonResult UpdateCart(string cartModel)
        {
            var jsonCart = new JavaScriptSerializer().Deserialize<List<CartItemModel>>(cartModel);
            var sessionCart = (CartModel)Session[Common.CommonConstants.CART_SESSION];

            foreach (var item in sessionCart.ListCartItem)
            {
                var jsonItem = jsonCart.SingleOrDefault(x => x.Product.Id == item.Product.Id);
                if (jsonItem != null)
                {
                    item.Quantity = jsonItem.Quantity;
                }
            }

            sessionCart.Total = GetCartTotal(sessionCart);
            Session[Common.CommonConstants.CART_SESSION] = sessionCart;

            return Json(new
            {
                status = true
            });
        }

        public JsonResult DeleteItem(int productId)
        {
            var sessionCart = (CartModel)Session[Common.CommonConstants.CART_SESSION];
            sessionCart.ListCartItem.RemoveAll(x => x.Product.Id == productId);
            sessionCart.Total = GetCartTotal(sessionCart);

            if (sessionCart.ListCartItem.Count == 0)
            {
                sessionCart = null;
            }

            Session[Common.CommonConstants.CART_SESSION] = sessionCart;

            return Json(new
            {
                status = true
            });
        }

        [HttpPost]
        public ActionResult ApplyDiscountCode(CartModel cartModel)
        {
            if (ModelState.IsValid)
            {
                var discountCodeDAO = new DiscountCodeDAO();
                var sessionCart = (CartModel)Session[Common.CommonConstants.CART_SESSION];
                var result = discountCodeDAO.GetDiscountCodeByCode(cartModel.DiscountCode);

                if (result != null)
                {
                    sessionCart.DiscountCode = result.Code;
                    sessionCart.DiscountAmount = (int)result.Discount_Amount;
                    Session[Common.CommonConstants.CART_SESSION] = sessionCart;
                }
                else
                {
                    ResetDiscountCode(sessionCart);
                    ModelState.AddModelError("", "Mã giảm giá không đúng!");
                }
            }

            return RedirectToAction("CartDetail");
        }

        public ActionResult DeleteDiscountCode()
        {
            var sessionCart = (CartModel)Session[Common.CommonConstants.CART_SESSION];
            ResetDiscountCode(sessionCart);

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

        // Checkout Area
        [HttpGet]
        public ActionResult Checkout()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Checkout(INVOICE invoice)
        {
            var sessionCart = (CartModel)Session[Common.CommonConstants.CART_SESSION];
            var sessionUser = Session[Common.CommonConstants.USER_SESSION];

            invoice.Created_Time = DateTime.Now;
            invoice.Status = 0;
            invoice.Total = sessionCart.Total * (100 - sessionCart.DiscountAmount) / 100;

            if (sessionCart.DiscountCode != null)
            {
                invoice.Id_Discount_Code = new DiscountCodeDAO().GetDiscountCodeByCode(sessionCart.DiscountCode).Id;
            }

            if (sessionUser != null)
            {
                var user = (USER)sessionUser;
                invoice.Id_User = user.Id;
            }

            if (ModelState.IsValid)
            {
                var invoiceId = new InvoiceDAO().Insert(invoice);

                if (invoiceId > 0)
                {
                    var detailDAO = new InvoiceDetailDAO();

                    foreach (var item in sessionCart.ListCartItem)
                    {
                        var invoiceDetail = new INVOICE_DETAIL
                        {
                            Id_Invoice = invoiceId,
                            Id_Product = item.Product.Id,
                            Quantity = item.Quantity
                        };

                        detailDAO.Insert(invoiceDetail);
                    }

                    Session[Common.CommonConstants.CART_SESSION] = null;

                    return Json(new { result = true });
                }
            }

            return PartialView("CheckoutForm", invoice);
        }

        private string PartialViewToString(string viewName, object model)
        {
            ViewData.Model = model;
            using (StringWriter writer = new StringWriter())
            {
                ViewEngineResult vResult = ViewEngines.Engines.FindPartialView(ControllerContext, viewName);
                ViewContext vContext = new ViewContext(this.ControllerContext, vResult.View, ViewData, new TempDataDictionary(), writer);
                vResult.View.Render(vContext, writer);

                return writer.ToString();
            }
        }
    }
}