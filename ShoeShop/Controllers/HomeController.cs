using Model.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShoeShop.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.CategoryList = new CategoryDAO().GetListOfCategories();

            return View();
        }

        [ChildActionOnly]
        public PartialViewResult HeaderCategory()
        {
            var listCategory = new CategoryDAO().GetListOfCategories();
            return PartialView(listCategory);
        }

        public ActionResult About()
        {
            return View();
        }
    }
}