using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security.DataProtection;
using Spot.Models;

namespace Spot
{
    public class AppUserManager : UserManager<AppUser>
    {
        public AppUserManager(IUserStore<AppUser> store, IDataProtectionProvider dataProtectionProvider) : base(store)
        {
            UserValidator = new UserValidator<AppUser>(this) {
                RequireUniqueEmail = true,
                AllowOnlyAlphanumericUserNames = false
            };

            PasswordValidator = new PasswordValidator {
                RequiredLength = 6,
                RequireNonLetterOrDigit = false,
                RequireDigit = true,
                RequireLowercase = true,
                RequireUppercase = true
            };
            
            UserLockoutEnabledByDefault = false;

            UserTokenProvider = new DataProtectorTokenProvider<AppUser>(dataProtectionProvider.Create("ASP.NET Identity"));
        }
    }
}