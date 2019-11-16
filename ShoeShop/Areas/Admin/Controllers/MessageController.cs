using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model.DAO;

namespace ShoeShop.Areas.Admin.Controllers
{
    public class MessageController : BaseController
	{
        MessageDAO message = new MessageDAO();
        // GET: Admin/Message
        public ActionResult Message()
        {
            return View(message.GetAllMessage());
        }
        [HttpGet]
        public ActionResult MessageDetail(int id)
        {
            return View(message.GetMessageId(id));
        }
        [HttpPost]
        public ActionResult DeleteMessage(int id)
        {
            return Json(new { result = message.DeleteMessage(id) });
        }
    }
}