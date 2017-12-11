using System.Web.Mvc;
using System.Web.Routing;

namespace Spot
{
    public class RouteRegistration
    {
        public static void Register(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute("Auth", "Auth/{action}", new { action = "SignIn", controller = "Auth" });
            routes.MapRoute("Home", "{action}", new { action = "Index", controller = "Home" });
        }
    }
}
