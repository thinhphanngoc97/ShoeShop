using ShoeShop.Areas.Admin.Models;
using ShoeShop.Common;
using Model.DAO;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShoeShop.Areas.Admin.Controllers
{
    public class LoginController : Controller
	{
		AdminDAO adminDAO = new AdminDAO();

		// GET: Admin/Login
		[HttpGet]
		public ActionResult Login()
        {
			if (Request.Cookies[CommonConstants.ADMIN_COOKIE] != null)
			{
				var result = adminDAO.LoginCheck(Request.Cookies[CommonConstants.ADMIN_COOKIE][CommonConstants.USERNAME], Request.Cookies[CommonConstants.ADMIN_COOKIE][CommonConstants.ADMINPASSWORD]);

				if (result == 1)
				{
					//Create session
					ADMIN admin = adminDAO.GetAdmin(Request.Cookies[CommonConstants.ADMIN_COOKIE][CommonConstants.USERNAME]);
					Session.Add(CommonConstants.ADMIN_SESSION, admin);

					return RedirectToAction("Invoice", "Invoice");
				}
			}

			return View();
        }

		[HttpPost]
		public ActionResult Login(LoginModel model)
		{
			if (ModelState.IsValid)
			{
				var result = adminDAO.LoginCheck(model.Username, Encryptor.MD5Hash(model.Password));

				if (result == 1)
				{
					if (model.RememberMe)
					{
						//Create cookie
						Response.Cookies[CommonConstants.ADMIN_COOKIE][CommonConstants.USERNAME] = model.Username.ToString();
						Response.Cookies[CommonConstants.ADMIN_COOKIE][CommonConstants.ADMINPASSWORD] = Encryptor.MD5Hash(model.Password).ToString();

						Response.Cookies[CommonConstants.ADMIN_COOKIE].Expires = DateTime.Now.AddDays(30);
					}

					//Create session
					ADMIN admin = adminDAO.GetAdmin(model.Username);

					Session.Add(CommonConstants.ADMIN_SESSION, admin);

					return Json(new { result = true });
				}
				else if (result == -1)
				{
					ModelState.AddModelError("", "Tên đăng nhập không tồn tại, vui lòng thử lại!");
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

			return PartialView("LoginForm", model);
		}

		public ActionResult Logout()
		{
			if (Request.Cookies[CommonConstants.ADMIN_COOKIE] != null)
			{
				Response.Cookies[CommonConstants.ADMIN_COOKIE].Expires = DateTime.Now.AddDays(-1);
			}

			Session[CommonConstants.ADMIN_SESSION] = null;

			return RedirectToAction("Login");
		}
	}
}