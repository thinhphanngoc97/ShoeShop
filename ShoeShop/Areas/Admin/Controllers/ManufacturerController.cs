using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model.DAO;
using Model.EF;
using System.IO;

namespace ShoeShop.Areas.Admin.Controllers
{
    public class ManufacturerController : BaseController
    {
        ManufacturerDAO manufacturer = new ManufacturerDAO();
        // GET: Admin/Manufacture
        public ActionResult Manufacturer()
        {
            return View(manufacturer.GetAllManufacturers());
        }
        [HttpGet]
        public ActionResult ManufacturerDetail(int id)
        {
            return View(manufacturer.GetManufacturerById(id));
        }
        public ActionResult EditManufacturer(MANUFACTURER _manufacturer)
        {
            if (ModelState.IsValid)
            {
                var dao = new ManufacturerDAO();
                var result = dao.Update(_manufacturer);
                if (result)
                {
                    return Json(new { result = true, data = PartialViewToString("ManufacturerDetailForm", _manufacturer) });
                }
                else
                {
                    ModelState.AddModelError("", "Đã xảy ra lỗi, sửa thương hiệu không thành công!");
                }
            }
            return PartialView("ManufacturerDetailForm", _manufacturer);
        }

        [HttpPost]
        public ActionResult AddManufacturer(MANUFACTURER _manufacturer)
        {
            if (ModelState.IsValid)
            {
                var result = manufacturer.Insert(_manufacturer);
                if (result > 0)
                {
                    return Json(new { result = 1 });
                }
                else
                {
                    ModelState.AddModelError("", "Đã xảy ra lỗi, thêm thương hiệu không thành công!");
                }
            }
            return PartialView("InsertManufacturerForm", _manufacturer);

        }
        [HttpPost]
        public ActionResult DeleteManufacturer(int id)
        {
            return Json(new { result = manufacturer.DeleteManufacturer(id) });
        }
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