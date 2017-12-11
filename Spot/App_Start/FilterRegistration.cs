using System.Web.Mvc;

namespace Spot
{
    public class FilterRegistration
    {
        public static void Register(GlobalFilterCollection filters)
        {
            filters.Add(new RequireHttpsAttribute());
            filters.Add(new HandleErrorAttribute());
            //filters.Add(new ValidateAntiForgeryTokenAttribute());
            filters.Add(new AuthorizeAttribute());
        }
    }
}
