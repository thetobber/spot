using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using Spot.Models;

namespace Spot.Controllers
{
    [AllowAnonymous]
    public class AuthController : Controller
    {
        [HttpGet]
        public ActionResult SignIn(string returnUrl)
        {
            var model = new SignInViewModel {
                Email = "test@localhost.dev",
                Password = "123",
                ReturnUrl = returnUrl
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SignIn(SignInViewModel model)
        {
            if (!ModelState.IsValid)
                return View();
            
            if (model.Email == "test@localhost.dev" && model.Password == "123") {
                var claims = new Claim[] {
                    new Claim(ClaimTypes.Name, "Tobias"),
                    new Claim(ClaimTypes.Email, "test@localhost.dev"),
                    new Claim(ClaimTypes.Country, "Denmark")
                };

                var identity = new ClaimsIdentity(claims, "ApplicationCookie");

                Request.GetOwinContext().Authentication.SignIn(identity);

                if (string.IsNullOrEmpty(model.ReturnUrl) || !Url.IsLocalUrl(model.ReturnUrl)) {
                    return RedirectToAction("Index", "Home");
                }

                return Redirect(model.ReturnUrl);
            }

            ModelState.AddModelError("SignInViewModel", "Incorrect e-mail or password.");
            return View();
        }

        public RedirectToRouteResult SignOut()
        {
            Request.GetOwinContext().Authentication.SignOut("ApplicationCookie");
            return RedirectToAction("Index", "Home");
        }
    }
}