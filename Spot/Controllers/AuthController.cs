using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Mvc;
using Spot.Models;

namespace Spot.Controllers
{
    public class AuthController : Controller
    {
        [HttpGet]
        public ActionResult SignIn(string returnUrl)
        {
            var model = new SignInViewModel {
                ReturnUrl = returnUrl
            };

            return View();
        }

        [HttpPost]
        public async Task<ActionResult> SignIn(SignInViewModel model)
        {
            if (!ModelState.IsValid)
                return View();

            if (model.Email == "test@localhost.dev" && model.Password == "123") {

                var claims = new Claim[]
                {

                };

                var identity = new ClaimsIdentity();
                
                
                );

            }

        }
    }
}