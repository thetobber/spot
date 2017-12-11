using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Spot.Models.User
{
    public class UserModel : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<UserModel> manager) =>
            await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
    }
}