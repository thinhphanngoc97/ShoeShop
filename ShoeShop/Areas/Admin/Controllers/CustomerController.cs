using ShoeShop.Common;
using Model.DAO;
using Model.EF;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShoeShop.Areas.Admin.Controllers
{
    public class CustomerController : BaseController
	{
		UserDAO userDAO = new UserDAO();

        // GET: Admin/Customer
        public ActionResult Customer()
        {
			var model = userDAO.GetListUser();

            return View(model);
        }

        public ActionResult CustomerDetail(int idCustomer)
        {
			var model = userDAO.GetUser(idCustomer);

			return View(model);
		}

		[HttpPost]
		public ActionResult InsertCustomer(USER user)
		{
			if (ModelState.IsValid)
			{
				var encryptedPassword = Encryptor.MD5Hash(user.Password);
				user.Password = encryptedPassword;

				var result = userDAO.InsertUser(user);

				if (result > 0)
				{
					return Json(new { result = true });
				}
				else if (result == 0)
				{
					ModelState.AddModelError("", "Email đã tồn tại, vui lòng chọn email khác!");
				}
				else
				{
					ModelState.AddModelError("", "Đã xảy ra lỗi, thêm tài khoản không thành công!");
				}
			}

			return PartialView("InsertCustomerForm", user);
		}

		[HttpPost]
		public ActionResult UpdateCustomer(USER user)
		{
			if (ModelState.IsValid)
			{
				var entity = userDAO.GetUser(user.Id);
				if (user.Password != entity.Password)
				{
					user.Password = Encryptor.MD5Hash(user.Password);
				}
				var result = userDAO.UpdateUser(user);

				if (result > 0)
				{
					return Json(new { result = true, data = PartialViewToString("CustomerDetailForm", user) });
				}
				else if (result == 0)
				{
					ModelState.AddModelError("", "Email đã tồn tại, vui lòng chọn email khác!");
				}
				else
				{
					ModelState.AddModelError("", "Đã xảy ra lỗi, sửa tài khoản không thành công!");
				}
			}

			return PartialView("CustomerDetailForm", user);
		}

		[HttpPost]
		public ActionResult DeleteCustomer(int idCustomer)
		{
			return Json(new { result = userDAO.DeleteUser(idCustomer) });
		}

		//Render PartialView as Json
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