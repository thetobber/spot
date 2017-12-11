using Microsoft.AspNet.Identity.EntityFramework;

namespace Spot
{
    public class RoleStore : RoleStore<IdentityRole>
    {
        public RoleStore(DatabaseContext context) : base(context) { }
    }
}