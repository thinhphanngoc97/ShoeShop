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
    public class DiscountController : BaseController
	{
        DiscountCodeDAO discountDAO = new DiscountCodeDAO();
        // GET: Admin/Discount
        public ActionResult Discount()
        {
            return View(discountDAO.GetAllDiscountCode());
        }
        [HttpGet]
        public ActionResult DiscountDetail(int id)
        {
            return View(discountDAO.GetDiscountCodeById(id));
        }
        [HttpPost]
        public ActionResult EditDiscount(DISCOUNT_CODE discount)
        {
            if (ModelState.IsValid)
            {
                var dao = new DiscountCodeDAO();
                var result = dao.Update(discount);
                if (result > 0)
                {
                    return Json(new { result = true, data = PartialViewToString("DiscountDetailForm", discount) });
                }
                else if(result == 0)
                {
                    ModelState.AddModelError("", "Mã giảm giá đã tồn tại, vui lòng chọn mã giảm giá khác!");
                }
                else
                {
                    ModelState.AddModelError("", "Đã xảy ra lỗi, sửa mã giảm giá không thành công!");
                }
            }
            return PartialView("DiscountDetailForm", discount);
        }

        [HttpPost]
        public ActionResult InsertDiscount(DISCOUNT_CODE discount)
        {
            if (ModelState.IsValid)
            {
                var result = discountDAO.Insert(discount);
                if (result > 0)
                {
                    return Json(new { result = 1 });
                }
                else if (result == 0)
                {
                    ModelState.AddModelError("", "Mã giảm giá đã tồn tại!");
                }
                else
                {
                    ModelState.AddModelError("", "Đã xảy ra lỗi, thêm mã giảm giá không thành công!");
                }
            }
            return PartialView("InsertDiscountForm", discount);
        }
        [HttpPost]
        public ActionResult DeleteDiscount(int id)
        {
            return Json(new { result = discountDAO.DeleteDiscount(id) });
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