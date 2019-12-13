using Model.DAO;
using Model.EF;
using ShoeShop.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShoeShop.Controllers
{
    public class UserController : Controller
    {
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(USER user)
        {
            if (ModelState.IsValid)
            {
                UserDAO userDAO = new UserDAO();

                var encryptedPassword = Encryptor.MD5Hash(user.Password);
                user.Password = encryptedPassword;

                var result = userDAO.InsertUser(user);

                if (result > 0)
                {
                    return Json(new { result = true, data = PartialViewToString("RegisterForm", new USER()) });
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

            return PartialView("RegisterForm", user);
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