using GuitarShop.Common;
using Model.DAO;
using Model.EF;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GuitarShop.Areas.Admin.Controllers
{
    public class AdminController : BaseController
	{
		AdminDAO adminDAO = new AdminDAO();

        // GET: Admin/Admin
        public ActionResult Admin()
        {
			var model = adminDAO.GetListAdmin();

			return View(model);
		}

        public ActionResult AdminDetail(int idAdmin)
        {
			var model = adminDAO.GetAdmin(idAdmin);

			return View(model);
		}

		[ChildActionOnly]
		public PartialViewResult HeaderAdmin()
		{
			var sessionAdmin = (ADMIN)Session[CommonConstants.ADMIN_SESSION];
			var admin = adminDAO.GetAdmin(sessionAdmin.Id);

			return PartialView(admin);
		}

		[HttpPost]
		public ActionResult InsertAdmin(ADMIN admin)
		{
			if (ModelState.IsValid)
			{
				var encryptedPassword = Encryptor.MD5Hash(admin.Password);
				admin.Password = encryptedPassword;

				var result = adminDAO.InsertAdmin(admin);

				if (result > 0)
				{
					return Json(new { result = true });
				}
				else if (result == 0)
				{
					ModelState.AddModelError("", "Tên đăng nhập đã tồn tại, vui lòng chọn tên đăng nhập khác!");
				}
				else
				{
					ModelState.AddModelError("", "Đã xảy ra lỗi, thêm tài khoản không thành công!");
				}
			}

			return PartialView("InsertAdminForm", admin);
		}

		[HttpPost]
		public ActionResult UpdateAdmin(ADMIN admin)
		{
			if (ModelState.IsValid)
			{
				var entity = adminDAO.GetAdmin(admin.Id);
				if (entity.Password != admin.Password)
				{
					admin.Password = Encryptor.MD5Hash(admin.Password);
				}
				var result = adminDAO.UpdateAdmin(admin);

				if (result > 0)
				{
					return Json(new { result = true, data = PartialViewToString("AdminDetailForm", admin) });
				}
				else if (result == 0)
				{
					ModelState.AddModelError("", "Tên đăng nhập đã tồn tại, vui lòng chọn tên đăng nhập khác!");
				}
				else
				{
					ModelState.AddModelError("", "Đã xảy ra lỗi, sửa tài khoản không thành công!");
				}
			}

			return PartialView("AdminDetailForm", admin);
		}

		[HttpPost]
		public ActionResult DeleteAdmin(int idAdmin)
		{
			return Json(new { result = adminDAO.DeleteAdmin(idAdmin, (ADMIN)Session[CommonConstants.ADMIN_SESSION]) });
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