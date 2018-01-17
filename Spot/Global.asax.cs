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
            routes.MapMvcAttributeRoutes();

            // UserController routes
            routes.MapRoute("UserRegister", "register", new { action = "Register", controller = "User" });
            routes.MapRoute("UserSignIn", "signin", new { action = "SignIn", controller = "User" });
            routes.MapRoute("UserSignOut", "signout", new { action = "SignOut", controller = "User" });

            // HomeController routes
            routes.MapRoute("HomeIndex", "", new { action = "Index", controller = "Home" });
            routes.MapRoute("HomeAbout", "about", new { action = "About", controller = "Home" });
            routes.MapRoute("HomeContact", "contact", new { action = "Contact", controller = "Home" });

            // Default fall through route
            routes.MapRoute("Default", "{*anything}", new { action = "NotFound", controller = "Home" });
        }
    }
}
