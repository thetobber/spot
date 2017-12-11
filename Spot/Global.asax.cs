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
            FilterRegistration.Register(GlobalFilters.Filters);
            RouteRegistration.Register(RouteTable.Routes);
        }
    }
}
