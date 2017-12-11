using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Spot.Models;

namespace Spot
{
    public class AppSignInManager : SignInManager<AppUser, string>
    {
        public AppSignInManager(AppUserManager userManager, IAuthenticationManager authManager) : base(userManager, authManager) { }

        public override Task<ClaimsIdentity> CreateUserIdentityAsync(AppUser user)
        {
            return user.GenerateUserIdentityAsync((AppUserManager)UserManager);
        }
    }
}