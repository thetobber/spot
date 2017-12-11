using Microsoft.AspNet.Identity.EntityFramework;
using Spot.Models.User;

namespace Spot
{
    public class UserStore : UserStore<UserModel>
    {
        public UserStore(DatabaseContext context) : base(context) { }
    }
}