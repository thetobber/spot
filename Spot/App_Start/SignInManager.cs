using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Spot.Models.User;

namespace Spot
{
    public class SignInManager : SignInManager<UserModel, string>
    {
        public SignInManager(UserManager userManager, IAuthenticationManager authManager) : base(userManager, authManager) { }

        public override Task<ClaimsIdentity> CreateUserIdentityAsync(UserModel user)
        {
            return user.GenerateUserIdentityAsync((UserManager)UserManager);
        }
    }
}