﻿using System.Reflection;
using System.Web;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.SignalR;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.SignalR;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.DataProtection;
using Owin;
using Spot.Models.User;
using Spot.Repositories;

[assembly: OwinStartup(typeof(Spot.Startup))]
namespace Spot
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var builder = new ContainerBuilder();
            // var config = new HubConfiguration();

            // Register SignalR hubs
            // builder.RegisterHubs(Assembly.GetExecutingAssembly());

            // Register the database context as a dependency
            builder
                .RegisterType<DatabaseContext>()
                .AsSelf()
                .InstancePerRequest();

            // Register dependencies for Identity
            builder
                .Register(c => HttpContext.Current.GetOwinContext().Authentication)
                .InstancePerRequest();

            builder
                .Register(c => app.GetDataProtectionProvider())
                .InstancePerRequest();

            builder
                .RegisterType<RoleStore>()
                .AsSelf()
                .InstancePerRequest();

            builder
                .RegisterType<RoleManager>()
                .AsSelf()
                .InstancePerRequest();

            builder
                .RegisterType<UserStore>()
                .As<IUserStore<UserModel>>()
                .InstancePerRequest();

            builder
                .RegisterType<UserManager>()
                .AsSelf()
                .InstancePerRequest();

            builder
                .RegisterType<SignInManager>()
                .AsSelf()
                .InstancePerRequest();

            // Register repositories as dependencies
            builder
                .RegisterType<PostRepository>()
                .As<IPostRepository>()
                .InstancePerRequest();

            // Register MVC controllers making dependencies injectable
            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            var container = builder.Build();

            // config.Resolver = new Autofac.Integration.SignalR.AutofacDependencyResolver(container);

            DependencyResolver
                .SetResolver(new Autofac.Integration.Mvc.AutofacDependencyResolver(container));

            // Register Autofac with OWIN
            app.UseAutofacMiddleware(container);
            app.UseAutofacMvc();
            app.MapSignalR();
            // "/signalr", config

            // Enables cookie authentication with OWIN middleware
            app.UseCookieAuthentication(new CookieAuthenticationOptions {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                CookieHttpOnly = true,
                CookieSecure = CookieSecureOption.SameAsRequest,
                LoginPath = new PathString("/SignIn")
            });
        }
    }
}
