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
    }
}