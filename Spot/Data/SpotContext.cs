using System.Data.Entity;
using Spot.Models;

namespace Spot.Data
{
    public class SpotContext : DbContext
    {
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Tag> Tags { get; set; }

        public SpotContext() : base("DefaultConnection")
        {
        }
    }
}
