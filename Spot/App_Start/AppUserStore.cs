using Microsoft.AspNet.Identity.EntityFramework;
using Spot.Models;

namespace Spot
{
    public class AppUserStore : UserStore<AppUser>
    {
        public AppUserStore(AppDbContext context) : base(context) { }
    }
}