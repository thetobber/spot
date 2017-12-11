using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Spot.Models.Auth.ViewModels;
using Spot.Models.User;

namespace Spot.Controllers
{
    [AllowAnonymous]
    public class AuthController : Controller
    {
        private readonly UserManager UserManager;
        private readonly SignInManager SignInManager;
        private readonly IAuthenticationManager AuthManager;

        public AuthController(UserManager userManager, SignInManager signInManager, IAuthenticationManager authManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
            AuthManager = authManager;
        }

        [HttpGet]
        public ActionResult SignIn(string returnUrl) => View(new SignInViewModel { ReturnUrl = returnUrl });

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SignIn(SignInViewModel model)
        {
            if (!ModelState.IsValid) {
                ModelState.AddModelError("", "Incorrect e-mail or password.");
                return View();
            }

            var result = await SignInManager.PasswordSignInAsync(model.Email, model.Password, false, false);

            if (result == SignInStatus.Success)
                return RedirectToLocal(model.ReturnUrl);

            ModelState.AddModelError("", "Incorrect e-mail or password.");
            return View();
        }

        [HttpGet]
        public RedirectToRouteResult SignOut()
        {
            AuthManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult Register() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid) {
                var user = new UserModel {
                    UserName = model.Email,
                    Email = model.Email,
                };

                var result = await UserManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                    return RedirectToAction("SignIn");

                foreach (var error in result.Errors) {
                    ModelState.AddModelError("", error);
                }
            }

            return View(model);
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (string.IsNullOrEmpty(returnUrl) || !Url.IsLocalUrl(returnUrl)) {
                return RedirectToAction("Index", "Home");
            }

            return Redirect(returnUrl);
        }
    }
}