using System.Web.Mvc;
using System.Web.Routing;

namespace Spot
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            //routes.MapMvcAttributeRoutes();

            routes.MapRoute("Auth", "Auth/{action}", new { action = "SignIn", controller = "Auth" });
            routes.MapRoute("Home", "{action}", new { action = "Index", controller = "Home" });
        }
    }
}
