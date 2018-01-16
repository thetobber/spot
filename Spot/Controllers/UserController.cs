using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Spot.Models.User.ViewModels;
using Spot.Models.User;

namespace Spot.Controllers
{
    [AllowAnonymous]
    public class UserController : Controller
    {
        private readonly UserManager UserManager;
        private readonly RoleManager RoleManager;
        private readonly SignInManager SignInManager;
        private readonly IAuthenticationManager AuthManager;

        public UserController(UserManager userManager, RoleManager roleManager, SignInManager signInManager, IAuthenticationManager authManager)
        {
            UserManager = userManager;
            RoleManager = roleManager;
            SignInManager = signInManager;
            AuthManager = authManager;
        }

        [HttpGet]
        public ActionResult SignIn(string returnUrl)
        {
            if (User.Identity.IsAuthenticated) {
                return RedirectToAction("Index", "Home");
            }

            return View(new SignInViewModel { ReturnUrl = returnUrl });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SignIn(SignInViewModel model)
        {
            if (User.Identity.IsAuthenticated) {
                return RedirectToAction("Index", "Home");
            }

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

                if (result.Succeeded) {
                    var roleResult = await UserManager.AddToRoleAsync(user.Id, "Subscriber");

                    if (roleResult.Succeeded)
                        return RedirectToAction("SignIn");
                }

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