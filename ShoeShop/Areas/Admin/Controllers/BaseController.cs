using ShoeShop.Common;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ShoeShop.Areas.Admin.Controllers
{
    public class BaseController : Controller
    {
        // GET: Base
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
			var session = (ADMIN)Session[CommonConstants.ADMIN_SESSION];
			if (session == null)
			{
				filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Login", action = "Login" }));
			}

			base.OnActionExecuting(filterContext);
        }
    }
}