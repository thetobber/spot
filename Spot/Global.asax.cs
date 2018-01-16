﻿using System.Web;
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
            routes.MapRoute("UserRegister", "Register", new { action = "Register", controller = "User" });
            routes.MapRoute("UserSignIn", "SignIn", new { action = "SignIn", controller = "User" });
            routes.MapRoute("UserSignOut", "SignOut", new { action = "SignOut", controller = "User" });

            // PostController routes
            //routes.MapRoute("PostPagedInitial", "posts", new { action = "Paged", controller = "Post" });
            //routes.MapRoute("PostPaged", "posts/page/{pageIndex}", new { action = "Paged", controller = "Post", pageIndex = UrlParameter.Optional });
            //routes.MapRoute("PostSingle", "posts/{id}", new { action = "Single", controller = "Post" }, new { id = "[0-9]+" });
            //routes.MapRoute("PostNew", "posts/create", new { action = "New", controller = "Post" });
            //routes.MapRoute("PostEdit", "posts/edit/{id}", new { action = "Edit", controller = "Post" }, new { id = "[0-9]+" });
            //routes.MapRoute("PostRemove", "posts/remove/{id}", new { action = "Edit", controller = "Post" }, new { id = "[0-9]+" });

            //routes.MapRoute("PostPaged", "feed/{tag}/{pageIndex}", new { action = "Paged", controller = "Post", pageIndex = UrlParameter.Optional });

            // HomeController routes
            routes.MapRoute("HomeIndex", "", new { action = "Index", controller = "Home" });
            routes.MapRoute("HomeAbout", "About", new { action = "About", controller = "Home" });
            routes.MapRoute("HomeContact", "Contact", new { action = "Contact", controller = "Home" });

            // Default fall through route
            routes.MapRoute("Default", "{*anything}", new { action = "NotFound", controller = "Home" });
        }
    }
}
