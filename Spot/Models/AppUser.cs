using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Spot.Models
{
    public class AppUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<AppUser> manager)
        {
            var identity = await manager.CreateIdentityAsync(this, "ApplicationCookie");
            return identity;
        }
    }
}