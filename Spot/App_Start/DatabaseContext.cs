using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using Spot.Models.Tag;
using Spot.Models.Post;
using Spot.Models.User;
using Spot.Models.Comment;

namespace Spot
{
    public class DatabaseContext : IdentityDbContext<UserModel>
    {
        public DbSet<PostModel> Posts { get; set; }
        public DbSet<CommentModel> Comments { get; set; }
        public DbSet<TagModel> Tags { get; set; }

        public DatabaseContext() : base("DefaultConnection") { }

        protected override void OnModelCreating(DbModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Custom models -> table names
            builder.Entity<PostModel>().ToTable("Posts");
            builder.Entity<CommentModel>().ToTable("Comments");
            builder.Entity<TagModel>().ToTable("Tags");
            builder.Entity<UserModel>().ToTable("Users");

            // Identity related model -> table names
            builder.Entity<IdentityUserRole>().ToTable("UserRoles");
            builder.Entity<IdentityUserLogin>().ToTable("UserLogins");
            builder.Entity<IdentityUserClaim>().ToTable("UserClaims");
            builder.Entity<IdentityRole>().ToTable("Roles");
        }
    }
}