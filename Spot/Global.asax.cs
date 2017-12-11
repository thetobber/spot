using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Spot
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RegisterFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
            MvcHandler.DisableMvcResponseHeader = true;
        }

        private void RegisterFilters(GlobalFilterCollection filters)
        {
            //filters.Add(new RequireHttpsAttribute());
            filters.Add(new HandleErrorAttribute());
            filters.Add(new AuthorizeAttribute());
        }

        private void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute("Auth", "Auth/{action}", new { action = "SignIn", controller = "Auth" });
            routes.MapRoute("Home", "{action}", new { action = "Index", controller = "Home" });
        }
    }
}
