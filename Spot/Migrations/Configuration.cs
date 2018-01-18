using System;
using System.Data.Entity.Migrations;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Spot.Models.Category;
using Spot.Models.Post;
using Spot.Models.User;

namespace Spot.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<DatabaseContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(DatabaseContext context)
        {
            var roleManager = new RoleManager(new RoleStore(context));
            var userManager = new UserManager<UserModel>(new UserStore(context));

            var roles = new[] {
                "Administrator",
                "Editor",
                "Subscriber"
            };

            foreach (var role in roles) {
                if (!roleManager.RoleExists(role))
                    roleManager.Create(new IdentityRole(role));
            }

            var user = new UserModel {
                Email = "test@localhost.dev",
                UserName = "Tobias",
                EmailConfirmed = true
            };

            var userResult = userManager.Create(user, "Asd12wer");

            if (userResult.Succeeded)
                userManager.AddToRole(user.Id, roles[0]);

            //Remove everything
            context.Posts.RemoveRange(context.Posts);
            context.Categories.RemoveRange(context.Categories);

            //Reset primary key counter
            context.Database.ExecuteSqlCommand("DBCC CHECKIDENT ('Posts', RESEED, 0)");
            context.Database.ExecuteSqlCommand("DBCC CHECKIDENT ('Categories', RESEED, 0)");

            var testCategory = new CategoryModel {
                Name = "Article",
                Description = "Curabitur ut diam ante. Fusce eget dapibus urna, sed consequat leo. Curabitur aliquet metus eget dui suscipit, in aliquam tellus faucibus. Maecenas eu libero aliquet dui tincidunt cursus. Curabitur at mollis quam, et eleifend quam."
            };

            context.Categories.Add(testCategory);

            for (var i = 0; i < 34; ++i) {
                context.Posts.Add(new PostModel {
                    Status = PostStatus.Public,
                    Title = $"Article title {i + 1}",
                    Excerpt = "Etiam ut magna vitae ex rhoncus pulvinar. Aliquam erat volutpat.",
                    Content = "Etiam ut magna vitae ex rhoncus pulvinar. Aliquam erat volutpat.",
                    Created = DateTime.Now,
                    Modified = DateTime.Now,
                    Published = DateTime.Now,
                    Category = testCategory,
                    Author = user.UserName
                });
            }

            context.SaveChanges();
        }
    }
}