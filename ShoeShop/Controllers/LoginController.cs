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
    public class LoginController : Controller
    {
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var userDAO = new UserDAO();
                var result = userDAO.LoginCheck(model.Email, Encryptor.MD5Hash(model.Password));

                if (result == 1)
                {
                    if (model.RememberMe)
                    {
                        //Create cookie
                        Response.Cookies[CommonConstants.USER_COOKIE][CommonConstants.USEREMAIL] = model.Email.ToString();
                        Response.Cookies[CommonConstants.USER_COOKIE][CommonConstants.USERPASSWORD] = Encryptor.MD5Hash(model.Password).ToString();

                        Response.Cookies[CommonConstants.USER_COOKIE].Expires = DateTime.Now.AddDays(30);
                    }

                    //Create session
                    USER user = userDAO.GetByEmail(model.Email);

                    Session.Add(CommonConstants.USER_SESSION, user);

                    // Check if the session cart is null then redirect to Homepage, else redirect to Cart Detail.
                    var sessionCart = (CartModel)Session[Common.CommonConstants.CART_SESSION];
                    if (sessionCart == null)
                    {
                        return Json(new { result = true });
                    }
                    else
                    {
                        return Json(new { result = false });
                    }
                }
                else if (result == -1)
                {
                    ModelState.AddModelError("", "Email không tồn tại, vui lòng thử lại!");
                }
                else if (result == 0)
                {
                    ModelState.AddModelError("", "Mật khẩu không đúng, vui lòng nhập lại!");
                }
                else
                {
                    ModelState.AddModelError("", "Đăng nhập không thành công!");
                }
            }
            return PartialView("LoginForm");
        }

        public ActionResult Logout()
        {
            if (Request.Cookies[CommonConstants.USER_COOKIE] != null)
            {
                Response.Cookies[CommonConstants.USER_COOKIE].Expires = DateTime.Now.AddDays(-1);
            }

            Session[CommonConstants.USER_SESSION] = null;

            return RedirectToAction("Index", "Home");
        }
    }
}