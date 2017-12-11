using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using Spot.Models;

namespace Spot
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Tag> Tags { get; set; }

        public AppDbContext() : base("DefaultConnection") { }

        public static AppDbContext Create()
        {
            return new AppDbContext();
        }
    }
}