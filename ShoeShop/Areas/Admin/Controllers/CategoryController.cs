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
    public class CategoryController : BaseController
	{
		CategoryDAO categoryDAO = new CategoryDAO();

        // GET: Admin/Category
        public ActionResult Category()
        {
			var model = categoryDAO.GetListOfCategories();

            return View(model);
        }

        public ActionResult CategoryDetail(int idCategory)
        {
			var model = categoryDAO.GetCategory(idCategory);

            return View(model);
        }

		[HttpPost]
		public ActionResult InsertCategory(CATEGORY category)
		{
			return Json(new { result = categoryDAO.InsertCategory(category) });
		}

		[HttpPost]
		public ActionResult UpdateCategory(CATEGORY category)
		{
			var model = categoryDAO.UpdateCategory(category);

			if(model == null)
			{
				return Json(new { result = false });
			}

			return Json(new { result = true, data = PartialViewToString("CategoryDetailForm", model) });
		}

		[HttpPost]
		public ActionResult DeleteCategory(int idCategory)
		{
			return Json(new { result = categoryDAO.DeleteCategory(idCategory) });
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