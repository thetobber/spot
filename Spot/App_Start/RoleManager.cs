using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Spot
{
    public class RoleManager : RoleManager<IdentityRole>
    {
        public RoleManager(RoleStore store) : base(store) { }
    }
}