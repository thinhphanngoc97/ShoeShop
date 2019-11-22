using Model.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShoeShop.Controllers
{
    public class ProductController : Controller
    {
        ProductDAO productDAO = new ProductDAO();
        public ActionResult MostDiscountProducts()
        {
            return PartialView(productDAO.GetMostDiscountProducts());
        }

        public ActionResult NewestProducts()
        {
            return PartialView(productDAO.GetNewestProducts());
        }

        public ActionResult ProductDetail(int id, string number_of_stars = null)
        {
            var product = productDAO.GetProductById(id);

            productDAO.GetProductExtraInfo(id);

            ViewBag.ListImage = productDAO.ListImage;
            ViewBag.ListRelatedProducts = productDAO.ListRelatedProducts;
            ViewBag.AverageRate = productDAO.AverageRate;
            ViewBag.TotalRate = productDAO.TotalRate;
            ViewBag.ListCountRate = productDAO.ListCountRate;

            if (!String.IsNullOrEmpty(number_of_stars))
            {
                productDAO.SaveRating(Double.Parse(number_of_stars), id);
            }

            return View(product);
        }
    }
}