using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ManageDress
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Service View",
                url: "dich-vu/{code}",
                defaults: new { controller = "Home", action = "Service", code = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "News View",
                url: "tin-tuc/{code}",
                defaults: new { controller = "Home", action = "News", code = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );           
        }
    }
}
