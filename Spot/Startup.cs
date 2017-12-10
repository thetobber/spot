using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;

[assembly: OwinStartup(typeof(Spot.Startup))]
namespace Spot
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseCookieAuthentication(new CookieAuthenticationOptions {
                AuthenticationType = "ApplicationCookie",
                CookieHttpOnly = true,
                CookieSecure = CookieSecureOption.SameAsRequest,
                LoginPath = new PathString("/Auth/SignIn"),
                LogoutPath = new PathString("/")
            });
        }
    }
}
