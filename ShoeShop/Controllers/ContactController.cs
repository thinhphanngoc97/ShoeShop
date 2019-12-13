using Model.DAO;
using Model.EF;
using ShoeShop.Common;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShoeShop.Controllers
{
    public class ContactController : Controller
    {
        [HttpGet]
        public ActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Contact(MESSAGE contact)
        {
            var sessionUser = Session[Common.CommonConstants.USER_SESSION];

            if (ModelState.IsValid)
            {
                string content = System.IO.File.ReadAllText(Server.MapPath("~/Views/Home/MessageEmail.html"));

                content = content.Replace("{{CustomerName}}", contact.Name);
                content = content.Replace("{{Email}}", contact.Email);
                content = content.Replace("{{Title}}", contact.Title);
                content = content.Replace("{{Message}}", contact.Message_Content);

                //var toEmail = ConfigurationManager.AppSettings["ToEmailAddress"].ToString();

                //new MailHelper().SendMail(toEmail, contact.Title, content);

                ContactDAO contactDAO = new ContactDAO();

                var result = contactDAO.Insert(contact);

                if (result > 0)
                {
                    return Json(new { result = true, data = PartialViewToString("ContactForm", new MESSAGE()) });
                }
                else
                {
                    ModelState.AddModelError("", "Đã xảy ra lỗi, gửi không thành công!");
                }
            }
            return PartialView("ContactForm", contact);
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