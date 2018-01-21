using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace StaffHours
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            /* 
             * This route is to make launching the page easier during dev.
             * As we're using MVC Web API for that data, nothing in /DIST/ has a dependency on
             * .Net or MVC, it could be served from any website providing the api was accessible.
             * This also means we don't need Controllers/HomeController.cs if we were to go into production
             */
            routes.MapRoute(
                name: "QuickLaunch",
                url: "",
                defaults: new { controller = "Home", action = "Index" }
            );

        }
    }
}
