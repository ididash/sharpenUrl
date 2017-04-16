using System.Web.Mvc;
using System.Web.Routing;

namespace URLShortener.UI
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "json result",
                url: "api/{controller}/{action}/{id}",
                defaults: new { id = UrlParameter.Optional });

            routes.MapRoute(
                name: "start",
                url: "",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "archive",
                url: "archive",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "default",
                url: "{*url}",
                defaults: new { controller = "Home", action = "ShortenerUrl", id = UrlParameter.Optional }
            );
        }
    }
}
