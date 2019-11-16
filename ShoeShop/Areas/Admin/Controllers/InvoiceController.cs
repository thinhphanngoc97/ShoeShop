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
    public class InvoiceController : BaseController
	{
		InvoiceDAO invoiceDAO = new InvoiceDAO();

        // GET: Admin/Invoice
        public ActionResult Invoice()
        {
			return View();
        }

        public ActionResult InvoiceDetail(int idInvoice)
        {
			INVOICE model = invoiceDAO.GetInvoice(idInvoice);

			return View(model);
        }

		public ActionResult UpdateInvoiceStatus(int id)
		{
			INVOICE invoice = invoiceDAO.updateInvoiceStatus(id);

			return Json(new { Id = invoice.Id });
		}

		public ActionResult LoadListInvoice(int idProperty = 1, int idTime = 3)
		{
			List<INVOICE> model = invoiceDAO.GetListInvoice(idProperty, idTime);

			return Json(new { result = true, data = PartialViewToString("~/Areas/Admin/Views/Invoice/ListInvoiceTable.cshtml", model) });
		}

		[HttpPost]
		public ActionResult UpdateInvoice(INVOICE invoice, FormCollection f)
		{
			invoice.Customer_Message = f["txtMessage"].ToString();
			//If checked, f["checkStatus"] = true,false
			bool checkStatus = Convert.ToBoolean(f["checkStatus"].Split(',')[0]);
			invoice.Status = checkStatus ? 1 : 0;
			invoice.Payment_Method = f["selectMethod"].ToString();
			INVOICE model = invoiceDAO.Update(invoice);

			if (model == null)
			{
				return Json(new { result = false });
			}

			return Json(new { result = true, data = PartialViewToString("InvoiceDetailForm", model) });
		}

		public ActionResult NumberInvoice()
		{
			ViewBag.numberInvoice = invoiceDAO.GetNumberInvoice();
			return PartialView();
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