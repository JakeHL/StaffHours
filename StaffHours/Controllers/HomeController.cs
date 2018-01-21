using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StaffHours.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            /* Please see description of why this exists in RouteConfig.cs Line 16 */
            return RedirectPermanent("/DIST/index.html");
        }
    }
}
