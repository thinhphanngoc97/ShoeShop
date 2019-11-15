using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model.DAO;
using ShoeShop.Models;

namespace ShoeShop.Controllers
{
    public class CategoryController : Controller
    {
        private CategoryDAO categoryDAO = new CategoryDAO();

        public ActionResult ProductList(int id, int? minimumPrice, int? maximumPrice, string brand = null, string style = null, int page = 1)
        {
            string name = categoryDAO.GetCategory(id).Name;
            string metadata = categoryDAO.GetCategory(id).Metadata;
            var model = new CategoryModel
            {
                Id = id,
                Name = name,
                Metadata = metadata,
                ManufacturerList = categoryDAO.GetListOfManufacturerNamesAndQuantity(),
                StyleList = categoryDAO.GetListOfStylesAndQuantity(),
                ProductList = categoryDAO.GetProductList(id, minimumPrice, maximumPrice, brand, style, page, 9),
                Brand = brand,
                Style = style,
                MinimumPrice = minimumPrice,
                MaximumPrice = maximumPrice
            };

            if (model.ProductList.Count == 0)
            {
                ViewBag.CategoryResultOverview = "Không tìm thấy sản phẩm nào!";
            }

            return View(model);
        }
    }
}