using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Spot.Models.Post;
using Spot.Models.Tag;
using Spot.Models.User;

namespace Spot.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<DatabaseContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
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
                if (!roleManager.RoleExists(role)) {
                    roleManager.Create(new IdentityRole(role));
                }
            }

            var user = new UserModel {
                Email = "test@localhost.dev",
                UserName = "test@localhost.dev",
                EmailConfirmed = true
            };

            var userResult = userManager.Create(user, "Asd12wer");

            if (userResult.Succeeded) {
                userManager.AddToRole(user.Id, roles[0]);
            }

            //Remove everything
            context.Posts.RemoveRange(context.Posts);
            context.Comments.RemoveRange(context.Comments);
            context.Tags.RemoveRange(context.Tags);

            //Reset primary key counter on posts and comments table
            context.Database.ExecuteSqlCommand("DBCC CHECKIDENT ('Posts', RESEED, 0)");
            context.Database.ExecuteSqlCommand("DBCC CHECKIDENT ('Comments', RESEED, 0)");
            context.Database.ExecuteSqlCommand("DBCC CHECKIDENT ('Tags', RESEED)");
            
            var posts = new List<PostModel> {
                new PostModel {
                    Title = "Lorem ipsum",
                    Excerpt = "Etiam ut magna vitae ex rhoncus pulvinar. Aliquam erat volutpat.",
                    Content = "Etiam ut magna vitae ex rhoncus pulvinar. Aliquam erat volutpat.",
                    Created = DateTime.Now,
                    Modified = DateTime.Now,
                    Published = DateTime.Now
                },
                new PostModel {
                    Title = "Aenean sit amet",
                    Excerpt = "Mauris id ullamcorper risus. Suspendisse consectetur ipsum ac fermentum condimentum.",
                    Content = "Mauris id ullamcorper risus. Suspendisse consectetur ipsum ac fermentum condimentum.",
                    Created = DateTime.Now.AddDays(-1),
                    Modified = DateTime.Now.AddDays(-1),
                    Published = DateTime.Now.AddDays(-1)
                },
                new PostModel {
                    Title = "Mauris id ullamcorper",
                    Excerpt = "Aenean sit amet placerat leo, ut rhoncus orci. Aenean imperdiet eget massa vel convallis.",
                    Content = "Aenean sit amet placerat leo, ut rhoncus orci. Aenean imperdiet eget massa vel convallis.",
                    Created = DateTime.Now.AddDays(-2),
                    Modified = DateTime.Now.AddDays(-2),
                    Published = DateTime.Now.AddDays(-2)
                },
                new PostModel {
                    Title = "Etiam ut magna vitae",
                    Excerpt = "Lorem ipsum dolor sit amet, consectetur adipiscing elit.",
                    Content = "Lorem ipsum dolor sit amet, consectetur adipiscing elit.",
                    Created = DateTime.Now.AddDays(-3),
                    Modified = DateTime.Now.AddDays(-3),
                    Published = DateTime.Now.AddDays(-3),
                    Tags = new List<TagModel> {
                        new TagModel {
                            Name = "Test"
                        }
                    }
                }
            };

            posts.ForEach(p => context.Posts.Add(p));
            context.SaveChanges();
        }
    }
}