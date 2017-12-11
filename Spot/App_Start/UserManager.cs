using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security.DataProtection;
using Spot.Models.User;

namespace Spot
{
    public class UserManager : UserManager<UserModel>
    {
        public UserManager(IUserStore<UserModel> store, IDataProtectionProvider dataProtectionProvider) : base(store)
        {
            UserValidator = new UserValidator<UserModel>(this) {
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

            UserTokenProvider = new DataProtectorTokenProvider<UserModel>(dataProtectionProvider.Create("ASP.NET Identity"));
        }
    }
}