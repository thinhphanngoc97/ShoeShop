using Model.DAO;
using Model.EF;
using ShoeShop.Common;
using ShoeShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShoeShop.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.CategoryList = new CategoryDAO().GetListOfCategories();

            if (Request.Cookies[CommonConstants.USER_COOKIE] != null && Session[CommonConstants.USER_SESSION] == null)
            {
                var userDAO = new UserDAO();
                var result = userDAO.LoginCheck(Request.Cookies[CommonConstants.USER_COOKIE][CommonConstants.USEREMAIL], Request.Cookies[CommonConstants.USER_COOKIE][CommonConstants.USERPASSWORD]);

                if (result == 1)
                {
                    //Create session
                    USER user = userDAO.GetByEmail(Request.Cookies[CommonConstants.USER_COOKIE][CommonConstants.USEREMAIL]);
                    Session.Add(CommonConstants.USER_SESSION, user);
                }
            }

            return View();
        }

        [ChildActionOnly]
        public PartialViewResult HeaderCart()
        {
            var cart = Session[Common.CommonConstants.CART_SESSION];
            var cartModel = new CartModel();

            if (cart != null)
            {
                cartModel = (CartModel)cart;
            }

            return PartialView(cartModel);
        }

        [ChildActionOnly]
        public PartialViewResult HeaderCategory()
        {
            var listCategory = new CategoryDAO().GetListOfCategories();
            return PartialView(listCategory);
        }

        [ChildActionOnly]
        public PartialViewResult HeaderUser()
        {
            var sessionUser = (USER)Session[Common.CommonConstants.USER_SESSION];
            return PartialView(sessionUser);
        }

        public ActionResult About()
        {
            return View();
        }
    }
}